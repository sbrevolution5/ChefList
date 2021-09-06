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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MasterMealWA.Server.Extensions;

namespace MasterMealWA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingListsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IShoppingService _shoppingService;
        private readonly UserManager<Chef> _userManager;


        public ShoppingListsController(ApplicationDbContext context, IShoppingService shoppingService, UserManager<Chef> userManager)
        {
            _context = context;
            _shoppingService = shoppingService;
            _userManager = userManager;
        }

        // GET: api/ShoppingLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingList>>> GetShoppingList()
        {
            var userId = HttpContext.GetUserId();

            return await _context.ShoppingList.Where(s => s.ChefId == userId).ToListAsync();
        }

        // GET: api/ShoppingLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingList>> GetShoppingList(int id)
        {
            var shoppingList = await _context.ShoppingList.Include(l => l.ShoppingIngredients).ThenInclude(s => s.Ingredient).Where(l => l.Id == id).FirstOrDefaultAsync();
            var userId = HttpContext.GetUserId();

            if (shoppingList == null || shoppingList.ChefId != userId)
            {
                return NotFound();
            }

            return shoppingList;
        }

        // PUT: api/ShoppingLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingList(int id, ShoppingList shoppingList)
        {
            var userId = _userManager.GetUserId(User);
            if (id != shoppingList.Id||shoppingList.ChefId != userId)
            {
                return BadRequest();
            }

            _context.Entry(shoppingList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingListExists(id))
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

        // POST: api/ShoppingLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShoppingList>> PostShoppingList(ListCreateDto shoppingList)
        {

            var userId = HttpContext.GetUserId();
            var list = await _shoppingService.CreateShoppingListForDateRangeAsync(shoppingList.EndDate, shoppingList.StartDate,userId);
            return CreatedAtAction("GetShoppingList", new { id = list.Id }, list);
        }

        // DELETE: api/ShoppingLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingList(int id)
        {
            var shoppingList = await _context.ShoppingList.Include(s => s.ShoppingIngredients).FirstOrDefaultAsync(s => s.Id == id);
            if (shoppingList == null)
            {
                return NotFound();
            }
            _context.RemoveRange(shoppingList.ShoppingIngredients);
            _context.ShoppingList.Remove(shoppingList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppingListExists(int id)
        {
            return _context.ShoppingList.Any(e => e.Id == id);
        }
    }
}
