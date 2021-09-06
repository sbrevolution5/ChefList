using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models.ViewModel
{
    public class RecipeViewModel
    {
        public int Id { get; set; }
        public List<QIngredient> Ingredients { get; set; }
        public List<Supply> Supplies { get; set; }
        public List<Step> Steps { get; set; }
        public string Name { get; set; }
        public int CookingTime { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public int TypeId { get; set; }
        public virtual RecipeTag Type { get; set; }
        
    }
}
