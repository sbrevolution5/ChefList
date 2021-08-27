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
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;

        public CommentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetRecipeCommentsAsync(int RecipeId)
        {
            var comments = await _context.Comment.Where(c => c.RecipeId == RecipeId).ToListAsync();
            return comments;
        }

        public async Task<List<Comment>> GetUserCommentsAsync(string userId)
        {
            var comments = await _context.Comment.Where(c => c.ChefId == userId).ToListAsync();
            return comments;
        }
    }
}
