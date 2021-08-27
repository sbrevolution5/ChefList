using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models
{
    public class Step
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int StepNumber { get; set; }
        public string Text { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}