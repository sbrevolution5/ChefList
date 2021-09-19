using MasterMealWA.Server.Data;
using MasterMealWA.Server.Services.Interfaces;
using MasterMealWA.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Server.Services
{
    public class ServingService : IServingService
    {
        private readonly ApplicationDbContext _context;

        public ServingService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Recipe> ScaleRecipeAsync(int recipeId, int desiredServings)
        {
            var recipe = await _context.Recipe.Include(r => r.Ingredients).ThenInclude(r => r.Ingredient).AsNoTracking().FirstOrDefaultAsync(r => r.Id == recipeId);
            Recipe singleServe = ConvertRecipeToSingleServing(recipe);
            Recipe correctServing = UpscaleServing(singleServe, desiredServings);
            throw new NotImplementedException();
        }

        private Recipe ConvertRecipeToSingleServing(Recipe recipe)
        {
            var defaultServings = recipe.Servings;
            foreach (var ingredient in recipe.Ingredients)
            {
                ingredient.NumberOfUnits /= defaultServings;
            }
            return recipe;
        }

        private Recipe UpscaleServing(Recipe singleServe, int desiredServings)
        {
            foreach (var ingredient in singleServe.Ingredients)
            {
                ingredient.NumberOfUnits *= desiredServings;

            }
            return singleServe;
        }
    }
}
