using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models.Dtos
{
    public class RecipeEditDto
    {
        public int Id { get; set; }
        public Recipe Recipe { get; set; }
        public List<QIngredient> IngredientsToRemove { get; set; }
        public List<QSupply> SuppliesToRemove { get; set; }
        public List<Step> StepsToRemove { get; set; }
        public List<RecipeTag> RecipeTags { get; set; }
    }
}
