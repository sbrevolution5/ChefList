﻿using MasterMealWA.Client.Services.Interfaces;
using MasterMealWA.Shared.Models;
using MasterMealWA.Shared.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MasterMealWA.Client.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _http;
        private readonly IHttpClientFactory _clientFactory;

        private readonly JsonSerializerOptions _options = new()
        {
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
            PropertyNamingPolicy = null
        };
        public ApiService(HttpClient http, IHttpClientFactory clientFactory)
        {
            _http = http;
            _clientFactory = clientFactory;
        }
        public async Task CreateAsync<T>(string url, T content)
        {
            await _http.PostAsJsonAsync<T>(url, content);
        }
        public async Task UpdateAsync<T>(string url, T content)
        {
            await _http.PutAsJsonAsync<T>(url, content);
        }
        public async Task<TResult> GetAsync<TResult>(string url)
        {
            var result = await _http.GetFromJsonAsync<TResult>(url,_options);
            return result;
        }
        public async Task<TResult> GetAnonAsync<TResult>(string url)
        {
            var client = _clientFactory.CreateClient("MasterMealWA.NonAuthServerAPI");
            var result = await client.GetFromJsonAsync<TResult>(url,_options);
            return result;
        }
        public async Task DeleteAsync(string url)
        {
            await _http.DeleteAsync(url);
        }
        public async Task CreateNewIngredientTypeAsync(IngredientType ingredientType)
        {
            await _http.PostAsJsonAsync("api/ingredientTypes", ingredientType);
        }
        public async Task DeleteIngredientAsync(int id)
        {
            await _http.DeleteAsync($"api/ingredients/{id}");
        }

        public async Task DeleteIngredientTypeAsync(int id)
        {
            await _http.DeleteAsync($"api/ingredienttypes/{id}");
        }
        public async Task DeleteRecipeAsync(int id)
        {
            await _http.DeleteAsync($"api/recipes/{id}");
        }

        public async Task DeleteTagAsync(int id)
        {
            await _http.DeleteAsync($"api/recipetags/{id}");
        }
        public async Task DeleteSupplyAsync(int id)
        {
            await _http.DeleteAsync($"api/supplies/{id}");
        }
        public async Task<List<IngredientType>> GetAllIngredientTypesAsync()
        {
            var list = await _http.GetFromJsonAsync<List<IngredientType>>("api/ingredientTypes", _options);
            return list;
        }

        public async Task<List<Meal>> GetMyMealsAsync()
        {
            var list = await _http.GetFromJsonAsync<List<Meal>>("api/meals", _options);
            return list;
        }
        public async Task<List<ShoppingList>> GetMyShoppingListsAsync()
        {
            var list = await _http.GetFromJsonAsync<List<ShoppingList>>("api/shoppingLists", _options);
            return list;
        }
        public async Task<ShoppingList> GetShoppingListAsync(int id)
        {
            var list = await _http.GetFromJsonAsync<ShoppingList>($"api/shoppingLists/{id}", _options);
            return list;
        }

        public async Task<Supply> GetSupplyAsync(int id)
        {
            var supply = await _http.GetFromJsonAsync<Supply>($"api/supplies/{id}", _options);
            return supply;
        }
        public async Task UpdateIngredientTypeAsync(IngredientType ingredientType)
        {
            await _http.PutAsJsonAsync("api/ingredienttypes", ingredientType);

        }
        public async Task UpdateTagAsync(RecipeTag tag)
        {
            await _http.PutAsJsonAsync($"api/recipetags/{tag.Id}", tag);

        }

        public async Task UpdateShoppingListAsync(ListCreateDto dto)
        {
            await _http.PutAsJsonAsync("api/shoppinglists", dto);
        }
        public async Task<List<Recipe>> GetMyRecipesAsync()
        {
            try
            {

                var result = await _http.GetFromJsonAsync<List<Recipe>>("api/recipes/myrecipes", _options);
                return result;
            }
            catch (Exception)
            {

                return new List<Recipe>();
            }
        }

        public async Task<int> UploadImageAsync(MultipartFormDataContent content)
        {
            try
            {
                var result = await _http.PostAsync("api/dbimages", content);
                return Convert.ToInt32(await result.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> UpdateImageAsync(MultipartFormDataContent content, int imageId)
        {
            try
            {
                var result = await _http.PutAsync($"api/dbimages/{imageId}", content);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<DBImage> GetImageAsync(int id)
        {
            DBImage result = await _http.GetFromJsonAsync<DBImage>($"api/dbimages/{id}");
            return result;
        }

        public async Task<bool> CreateNewRatingAsync(int recipeId, string userId, int rating)
        {
            try
            {
                await _http.PostAsJsonAsync($"api/ratings", new Rating()
                {
                    Stars = rating,
                    ChefId = userId,
                    RecipeId = recipeId
                });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CreateOrUpdateRatingAsync(int recipeId, string userId, int rating, bool isNew = true)
        {
            bool res;
            if (isNew)
            {
                res = await CreateNewRatingAsync(recipeId, userId, rating);
            }
            else
            {
                res = await UpdateRatingAsync(recipeId, userId, rating);
            }
            return res;
        }

        public async Task<bool> UpdateRatingAsync(int recipeId, string userId, int rating)
        {
            RatingEditDto dto = new()
            {
                RecipeId = recipeId,
                ChefId = userId,
                NewRating = rating
            };
            try
            {
                await _http.PutAsJsonAsync($"api/ratings/", dto);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Recipe> ScaleRecipeAsync(int recipeId, int desiredServings)
        {
            try
            {
                var options = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };
                var recipe = await _http.GetFromJsonAsync<Recipe>($"api/recipes/{recipeId}/scale/{desiredServings}", options);

                return recipe;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}