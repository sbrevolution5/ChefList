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

            if (filter.NationalityChips is not null)
            {

            var nationalitySet = filter.NationalityChips.Select(r => (RecipeTag)r.Tag);
            if (nationalitySet.Any())
            {
                
                if (filter.HasAllNationalities)
                {
                    recipes = recipes.Where(r => nationalitySet.All(i => r.Tags.Contains(i))).ToList();

                }
                else
                {
                    List<Recipe> recipesWithTags = new();
                    foreach (var item in nationalitySet)
                    {
                        recipesWithTags.AddRange(recipes.Where(r => r.Tags.Contains(item)));
                    }
                    recipes = recipesWithTags.Distinct().ToList();
                }
            }


            }
            if (filter.ProteinChips is not null)
            {

            //Protein
            var proteinSet = filter.ProteinChips.Select(r => (RecipeTag)r.Tag);
            if (proteinSet.Any())
            {
                
                if (filter.HasAllProteins)
                {
                    recipes = recipes.Where(r => proteinSet.All(p => r.Tags.Contains(p))).ToList();
                }
                else
                {
                    List<Recipe> recipesWithTags = new();
                    foreach (var item in proteinSet)
                    {
                        recipesWithTags.AddRange(recipes.Where(r => r.Tags.Contains(item)));
                    }
                    recipes = recipesWithTags.Distinct().ToList();
                }
            }

            }

            //Type
            var typeSet = filter.TypeChips.Select(r => (RecipeTag)r.Tag);
            if (typeSet.Any())
            {
                if (filter.HasAllTypes)
                {
                    recipes = recipes.Where(r => typeSet.All(t => r.Tags.Contains(t))).ToList();
                }
                else
                {
                    List<Recipe> recipesWithTags = new();
                    foreach (var item in typeSet)
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