using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models.ViewModel
{
    public class RecipeEditViewModel
    {
        public List<RecipeTag> Tags { get; set; }
        public Recipe Recipe { get; set; }
    }
}
