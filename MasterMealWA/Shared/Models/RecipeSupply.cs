using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models
{
    public class RecipeSupply
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public int SupplyId { get; set; }
        public Supply Supply { get; set; }
    }
}
