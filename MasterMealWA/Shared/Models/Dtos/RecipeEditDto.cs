using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models.Dtos
{
    public class RecipeEditDto
    {
        public Recipe Recipe { get; set; }
        public List<RecipeTag> RecipeTags { get; set; }
    }
}
