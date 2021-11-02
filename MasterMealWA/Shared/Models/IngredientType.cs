using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models
{
    public class IngredientType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ShoppingOnly { get; set; }
    }
}
