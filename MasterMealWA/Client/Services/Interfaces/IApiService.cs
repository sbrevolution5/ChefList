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
        Task DeleteRecipeAsync(Recipe recipe);
        Task DeleteRecipeTypeAsync(RecipeType type);
        Task DeleteMealAsync(Meal meal);
        Task DeleteIngredientAsync(Ingredient ingredient);
        Task DeleteIngredientTypeAsync(IngredientType ingredientType);
        Task DeleteShoppingListAsync(ShoppingList shoppingList);
        Task DeleteSupplyAsync(Supply supply);
        Task GetRecipeAsync(Recipe recipe);
        Task GetRecipeTypeAsync(RecipeType type);
        Task GetMealAsync(Meal meal);
        Task GetIngredientAsync(Ingredient ingredient);
        Task GetIngredientTypeAsync(IngredientType ingredientType);
        Task GetShoppingListAsync(ShoppingList shoppingList);
        Task GetSupplyAsync(Supply supply);
        
    }
}
