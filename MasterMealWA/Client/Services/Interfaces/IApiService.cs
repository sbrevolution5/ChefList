using MasterMealWA.Shared.Models;
using MasterMealWA.Shared.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MasterMealWA.Client.Services.Interfaces
{
    public interface IApiService
    {
        Task CreateNewRecipeAsync(Recipe recipe);
        Task<bool> CreateNewRatingAsync(int recipeId, string userId, int rating);
        Task CreateNewTagAsync(RecipeTag type);
        Task CreateNewMealAsync(Meal meal);
        Task CreateNewIngredientAsync(Ingredient ingredient);
        Task CreateNewIngredientTypeAsync(IngredientType ingredientType);
        Task CreateNewShoppingListAsync(ListCreateDto dto);
        Task CreateNewSupplyAsync(Supply supply);
        Task UpdateRecipeAsync(RecipeEditDto recipe);
        Task UpdateTagAsync(RecipeTag tag);
        Task UpdateMealAsync(Meal meal);
        Task UpdateIngredientAsync(Ingredient ingredient);
        Task UpdateIngredientTypeAsync(IngredientType ingredientType);
        Task UpdateShoppingListAsync(ListCreateDto dto);
        Task UpdateSupplyAsync(Supply supply);
        Task<DBImage> GetImageAsync(int id);
        Task<bool> UpdateImageAsync(MultipartFormDataContent content,int imageId);
        Task<int> UploadImageAsync(MultipartFormDataContent content);
        Task DeleteRecipeAsync(int id);
        Task DeleteTagAsync(int id);
        Task DeleteMealAsync(int id);
        Task DeleteIngredientAsync(int id);
        Task DeleteShoppingListAsync(int id);
        Task DeleteSupplyAsync(int id);
        Task<Recipe> GetRecipeAsync(int id,bool auth);
        Task<RecipeTag> GetRecipeTypeAsync(int id);
        Task<Meal> GetMealAsync(int id);
        Task<Ingredient> GetIngredientAsync(int id);
        Task<IngredientType> GetIngredientTypeAsync(int id);
        Task<ShoppingList> GetShoppingListAsync(int id);
        Task<Supply> GetSupplyAsync(int id);
        Task<List<Recipe>> GetAllRecipesAsync(bool auth);
        Task<List<Recipe>> GetMyRecipesAsync();
        Task<List<RecipeTag>> GetAllTagsAsync();
        Task<List<Meal>> GetMyMealsAsync();
        Task<List<Ingredient>> GetAllIngredientsAsync();
        Task<List<IngredientType>> GetAllIngredientTypesAsync();
        Task<List<ShoppingList>> GetMyShoppingListsAsync();
        Task<List<Supply>> GetAllSuppliesAsync();

        
    }
}
