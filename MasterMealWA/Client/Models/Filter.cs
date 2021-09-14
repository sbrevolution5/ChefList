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
        public bool HasAllNationality { get; set; }
        public MudChip[] ProteinChips { get; set; }
        public bool HasAllProtein { get; set; }
        public MudChip[] TypeChips { get; set; }
        public bool HasAllType { get; set; }
        public int MaxRating { get; set; } = 5;
        public int MinRating { get; set; } = 1;
        public bool CookingTime { get; set; }
        public int MinCookingTime { get; set; }
        public int MaxCookingTime { get; set; }
    }
}
