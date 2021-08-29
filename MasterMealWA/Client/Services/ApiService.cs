using MasterMealWA.Client.Services.Interfaces;
using MasterMealWA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MasterMealWA.Client.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _options = new()
        {
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
            PropertyNamingPolicy = null
        };
        public ApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task CreateNewIngredientAsync(Ingredient ingredient)
        {
            await _http.PostAsJsonAsync("api/ingredients", ingredient);
        }

        public async Task CreateNewIngredientTypeAsync(IngredientType ingredientType)
        {
            await _http.PostAsJsonAsync("api/ingredientTypes", ingredientType);
        }

        public async Task CreateNewMealAsync(Meal meal)
        {
            await _http.PostAsJsonAsync("api/meals", meal);
        }

        public async Task CreateNewRecipeAsync(Recipe recipe)
        {
            await _http.PostAsJsonAsync("api/recipes", recipe);
        }

        public async Task CreateNewRecipeTypeAsync(RecipeType type)
        {
            await _http.PostAsJsonAsync("api/recipeTypes", type);
        }

        public async Task CreateNewShoppingListAsync(ShoppingList shoppingList)
        {
            await _http.PostAsJsonAsync("api/ShoppingLists", shoppingList);
        }

        public Task CreateNewSupplyAsync(Supply supply)
        {
            throw new NotImplementedException();
        }

        public Task DeleteIngredientAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteIngredientTypeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMealAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRecipeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRecipeTypeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteShoppingListAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSupplyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetIngredientAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetIngredientTypeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetMealAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetRecipeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetRecipeTypeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetShoppingListAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetSupplyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateIngredientAsync(Ingredient ingredient)
        {
            throw new NotImplementedException();
        }

        public Task UpdateIngredientTypeAsync(IngredientType ingredientType)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMealAsync(Meal meal)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRecipeAsync(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRecipeTypeAsync(RecipeType type)
        {
            throw new NotImplementedException();
        }

        public Task UpdateShoppingListAsync(ShoppingList shoppingList)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSupplyAsync(Supply supply)
        {
            throw new NotImplementedException();
        }
    }
}