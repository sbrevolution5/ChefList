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
        public MudChip[] NationalityChips { get; set; }
        public bool HasAllNationalities { get; set; }
        public MudChip[] ProteinChips { get; set; }
        public bool HasAllProteins { get; set; }
        public MudChip[] TypeChips { get; set; }
        public bool HasAllTypes { get; set; }
        public int LowestRating { get; set; } = 0;
        public bool CookingTime { get; set; }
        public int MinCookingTime { get; set; } = 15;
        public int MaxCookingTime { get; set; } = 90;
    }
}
