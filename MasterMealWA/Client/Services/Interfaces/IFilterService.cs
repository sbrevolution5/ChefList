using MasterMealWA.Client.Models;
using MasterMealWA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Client.Services.Interfaces
{
    public interface IFilterService
    {
        public List<Recipe> ApplyFilter(List<Recipe> allRecipes, Filter filter,string userId);
    }
}
