using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models
{
    public class Rating
    {
        public int Id { get; set; }
        [Required]
        [Range(1,5)]
        public int Stars { get; set; }
        public string ChefId { get; set; }
        public virtual Chef Chef { get; set; }
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
