using MasterMealWA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Client.Services.Interfaces
{
    public interface IApiService
    {
        Task CreateNewRecipeAsync(Recipe recipe);
        Task CreateNewRecipeTypeAsync(RecipeType type);
        Task CreateNewMealAsync(Meal meal);
        Task CreateNewIngredientAsync(Ingredient ingredient);
        Task CreateNewIngredientTypeAsync(IngredientType ingredientType);
        Task CreateNewShoppingListAsync(ShoppingList shoppingList);
        Task CreateNewSupplyAsync(Supply supply);
        Task UpdateRecipeAsync(Recipe recipe);
        Task UpdateRecipeTypeAsync(RecipeType type);
        Task UpdateMealAsync(Meal meal);
        Task UpdateIngredientAsync(Ingredient ingredient);
        Task UpdateIngredientTypeAsync(IngredientType ingredientType);
        Task UpdateShoppingListAsync(ShoppingList shoppingList);
        Task UpdateSupplyAsync(Supply supply);
        Task DeleteRecipeAsync(int id);
        Task DeleteRecipeTypeAsync(int id);
        Task DeleteMealAsync(int id);
        Task DeleteIngredientAsync(int id);
        Task DeleteIngredientTypeAsync(int id);
        Task DeleteShoppingListAsync(int id);
        Task DeleteSupplyAsync(int id);
        Task<Recipe> GetRecipeAsync(int id);
        Task<RecipeType> GetRecipeTypeAsync(int id);
        Task<Meal> GetMealAsync(int id);
        Task<Ingredient> GetIngredientAsync(int id);
        Task<IngredientType> GetIngredientTypeAsync(int id);
        Task<ShoppingList> GetShoppingListAsync(int id);
        Task<Supply> GetSupplyAsync(int id);

        
    }
}
