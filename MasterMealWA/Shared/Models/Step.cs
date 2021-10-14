using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models
{
    public class Step
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        [Required]
        public int StepNumber { get; set; } = 1;
        [Required]
        public string Text { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}