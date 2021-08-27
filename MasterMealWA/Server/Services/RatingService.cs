using MasterMealWA.Server.Data;
using MasterMealWA.Shared.Models;
using MasterMealWA.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Server.Services
{
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _context;

        public RatingService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<Rating>> GetRatingsByUserAsync(string userId)
        {
            var ratings = await _context.Rating.Where(r => r.ChefId == userId).ToListAsync();
            return ratings;
        }
    }
}
