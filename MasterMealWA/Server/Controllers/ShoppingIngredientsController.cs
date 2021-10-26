using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MasterMealWA.Server.Data;
using MasterMealWA.Shared.Models;
using MasterMealWA.Shared.Models.Dtos;
using MasterMealWA.Server.Services.Interfaces;

namespace MasterMealWA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingIngredientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMeasurementService _measurementService;

        public ShoppingIngredientsController(ApplicationDbContext context, IMeasurementService measurementService)
        {
            _context = context;
            _measurementService = measurementService;
        }

        // GET: api/ShoppingIngredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingIngredient>>> GetShoppingIngredient()
        {
            return await _context.ShoppingIngredient.ToListAsync();
        }

        // GET: api/ShoppingIngredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingIngredient>> GetShoppingIngredient(int id)
        {
            var shoppingIngredient = await _context.ShoppingIngredient.FindAsync(id);

            if (shoppingIngredient == null)
            {
                return NotFound();
            }

            return shoppingIngredient;
        }

        // PUT: api/ShoppingIngredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingIngredient(int id, ShoppingIngredient shoppingIngredient)
        {
            //if quantity has changed in any way, we need to recalculate measurement and add a note
            if (id != shoppingIngredient.Id)
            {
                return BadRequest();
            }
            if (shoppingIngredient.MeasurementType == Shared.Enums.MeasurementType.Volume)
            {

                shoppingIngredient.Quantity = _measurementService.EncodeVolumeMeasurement(shoppingIngredient.QuantityNumber, shoppingIngredient.Fraction, shoppingIngredient.VolumeMeasurementUnit.Value);
            }
            else if (shoppingIngredient.MeasurementType == Shared.Enums.MeasurementType.Mass)
            {
                shoppingIngredient.Quantity = _measurementService.EncodeMassMeasurement(shoppingIngredient.QuantityNumber, shoppingIngredient.Fraction, shoppingIngredient.MassMeasurementUnit.Value);
            }
            else
            {
                shoppingIngredient.Quantity = _measurementService.EncodeUnitMeasurement(shoppingIngredient.QuantityNumber, shoppingIngredient.Fraction);
            }
            var oldIngredient = await _context.ShoppingIngredient.Include(s => s.Ingredient).Include(s => s.IngredientType).AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            if (oldIngredient.Quantity != shoppingIngredient.Quantity)
            {
                shoppingIngredient = _measurementService.GetMeasurementForShoppingIngredient(shoppingIngredient);
                if (!shoppingIngredient.Notes.Where(s => s.StartsWith("Edited")).Any())
                {
                    shoppingIngredient.Notes.Add($"Edited from: {oldIngredient.QuantityString}");
                }
            }
            _context.Entry(shoppingIngredient).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingIngredientExists(id))
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

        // POST: api/ShoppingIngredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShoppingIngredient>> PostShoppingIngredient(ShoppingIngredient shoppingIngredient)
        {
            _context.ShoppingIngredient.Add(shoppingIngredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingIngredient", new { id = shoppingIngredient.Id }, shoppingIngredient);
        }
        //POST: api/ShoppingIngredients/AddToList/
        [HttpPost("AddToList")]
        public async Task<ActionResult<ShoppingIngredient>> PostShoppingIngredientAndAddToList(AddToShoppingDto dto)
        {

            var list = await _context.ShoppingList.Include(s => s.ShoppingIngredients).Where(s => s.Id == dto.ListId).FirstOrDefaultAsync();

            _context.ShoppingIngredient.Add(dto.Ingredient);
            list.ShoppingIngredients.Add(dto.Ingredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingIngredient", new { id = dto.Ingredient.Id }, dto.Ingredient);
        }

        // DELETE: api/ShoppingIngredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingIngredient(int id)
        {
            var shoppingIngredient = await _context.ShoppingIngredient.FindAsync(id);
            if (shoppingIngredient == null)
            {
                return NotFound();
            }

            _context.ShoppingIngredient.Remove(shoppingIngredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}/{listId}")]
        public async Task<IActionResult> DeleteShoppingIngredient(int id, int listId)
        {
            var shoppingList = await _context.ShoppingList.Include(s => s.ShoppingIngredients).Where(s => s.Id == listId).FirstOrDefaultAsync();
            var shoppingIngredient = await _context.ShoppingIngredient.FindAsync(id);
            if (shoppingIngredient == null)
            {
                return NotFound();
            }

            shoppingList.ShoppingIngredients.Remove(shoppingIngredient);
            _context.ShoppingIngredient.Remove(shoppingIngredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppingIngredientExists(int id)
        {
            return _context.ShoppingIngredient.Any(e => e.Id == id);
        }
    }
}
