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
            var result = await _http.GetFromJsonAsync<TResult>(url, _options);
            return result;
        }
        public async Task<TResult> GetAnonAsync<TResult>(string url)
        {
            var client = _clientFactory.CreateClient("MasterMealWA.NonAuthServerAPI");
            var result = await client.GetFromJsonAsync<TResult>(url, _options);
            return result;
        }
        public async Task DeleteAsync(string url)
        {
            await _http.DeleteAsync(url);
        }

        public async Task<TResult> CreateAndRetrieveAsync<T, TResult>(string url, T content)
        {
            //Need to send the data to convert, and get a result
            var result = await _http.PostAsync(url,content);
            if (result.IsSuccessStatusCode)
            {
            return await result.Content.ReadFromJsonAsync<TResult>();

            }
            else
            {
                string errMsg = await result.Content.ReadAsStringAsync();
                Console.WriteLine(errMsg);
                throw new Exception(errMsg);
            }

        }
    }
}