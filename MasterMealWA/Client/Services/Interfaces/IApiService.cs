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
        Task CreateAsync<T>(string url, T content);
        Task UpdateAsync<T>(string url, T content);
        Task<TResult> GetAsync<TResult>(string url);
        Task<TResult> GetAnonAsync<TResult>(string url);
        Task DeleteAsync(string url);
        Task<Recipe> ScaleRecipeAsync(int recipeId, int desiredServings);
        Task<bool> CreateOrUpdateRatingAsync(int recipeId, string userId, int rating, bool isNew = true);
        Task<bool> CreateNewRatingAsync(int recipeId, string userId, int rating);
        Task<bool> UpdateRatingAsync(int recipeId, string userId, int rating);
        Task CreateNewIngredientTypeAsync(IngredientType ingredientType);
        Task CreateNewSupplyAsync(Supply supply);
        Task UpdateTagAsync(RecipeTag tag);
        Task UpdateIngredientAsync(Ingredient ingredient);
        Task UpdateIngredientTypeAsync(IngredientType ingredientType);
        Task UpdateShoppingListAsync(ListCreateDto dto);
        Task UpdateSupplyAsync(Supply supply);
        Task<DBImage> GetImageAsync(int id);
        Task<bool> UpdateImageAsync(MultipartFormDataContent content,int imageId);
        Task<int> UploadImageAsync(MultipartFormDataContent content);
        Task DeleteRecipeAsync(int id);
        Task DeleteTagAsync(int id);
        Task DeleteIngredientAsync(int id);
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
        Task<List<Meal>> GetMyMealsAsync();
        Task<List<Ingredient>> GetAllIngredientsAsync();
        Task<List<IngredientType>> GetAllIngredientTypesAsync();
        Task<List<ShoppingList>> GetMyShoppingListsAsync();
        Task<List<Supply>> GetAllSuppliesAsync();

        
    }
}
