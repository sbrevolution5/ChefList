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
        Task UpdateTagAsync(RecipeTag tag);
        Task UpdateIngredientTypeAsync(IngredientType ingredientType);
        Task UpdateShoppingListAsync(ListCreateDto dto);
        Task<DBImage> GetImageAsync(int id);
        Task<bool> UpdateImageAsync(MultipartFormDataContent content,int imageId);
        Task<int> UploadImageAsync(MultipartFormDataContent content);
        Task DeleteRecipeAsync(int id);
        Task<ShoppingList> GetShoppingListAsync(int id);
        Task<List<Meal>> GetMyMealsAsync();
        Task<List<IngredientType>> GetAllIngredientTypesAsync();
        Task<List<ShoppingList>> GetMyShoppingListsAsync();
    }
}
