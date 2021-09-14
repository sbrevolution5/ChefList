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
        public List<Recipe> ApplyFilter(List<Recipe> allRecipes, Filter filter,string userId)
        {
            List<Recipe> recipes = allRecipes;
            //MyRecipes
            if (filter.MyRecipes)
            {
                recipes = recipes.Where(r => r.AuthorId == userId).ToList();
            }
            //MySupplies
            if (filter.MySupplies)
            {
                //TODO: Supplies USER CURRENTLY HAS NO ABILITY TO DO THIS
                
            }
            //Nationality
            var selectedNationalities = filter.NationalityChips.Select(r => (RecipeTag)r.Tag).ToList();
            if (selectedNationalities.Count > 0)
            {
                if (filter.HasAllNationalities)
                {

                }
                else
                {

                }
            }
            //Protein
            var selectedProteins = filter.ProteinChips.Select(r => (RecipeTag)r.Tag).ToList();
            if (selectedProteins.Count > 0)
            {
                if (filter.HasAllProteins)
                {

                }
                else
                {

                }
            }
            //Type
            var selectedTypes = filter.TypeChips.Select(r => (RecipeTag)r.Tag).ToList();
            if (selectedTypes.Count > 0)
            {
                if (filter.HasAllTypes)
                {

                }
                else
                {

                }
            }
            //Rating
            //CookingTime
            return recipes;
        }
    }
}
