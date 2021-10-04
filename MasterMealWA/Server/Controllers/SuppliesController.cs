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
using MasterMealWA.Server.Extensions;

namespace MasterMealWA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SuppliesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SuppliesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Supplies
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Supply>>> GetSupply()
        {
            return await _context.Supply.ToListAsync();
        }
        [HttpGet("user")]
        public async Task<ActionResult<List<Supply>>> GetUserSupply()
        {
            var userId = HttpContext.GetUserId();
            var user = await _context.Users.Include(u => u.ChefSupplies).FirstOrDefaultAsync(u => u.Id == userId);
            var userSupplies = user.ChefSupplies.ToList();
            return userSupplies;
        }
        [HttpPut("user")]
        public async Task<ActionResult> UpdateUserSupply(List<Supply> supplies)
        {
            
            var userId = HttpContext.GetUserId();
            var user = await _context.Users.Include(u=>u.ChefSupplies).FirstOrDefaultAsync(u => u.Id == userId);
            //Any tags on database recipe that aren't on the dto recipe must be removed
            user.ChefSupplies.Where(sup => !supplies.Any(id => id.Id == sup.Id)).ToList()
                         .ForEach(sup => user.ChefSupplies.Remove(sup));
            //any tags on the incoming list that aren't on the database list need to be added
            supplies.Where(sup => !user.ChefSupplies.Any(sup2 => sup2.Id == sup.Id)).ToList().ForEach(sup => user.ChefSupplies.Add(_context.Supply.Where(s => s.Id == sup.Id).First()));
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/Supplies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supply>> GetSupply(int id)
        {
            var supply = await _context.Supply.FindAsync(id);

            if (supply == null)
            {
                return NotFound();
            }

            return supply;
        }

        // PUT: api/Supplies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupply(int id, Supply supply)
        {
            var duplicate = await _context.Supply.Where(i => i.Id != supply.Id && i.Name.ToLower() == supply.Name.ToLower()).FirstOrDefaultAsync();
            if (duplicate is not null)
            {
                return BadRequest();
            }
            if (id != supply.Id)
            {
                return BadRequest();
            }

            _context.Entry(supply).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplyExists(id))
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

        // POST: api/Supplies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Supply>> PostSupply(Supply supply)
        {
            var duplicate = await _context.Supply.Where(i => i.Name.ToLower() == supply.Name.ToLower()).FirstOrDefaultAsync();
            if (duplicate is not null)
            {
                return BadRequest();
            }
            _context.Supply.Add(supply);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupply", new { id = supply.Id }, supply);
        }

        // DELETE: api/Supplies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupply(int id)
        {
            var supply = await _context.Supply.FindAsync(id);
            if (supply == null)
            {
                return NotFound();
            }

            _context.Supply.Remove(supply);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupplyExists(int id)
        {
            return _context.Supply.Any(e => e.Id == id);
        }
    }
}
