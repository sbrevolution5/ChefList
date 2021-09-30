using MasterMealWA.Shared.Models;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Client.Models
{
    public class Filter
    {
        public bool MyRecipes { get; set; }
        public bool MySupplies { get; set; }
        public List<Supply> SupplyList { get; set; }
        public List<RecipeTag> NationalityList { get; set; }
        public bool HasAllNationalities { get; set; }
        public List<RecipeTag> ProteinList { get; set; } 
        public bool HasAllProteins { get; set; }
        public List<RecipeTag> TypeList { get; set; }
        public bool HasAllTypes { get; set; }
        public int LowestRating { get; set; } = 0;
        public bool CookingTime { get; set; }
        public int MinCookingTime { get; set; } = 15;
        public int MaxCookingTime { get; set; } = 90;
        public bool UseProteins { get; set; }
        public bool UseNationalities { get; set; }
        public bool UseType { get; set; }
    }
}
