using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models.Dtos
{
    public class RatingEditDto
    {
        public int RecipeId { get; set; }
        public string ChefId { get; set; }
        public int NewRating { get; set; }
    }
}
