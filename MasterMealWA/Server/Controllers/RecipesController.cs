using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasterMealWA.Server.Data;
using MasterMealWA.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using MasterMealWA.Shared.Enums;
using MasterMealWA.Server.Services.Interfaces;
using SixLabors.ImageSharp;
using Microsoft.AspNetCore.Identity;
using MasterMealWA.Shared.Models.Dtos;
using MasterMealWA.Server.Extensions;

namespace MasterMealWA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecipesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMeasurementService _measurementService;
        private readonly IFileService _fileService;
        private readonly IServingService _servingService;
        public RecipesController(ApplicationDbContext context, IMeasurementService measurementService, IFileService fileService, IServingService servingService)
        {
            _context = context;
            _measurementService = measurementService;
            _fileService = fileService;
            _servingService = servingService;
        }

        // GET: api/Recipes
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipe()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return await _context.Recipe.Include(r => r.Author).Include(r => r.Ratings).Include(r => r.Image).Where(r => !r.IsPrivate).Include(r => r.Tags).ToListAsync();
            }
            var userId = HttpContext.GetUserId();
            return await _context.Recipe.Include(r => r.Author).Include(r => r.Image).Include(r => r.Supplies).ThenInclude(s => s.Supply).Where(r => !r.IsPrivate || r.AuthorId == userId).Include(r => r.Tags).Include(r => r.Ratings).ToListAsync();
        }
        // GET: api/Recipes/ingredients
        [HttpGet("ingredients")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipeForIngredientPage()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return await _context.Recipe.Include(r => r.Author).Include(r => r.Ingredients).ThenInclude(r => r.Ingredient).Where(r => !r.IsPrivate).ToListAsync();
            }
            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                return await _context.Recipe.Include(r => r.Author).Include(r => r.Ingredients).ThenInclude(r => r.Ingredient).ToListAsync();

            }
            var userId = HttpContext.GetUserId();
            return await _context.Recipe.Include(r => r.Ingredients).ThenInclude(r => r.Ingredient).Where(r => !r.IsPrivate || r.AuthorId == userId).ToListAsync();
        }
        // GET: api/Recipes/myrecipes
        [HttpGet("myrecipes")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetMyRecipes()
        {

            var userId = HttpContext.GetUserId();
            return await _context.Recipe.Include(r => r.Author).Include(r => r.Tags).Include(r => r.Ratings).Include(r => r.Image).Where(r => r.AuthorId == userId).ToListAsync();
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _context.Recipe.Include(r => r.Steps)
                                              .Include(r => r.Supplies)
                                              .ThenInclude(q => q.Supply)
                                              .Include(r => r.Tags)
                                              .Include(r => r.Ingredients)
                                              .ThenInclude(r => r.Ingredient)
                                              .Include(r => r.Author)
                                              .Include(r => r.Ratings)
                                              .Include(r => r.Image)
                                              .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        // PUT: api/Recipes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, RecipeEditDto recipeDto)
        {
            var userId = HttpContext.GetUserId();
            var recipe = recipeDto.Recipe;
            if (recipe.AuthorId != userId)
            {
                return BadRequest();
            }
            #region Image
            if (!recipeDto.ResetImage && recipeDto.ImageChanged)
            {

                if (recipe.ImageId == 1 && recipe.Image.Id != 1)
                {
                    _context.Add(recipe.Image);
                    await _context.SaveChangesAsync();
                    recipe.ImageId = recipe.Image.Id;

                }
                else
                {
                    var img = await _context.DBImage.FindAsync(recipe.ImageId);
                    img.ImageData = recipe.Image.ImageData;
                    img.ContentType = recipe.Image.ContentType;
                }
            }
            else if (recipeDto.ResetImage)
            {
                recipe.Image = null;
                recipe.ImageId = 1;
            }
            #endregion Image
            var tags = recipeDto.RecipeTags;
            var dbrecipe = await _context.Recipe.Include(r => r.Tags).FirstOrDefaultAsync(r => r.Id == id);
            //Any tags on database recipe that aren't on the dto recipe must be removed
            dbrecipe.Tags.Where(tag => !recipeDto.RecipeTags.Any(id => id.Id == tag.Id)).ToList()
                         .ForEach(tag => dbrecipe.Tags.Remove(tag));
            //any tags on the dto recipe that aren't on the database recipe need to be added
            recipeDto.RecipeTags.Where(id => !dbrecipe.Tags.Any(tag => tag.Id == id.Id)).ToList().ForEach(id => dbrecipe.Tags.Add(_context.RecipeTag.Where(t => t.Id == id.Id).First()));
            if (id != dbrecipe.Id)
            {
                return BadRequest();
            }
            dbrecipe.AuthorId = recipe.AuthorId;
            dbrecipe.Steps = recipe.Steps;
            dbrecipe.Ingredients = recipe.Ingredients;
            foreach (var ingredient in dbrecipe.Ingredients)
            {
                //ingredient.RecipeId = recipe.Id;
                if (ingredient.MeasurementType == MeasurementType.Volume)
                {
                    ingredient.MassMeasurementUnit = null;
                    ingredient.NumberOfUnits = _measurementService.EncodeVolumeMeasurement(ingredient.QuantityNumber, ingredient.Fraction, ingredient.VolumeMeasurementUnit.Value);
                    ingredient.Quantity = _measurementService.DecodeVolumeMeasurement(ingredient.NumberOfUnits);
                }
                else if (ingredient.MeasurementType == MeasurementType.Mass)
                {
                    ingredient.VolumeMeasurementUnit = null;
                    ingredient.NumberOfUnits = _measurementService.EncodeMassMeasurement(ingredient.QuantityNumber, ingredient.Fraction, ingredient.MassMeasurementUnit.Value);
                    ingredient.Quantity = _measurementService.DecodeMassMeasurement(ingredient.NumberOfUnits);
                }
                else if (ingredient.MeasurementType == MeasurementType.Count)
                {
                    ingredient.VolumeMeasurementUnit = null;
                    ingredient.MassMeasurementUnit = null;
                    ingredient.NumberOfUnits = _measurementService.EncodeUnitMeasurement(ingredient.QuantityNumber, ingredient.Fraction);
                    ingredient.Quantity = _measurementService.DecodeUnitMeasurement(ingredient.NumberOfUnits);
                }
                _context.Entry(ingredient).State = EntityState.Modified;
            }
            dbrecipe.Supplies = recipe.Supplies;
            dbrecipe.Description = recipe.Description;
            dbrecipe.Name = recipe.Name;
            dbrecipe.RecipeSource = recipe.RecipeSource;
            dbrecipe.RecipeSourceUrl = recipe.RecipeSourceUrl;
            dbrecipe.Servings = recipe.Servings;
            dbrecipe.CookingTime = recipe.CookingTime;
            dbrecipe.ImageId = recipe.ImageId;
            _context.Entry(dbrecipe).State = EntityState.Modified;
            _context.QIngredient.RemoveRange(recipeDto.IngredientsToRemove);
            _context.QSupply.RemoveRange(recipeDto.SuppliesToRemove);
            _context.Step.RemoveRange(recipeDto.StepsToRemove);
            foreach (var step in dbrecipe.Steps)
            {
                _context.Entry(step).State = EntityState.Modified;
            }

            foreach (var supply in dbrecipe.Supplies)
            {
                _context.Entry(supply).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Recipes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            if (recipe.Image is not null && recipe.Image.Id != 1)
            {

                _context.DBImage.Add(recipe.Image);
                await _context.SaveChangesAsync();
                recipe.ImageId = recipe.Image.Id;
            }
            else
            {
                recipe.ImageId = 1;
            }
            _context.UpdateRange(recipe.Tags);
            recipe.Created = DateTime.Now;
            _context.Recipe.Add(recipe);
            foreach (var ingredient in recipe.Ingredients)
            {
                //ingredient.RecipeId = recipe.Id;
                if (ingredient.MeasurementType == MeasurementType.Volume)
                {
                    ingredient.MassMeasurementUnit = null;
                    ingredient.NumberOfUnits = _measurementService.EncodeVolumeMeasurement(ingredient.QuantityNumber, ingredient.Fraction, ingredient.VolumeMeasurementUnit.Value);
                    ingredient.Quantity = _measurementService.DecodeVolumeMeasurement(ingredient.NumberOfUnits);
                }
                else if (ingredient.MeasurementType == MeasurementType.Mass)
                {
                    ingredient.VolumeMeasurementUnit = null;
                    ingredient.NumberOfUnits = _measurementService.EncodeMassMeasurement(ingredient.QuantityNumber, ingredient.Fraction, ingredient.MassMeasurementUnit.Value);
                    ingredient.Quantity = _measurementService.DecodeMassMeasurement(ingredient.NumberOfUnits);
                }
                else if (ingredient.MeasurementType == MeasurementType.Count)
                {
                    ingredient.VolumeMeasurementUnit = null;
                    ingredient.MassMeasurementUnit = null;
                    ingredient.NumberOfUnits = _measurementService.EncodeUnitMeasurement(ingredient.QuantityNumber, ingredient.Fraction);
                    ingredient.Quantity = _measurementService.DecodeUnitMeasurement(ingredient.NumberOfUnits);
                }
                _context.Add(ingredient);
            }
            foreach (var supply in recipe.Supplies)
            {
                supply.RecipeId = recipe.Id;
                _context.Add(supply);
            }
            foreach (var step in recipe.Steps)
            {
                step.RecipeId = recipe.Id;
                _context.Add(step);
            }
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRecipe", new { id = recipe.Id }, recipe);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var userId = HttpContext.GetUserId();
            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe.AuthorId != userId)
            {
                return BadRequest();
            }
            if (recipe == null)
            {
                return NotFound();
            }
            if (recipe.ImageId != 1)
            {
                var image = await _context.DBImage.FindAsync(recipe.ImageId);
                _context.DBImage.Remove(image);
            }
            _context.RemoveRange(recipe.Steps);
            _context.RemoveRange(recipe.Supplies);
            _context.RemoveRange(recipe.Ingredients);
            _context.Recipe.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [AllowAnonymous]
        [Route("{recipeId}/scale/{desiredServings}")]
        [HttpGet("{recipeId}/scale/{desiredServings}")]
        public async Task<ActionResult<List<QIngredient>>> ScaleRecipe(int recipeId, int desiredServings)
        {
            try
            {

                var result = await _servingService.ScaleRecipeAsync(recipeId, desiredServings);
                foreach (var ingredient in result.Ingredients)
                {
                    //ingredient.RecipeId = recipe.Id;
                    if (ingredient.MeasurementType == MeasurementType.Volume)
                    {
                        ingredient.MassMeasurementUnit = null;
                        ingredient.Quantity = _measurementService.DecodeVolumeMeasurement(ingredient.NumberOfUnits);
                    }
                    else if (ingredient.MeasurementType == MeasurementType.Mass)
                    {
                        ingredient.VolumeMeasurementUnit = null;
                        ingredient.Quantity = _measurementService.DecodeMassMeasurement(ingredient.NumberOfUnits);
                    }
                    else if (ingredient.MeasurementType == MeasurementType.Count)
                    {
                        ingredient.VolumeMeasurementUnit = null;
                        ingredient.MassMeasurementUnit = null;
                        ingredient.Quantity = _measurementService.DecodeUnitMeasurement(ingredient.NumberOfUnits);
                    }
                }
                return result.Ingredients.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private bool RecipeExists(int id)
        {
            return _context.Recipe.Any(e => e.Id == id);
        }
    }
}
