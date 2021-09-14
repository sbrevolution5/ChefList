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
        public List<Recipe> ApplyFilter(List<Recipe> allRecipes, Filter filter, string userId)
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


            var nationalitySet = filter.NationalityChips.Select(r => (RecipeTag)r.Tag);
            if (nationalitySet.Any())
            {
                var selectedNationalities = nationalitySet.ToList();
                if (filter.HasAllNationalities)
                {
                    recipes = recipes.Where(r => selectedNationalities.All(i => r.Tags.Contains(i))).ToList();

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
            var proteinSet = filter.NationalityChips.Select(r => (RecipeTag)r.Tag);
            if (proteinSet.Any())
            {
                var selectedProteins = proteinSet.ToList();
                if (filter.HasAllProteins)
                {
                    recipes = recipes.Where(r => selectedProteins.All(p => r.Tags.Contains(p))).ToList();
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
            var typeSet = filter.NationalityChips.Select(r => (RecipeTag)r.Tag);
            if (typeSet.Any())
            {
                var selectedTypes = typeSet.ToList();
                if (filter.HasAllTypes)
                {
                    recipes = recipes.Where(r => selectedTypes.All(t => r.Tags.Contains(t))).ToList();
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
            if (filter.LowestRating > 1)
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