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
using MasterMealWA.Shared.Models.Dtos;
using MasterMealWA.Server.Extensions;

namespace MasterMealWA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RatingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RatingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Ratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRating()
        {
            return await _context.Rating.ToListAsync();
        }

        // GET: api/Ratings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetRating(int id)
        {
            var rating = await _context.Rating.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            return rating;
        }

        // PUT: api/Ratings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutRating(RatingEditDto dto)
        {
            var userId = HttpContext.GetUserId();
            var rating = await _context.Rating.Where(r => r.ChefId == dto.ChefId && r.RecipeId == dto.RecipeId).FirstOrDefaultAsync();
            if (rating.RecipeId != dto.RecipeId || rating.ChefId != dto.ChefId || userId != dto.ChefId)
            {
                return BadRequest();
            }
            rating.Stars = dto.NewRating;
            _context.Entry(rating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Ratings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rating>> PostRating(Rating rating)
        {
            var userId = HttpContext.GetUserId();
            
            if (await UserPreviouslyRatedRecipeAsync(userId, rating.RecipeId))
            {
                return BadRequest();
            }
            _context.Rating.Add(rating);
            var recipe = await _context.Recipe.Where(r => r.Id == rating.RecipeId).FirstOrDefaultAsync();
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRating", new { id = rating.Id }, rating);
        }

        private async Task<bool> UserPreviouslyRatedRecipeAsync(string userId, int recipeId)
        {
            var recipe = await _context.Recipe.Where(r => r.Id == recipeId).Include(r => r.Ratings).FirstOrDefaultAsync();
            var userRating = recipe.Ratings.Select(r => r.ChefId).Where(c => c == userId).FirstOrDefault();
            if (userRating is null)
            {
                return false;
            }
            return true;
        }

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            var rating = await _context.Rating.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            _context.Rating.Remove(rating);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RatingExists(int id)
        {
            return _context.Rating.Any(e => e.Id == id);
        }
    }
}
