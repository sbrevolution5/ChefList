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
                    List<Recipe> recipesWithTags = new();
                    foreach (var item in selectedNationalities)
                    {
                        recipesWithTags.AddRange(recipes.Where(r => r.Tags.Contains(item)));
                    }
                    recipes = recipesWithTags.Distinct().ToList();
                }
            }
            //Protein
            var selectedProteins = filter.ProteinChips.Select(r => (RecipeTag)r.Tag).ToList();
            if (selectedProteins.Count > 0)
            {
                if (filter.HasAllProteins)
                {
                    recipes = recipes.Where()
                }
                else
                {
                    List<Recipe> recipesWithTags = new();
                    foreach (var item in selectedProteins)
                    {
                        recipesWithTags.AddRange(recipes.Where(r => r.Tags.Contains(item)));
                    }
                    recipes = recipesWithTags.Distinct().ToList();
                }
            }
            //Type
            var selectedTypes = filter.TypeChips.Select(r => (RecipeTag)r.Tag).ToList();
            if (selectedTypes.Count > 0)
            {
                if (filter.HasAllTypes)
                {
                    //How do I do this!?
                }
                else
                {
                    List<Recipe> recipesWithTags = new();
                    foreach (var item in selectedTypes)
                    {
                        recipesWithTags.AddRange(recipes.Where(r => r.Tags.Contains(item)));
                    }
                    recipes = recipesWithTags.Distinct().ToList();
                }
            }
            //Rating
            if (filter.LowestRating>1)
            {
                recipes = recipes.Where(r => r.AvgRating >= filter.LowestRating).ToList();
            }
            //CookingTime
            if (filter.CookingTime)
            {
                recipes = recipes.Where(r => r.CookingTime >= filter.MinCookingTime && r.CookingTime <= filter.MaxCookingTime).ToList();
            }
            return recipes;
        }
    }
}
