using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string ChefId { get; set; }
        public virtual Chef Chef { get; set; }
        public List<ShoppingIngredient> ShoppingIngredients { get; set; }
    }
}
