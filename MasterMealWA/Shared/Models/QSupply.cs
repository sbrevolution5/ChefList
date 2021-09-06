using MasterMealWA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models
{
    public class QSupply
    {
        public int Id { get; set; }
        public int SupplyId { get; set; }
        public int RecipeId { get; set; }
        public int Quantity { get; set; }
        public virtual Recipe recipe { get; set; }
        public virtual Supply Supply { get; set; }
    }
}
