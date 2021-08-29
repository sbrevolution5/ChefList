﻿using System;
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
    public class ShoppingIngredientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShoppingIngredientsController(ApplicationDbContext context)
        {
            _context = context;
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
            if (id != shoppingIngredient.Id)
            {
                return BadRequest();
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

        private bool ShoppingIngredientExists(int id)
        {
            return _context.ShoppingIngredient.Any(e => e.Id == id);
        }
    }
}