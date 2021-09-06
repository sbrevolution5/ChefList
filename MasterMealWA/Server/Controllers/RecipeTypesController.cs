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

namespace MasterMealWA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecipeTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecipeTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RecipeTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeTag>>> GetRecipeType()
        {
            var typelist = await _context.RecipeTag.ToListAsync();
            return typelist;
        }

        // GET: api/RecipeTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeTag>> GetRecipeType(int id)
        {
            var recipeType = await _context.RecipeTag.FindAsync(id);

            if (recipeType == null)
            {
                return NotFound();
            }

            return recipeType;
        }

        // PUT: api/RecipeTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipeType(int id, RecipeTag recipeType)
        {
            if (id != recipeType.Id)
            {
                return BadRequest();
            }

            _context.Entry(recipeType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeTypeExists(id))
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

        // POST: api/RecipeTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RecipeTag>> PostRecipeType(RecipeTag recipeType)
        {
            _context.RecipeTag.Add(recipeType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipeType", new { id = recipeType.Id }, recipeType);
        }

        // DELETE: api/RecipeTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeType(int id)
        {
            var recipeTag = await _context.RecipeTag.FindAsync(id);
            if (recipeTag == null)
            {
                return NotFound();
            }

            _context.RecipeTag.Remove(recipeTag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeTypeExists(int id)
        {
            return _context.RecipeTag.Any(e => e.Id == id);
        }
    }
}
