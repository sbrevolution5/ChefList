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

        public async Task CreateNewSupplyAsync(Supply supply)
        {
            await _http.PostAsJsonAsync("api/Supplies", supply);

        }

        public async Task DeleteIngredientAsync(int id)
        {
            await _http.DeleteAsync($"api/ingredients/{id}");
        }

        public async Task DeleteIngredientTypeAsync(int id)
        {
            await _http.DeleteAsync($"api/ingredienttypes/{id}");
        }

        public async Task DeleteMealAsync(int id)
        {
            await _http.DeleteAsync($"api/meals/{id}");
        }

        public async Task DeleteRecipeAsync(int id)
        {
            await _http.DeleteAsync($"api/recipes/{id}");
        }

        public async Task DeleteRecipeTypeAsync(int id)
        {
            await _http.DeleteAsync($"api/recipetypes/{id}");
        }

        public async Task DeleteShoppingListAsync(int id)
        {
            await _http.DeleteAsync($"api/shoppinglists/{id}");
        }

        public async Task DeleteSupplyAsync(int id)
        {
            await _http.DeleteAsync($"api/supplies/{id}");
        }

        public async Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            var list = await _http.GetFromJsonAsync<List<Ingredient>>("api/ingredients",_options);
            return list;
        }

        public async Task<List<IngredientType>> GetAllIngredientTypesAsync()
        {
            var list = await _http.GetFromJsonAsync<List<IngredientType>>("api/ingredientTypes", _options);
            return list;
        }

        public async Task<List<Meal>> GetAllMealsAsync()
        {
            var list = await _http.GetFromJsonAsync<List<Meal>>("api/meals",_options);
            return list;
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            var list = await _http.GetFromJsonAsync<List<Recipe>>("api/recipes", _options);
            return list;
        }

        public async Task<List<RecipeType>> GetAllRecipeTypesAsync()
        {
            var list = await _http.GetFromJsonAsync<List<RecipeType>>("api/recipeTypes", _options);
            return list;
        }

        public async Task<List<ShoppingList>> GetAllShoppingListsAsync()
        {
            var list = await _http.GetFromJsonAsync<List<ShoppingList>>("api/shoppingLists", _options);
            return list;
        }

        public async Task<List<Supply>> GetAllSuppliesAsync()
        {
            var list = await _http.GetFromJsonAsync<List<Supply>>("api/supplies", _options);
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

        public async Task UpdateIngredientAsync(Ingredient ingredient)
        {
            await _http.PutAsJsonAsync("api/ingredients", ingredient);

        }

        public async Task UpdateIngredientTypeAsync(IngredientType ingredientType)
        {
            await _http.PutAsJsonAsync("api/ingredienttypes", ingredientType);

        }

        public async Task UpdateMealAsync(Meal meal)
        {
            await _http.PutAsJsonAsync($"api/meals/{meal.Id}", meal);
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            await _http.PutAsJsonAsync($"api/recipes/{recipe.Id}", recipe,_options);

        }

        public async Task UpdateRecipeTypeAsync(RecipeType type)
        {
            await _http.PutAsJsonAsync("api/recipetypes", type);

        }

        public async Task UpdateShoppingListAsync(ShoppingList shoppingList)
        {
            await _http.PutAsJsonAsync("api/shoppinglists", shoppingList);

        }

        public async Task UpdateSupplyAsync(Supply supply)
        {
            await _http.PutAsJsonAsync("api/supplies", supply);

        }
    }
}