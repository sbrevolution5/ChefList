using MasterMealWA.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models
{
    public class RecipeTag
    {
        public bool Equals(RecipeTag x, RecipeTag y)
        {
            // TODO - Add null handling.
            return x.Id == y.Id;
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public CategoryType Category { get; set; }
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
