using MasterMealWA.Shared.Models;
using System.Threading.Tasks;

namespace MasterMealWA.Server.Services.Interfaces
{
    public interface IServingService
    {
        Task<Recipe> ScaleRecipeAsync(int recipeId, int desiredServings);
    }
}