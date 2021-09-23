using MasterMealWA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Client.Models
{
    public class ScaledIngredient
    {
        public QIngredient old { get; set; }
        public QIngredient scaled { get; set; }
    }
}
