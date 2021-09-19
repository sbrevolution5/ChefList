using MasterMealWA.Server.Data;
using MasterMealWA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Server.Services
{
    public class ServingService
    {
        private readonly ApplicationDbContext _context;

        public ServingService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Recipe> ScaleRecipeAsync(int recipeId, int desiredServings)
        {
            var singleServe = ConvertRecipeToSingleServing();
            var correctServing = UpscaleServing()
            throw new NotImplementedException();
        }
    }
}
