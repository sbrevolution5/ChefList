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

            if (filter.NationalityList is not null)
            {



                if (filter.HasAllNationalities)
                {
                    recipes = recipes.Where(r => filter.NationalityList.All(p => r.Tags.Contains(p))).ToList();
                }
                else
                {
                    recipes = recipes.Where(r => r.Tags.Select(t => t.Id).Intersect(filter.NationalityList.Select(t => t.Id)).Any()).ToList();
                }


            }

            if (filter.ProteinList is not null)
            {



                if (filter.HasAllProteins)
                {
                    recipes = recipes.Where(r => filter.ProteinList.All(p => r.Tags.Contains(p))).ToList();
                }
                else
                {
                    recipes = recipes.Where(r => r.Tags.Select(t=>t.Id).Intersect(filter.ProteinList.Select(t=>t.Id)).Any()).ToList();
                }


            }

            //Type

            if (filter.TypeList is not null)
            {



                if (filter.HasAllTypes)
                {
                    recipes = recipes.Where(r => filter.TypeList.All(p => r.Tags.Contains(p))).ToList();
                }
                else
                {
                    recipes = recipes.Where(r => r.Tags.Select(t => t.Id).Intersect(filter.TypeList.Select(t => t.Id)).Any()).ToList();
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