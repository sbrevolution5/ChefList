using MasterMealWA.Client.Services.Interfaces;
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
            PropertyNamingPolicy = null,
            PropertyNameCaseInsensitive = true
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
        public async Task<List<Meal>> GetMyMealsAsync()
        {
            var list = await _http.GetFromJsonAsync<List<Meal>>("api/meals", _options);
            return list;
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
    }
}