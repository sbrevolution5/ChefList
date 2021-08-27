using MasterMealWA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Server.Services.Interfaces
{
    public interface ICommentService
    {
        public Task<List<Comment>> GetRecipeCommentsAsync(int RecipeId);
        public Task<List<Comment>> GetUserCommentsAsync(string userId);
    }
}
