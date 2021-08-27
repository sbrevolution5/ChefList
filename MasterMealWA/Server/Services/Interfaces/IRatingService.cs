using MasterMealWA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Server.Services.Interfaces
{
    public interface IRatingService
    {
        public Task<List<Rating>> GetRatingsByUserAsync(string userId);
    }
}
