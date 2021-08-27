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
    public class DBImagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DBImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DBImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DBImage>>> GetDBImage()
        {
            return await _context.DBImage.ToListAsync();
        }

        // GET: api/DBImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DBImage>> GetDBImage(int id)
        {
            var dBImage = await _context.DBImage.FindAsync(id);

            if (dBImage == null)
            {
                return NotFound();
            }

            return dBImage;
        }

        // PUT: api/DBImages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDBImage(int id, DBImage dBImage)
        {
            if (id != dBImage.Id)
            {
                return BadRequest();
            }

            _context.Entry(dBImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DBImageExists(id))
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

        // POST: api/DBImages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DBImage>> PostDBImage(DBImage dBImage)
        {
            _context.DBImage.Add(dBImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDBImage", new { id = dBImage.Id }, dBImage);
        }

        // DELETE: api/DBImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDBImage(int id)
        {
            var dBImage = await _context.DBImage.FindAsync(id);
            if (dBImage == null)
            {
                return NotFound();
            }

            _context.DBImage.Remove(dBImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DBImageExists(int id)
        {
            return _context.DBImage.Any(e => e.Id == id);
        }
    }
}
