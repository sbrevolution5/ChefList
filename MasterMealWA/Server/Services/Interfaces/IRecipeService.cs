using MasterMealWA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Server.Services.Interfaces
{
    public interface IRecipeService
    {
        public Task<Recipe> GetRecipeByIdAsync(int RecipeId);
        public Task<List<Recipe>> GetUserRecipesAsync(string userId);
        public Task<List<Recipe>> GetUserRecipesByTypeAsync(string userId,int typeId);
        public Task<List<Recipe>> GetRecipesByTypeAsync(int typeId);
        public Task<List<Recipe>> GetRecipesByRatingAsync(int minRating);
        public Task<List<Recipe>> GetRecipesByMaxCookingTimeAsync(int maxTime);
        public Task<List<Recipe>> GetRecipesByMinCookingTimeAsync(int minTime);
        public Task<List<Recipe>> GetUserFavoriteRecipesAsync(string UserId);
        public Task<List<Recipe>> GetRecipesByIngredientsAsync(List<Ingredient> ingredients);
        public Task<List<Recipe>> GetRecipesBySuppliesAsync(List<Supply> supplies);
        public Task<List<Recipe>> GetUserRecipesWithNoRating(string userId);
    }
}
