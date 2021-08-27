using MasterMealWA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Server.Services.Interfaces
{
    public interface ISupplyService
    {
        public Task<Supply> GetSupplyByIdAsync(int supplyId);
        public Task<List<Supply>> GetSuppliesForRecipeAsync(int RecipeId);
        public Task<List<Supply>> GetSuppliesForMultipleRecipesAsync(List<Recipe> Recipes);
    }
}
