using MasterMealWA.Client.Models;
using MasterMealWA.Client.Services.Interfaces;
using MasterMealWA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Client.Services
{
    public class FilterService : IFilterService
    {
        public List<Recipe> ApplyFilter(List<Recipe> allRecipes, Filter filter)
        {
            //MyRecipes
            //MySupplies
            //Nationality
            //Protein
            //Type
            //Rating
            //CookingTime
        }
    }
}
