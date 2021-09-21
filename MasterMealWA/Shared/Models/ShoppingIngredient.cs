using MasterMealWA.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models
{
    public class ShoppingIngredient
    {
        public int Id { get; set; }
        public int IngredientTypeId { get; set; }
        public virtual IngredientType IngredientType{ get; set; }
        public int Quantity { get; set; }
        public string QuantityString { get; set; }
        public List<string> Notes { get; set; }
        public bool InPantry { get; set; }
        public bool InCart { get; set; }
    }
}
