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
        Task<List<Meal>> GetMyMealsAsync();
    }
}
