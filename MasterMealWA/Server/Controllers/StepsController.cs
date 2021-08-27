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
    public class StepsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StepsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Steps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Step>>> GetStep()
        {
            return await _context.Step.ToListAsync();
        }

        // GET: api/Steps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Step>> GetStep(int id)
        {
            var step = await _context.Step.FindAsync(id);

            if (step == null)
            {
                return NotFound();
            }

            return step;
        }

        // PUT: api/Steps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStep(int id, Step step)
        {
            if (id != step.Id)
            {
                return BadRequest();
            }

            _context.Entry(step).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StepExists(id))
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

        // POST: api/Steps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Step>> PostStep(Step step)
        {
            _context.Step.Add(step);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStep", new { id = step.Id }, step);
        }

        // DELETE: api/Steps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStep(int id)
        {
            var step = await _context.Step.FindAsync(id);
            if (step == null)
            {
                return NotFound();
            }

            _context.Step.Remove(step);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StepExists(int id)
        {
            return _context.Step.Any(e => e.Id == id);
        }
    }
}
