using MasterMealWA.Shared.Enums;
using MasterMealWA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Server.Services.Interfaces
{
    public interface IIngredientService
    {
        public Task<MeasurementType> GetMeasurementTypeOfIngredientByIdAsync(int ingredientId);
        public Task<Ingredient> GetIngredientByIdAsync(int ingredientId);
        public Task<List<Ingredient>> GetRecipeIngredientsAsync(int RecipeId);
        public Task<List<Ingredient>> GetShoppingListIngredientsAsync(int shoppingListId);
    }
}
