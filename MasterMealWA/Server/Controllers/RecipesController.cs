﻿using System;
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

namespace MasterMealWA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMeasurementService _measurementService;
        public RecipesController(ApplicationDbContext context, IMeasurementService measurementService)
        {
            _context = context;
            _measurementService = measurementService;
        }

        // GET: api/Recipes
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipe()
        {
            return await _context.Recipe.ToListAsync();
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _context.Recipe.Include(r => r.Steps)
                                              .Include(r => r.Supplies)
                                              .Include(r => r.Type)
                                              .Include(r => r.Ingredients)
                                              .ThenInclude(r => r.Ingredient)
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
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

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

            //int imageId = 1;
            //if (recipe.Image is not null)
            //{
            //    using var image = Image.Load(recipe.Image.OpenReadStream());
            //    var imageBytes = await _fileService.ConvertFileToByteArrayAsync(image, imageFile.ContentType);
            //    DBImage dBImage = new()
            //    {
            //        ContentType = imageFile.ContentType,
            //        ImageData = imageBytes
            //    };
            //    context.Add(dBImage);
            //    await context.SaveChangesAsync();
            //    imageId = dBImage.Id;
            //}
            recipe.ImageId = 1;
            //var allSupplies = await _context.Supply.ToListAsync();
            //List<Supply> supplies = recipe.Supplies.ToList();
            //recipe.Supplies.Clear();
            //foreach (var sup in supplies)
            //{
            //    var thisSupply = allSupplies.Where(s => s.Id == sup.Id).FirstOrDefault();
            //    thisSupply.Recipes.Add(recipe);
            //    recipe.Supplies.Add(thisSupply);
            //    _context.Update(thisSupply);
            //}
            _context.Add(recipe);
            foreach (var ingredient in recipe.Ingredients)
            {
                ingredient.RecipeId = recipe.Id;
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
            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipe.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipe.Any(e => e.Id == id);
        }
    }
}
