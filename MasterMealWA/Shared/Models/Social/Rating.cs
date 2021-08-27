using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        public string ChefId { get; set; }
        public virtual Chef Chef { get; set; }
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
