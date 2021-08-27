using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models
{
    public class ShoppingIngredient
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public string Measurement { get; set; }
        public List<string> Notes { get; set; }
        public bool InPantry { get; set; }

    }
}
