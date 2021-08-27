using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasterMealWA.Server.Data;
using MasterMealWA.Shared.Models;

namespace MasterMealWA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChefsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Chefs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chef>>> GetChef()
        {
            return await _context.Chef.ToListAsync();
        }

        // GET: api/Chefs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chef>> GetChef(Guid id)
        {
            var chef = await _context.Chef.FindAsync(id);

            if (chef == null)
            {
                return NotFound();
            }

            return chef;
        }

        // PUT: api/Chefs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChef(Guid id, Chef chef)
        {
            if (id != chef.Id)
            {
                return BadRequest();
            }

            _context.Entry(chef).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChefExists(id))
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

        // POST: api/Chefs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Chef>> PostChef(Chef chef)
        {
            _context.Chef.Add(chef);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChef", new { id = chef.Id }, chef);
        }

        // DELETE: api/Chefs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChef(Guid id)
        {
            var chef = await _context.Chef.FindAsync(id);
            if (chef == null)
            {
                return NotFound();
            }

            _context.Chef.Remove(chef);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChefExists(Guid id)
        {
            return _context.Chef.Any(e => e.Id == id);
        }
    }
}
