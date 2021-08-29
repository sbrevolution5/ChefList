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

        public async Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            var list = await _http.GetFromJsonAsync<List<Ingredient>>("api/ingredients");
            return list;
        }

        public async Task<List<IngredientType>> GetAllIngredientTypesAsync()
        {
            var list = await _http.GetFromJsonAsync<List<IngredientType>>("api/ingredientTypes");
            return list;
        }

        public async Task<List<Meal>> GetAllMealsAsync()
        {
            var list = await _http.GetFromJsonAsync<List<Meal>>("api/meals");
            return list;
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            var list = await _http.GetFromJsonAsync<List<Recipe>>("api/recipes");
            return list;
        }

        public async Task<List<RecipeType>> GetAllRecipeTypesAsync()
        {
            var list = await _http.GetFromJsonAsync<List<RecipeType>>("api/recipeTypes");
            return list;
        }

        public async Task<List<ShoppingList>> GetAllShoppingListsAsync()
        {
            var list = await _http.GetFromJsonAsync<List<ShoppingList>>("api/shoppingLists");
            return list;
        }

        public async Task<List<Supply>> GetAllSuppliesAsync()
        {
            var list = await _http.GetFromJsonAsync<List<Supply>>("api/supplies");
            return list;
        }

        public async Task<Ingredient> GetIngredientAsync(int id)
        {
            var ing = await _http.GetFromJsonAsync<Ingredient>($"api/ingredients/{id}", _options);
            return ing;
        }

        public async Task<IngredientType> GetIngredientTypeAsync(int id)
        {
            var ingType = await _http.GetFromJsonAsync<IngredientType>($"api/IngredientTypes/{id}", _options);
            return ingType;
        }

        public async Task<Meal> GetMealAsync(int id)
        {
            var meal=await _http.GetFromJsonAsync<Meal>($"api/Meals/{id}", _options);
            return meal;
        }

        public async Task<Recipe> GetRecipeAsync(int id)
        {
            var recipe = await _http.GetFromJsonAsync<Recipe>($"api/recipes/{id}", _options);
            return recipe;
        }

        public async Task<RecipeType> GetRecipeTypeAsync(int id)
        {
            var type = await _http.GetFromJsonAsync<RecipeType>($"api/recipetypes/{id}", _options);
            return type;
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