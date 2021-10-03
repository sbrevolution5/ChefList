using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models.Dtos
{
    class AddToShoppingDto
    {
        public ShoppingIngredient Ingredient { get; set; }
        public int ListId { get; set; }
    }
}
