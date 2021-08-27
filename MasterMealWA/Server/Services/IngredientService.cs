using MasterMealWA.Server.Data;
using MasterMealWA.Shared.Enums;
using MasterMealWA.Server.Models;
using MasterMealWA.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterMealWA.Shared.Models;

namespace MasterMealWA.Server.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IDbContextFactory<ApplicationDbContext> ContextFactory;

        public IngredientService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            ContextFactory = contextFactory;
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int ingredientId)
        {
            using var _context = ContextFactory.CreateDbContext();
            var ingredient = await _context.Ingredient.FirstOrDefaultAsync(i => i.Id == ingredientId);
            return ingredient;
        }

        public async Task<MeasurementType> GetMeasurementTypeOfIngredientByIdAsync(int ingredientId)
        {
            var ingredient = await GetIngredientByIdAsync(ingredientId);
            return ingredient.MeasurementType;
        }

        public Task<List<Ingredient>> GetRecipeIngredientsAsync(int RecipeId)
        {

            throw new NotImplementedException();
        }

        public Task<List<Ingredient>> GetShoppingListIngredientsAsync(int shoppingListId)
        {
            throw new NotImplementedException();
        }
    }
}
