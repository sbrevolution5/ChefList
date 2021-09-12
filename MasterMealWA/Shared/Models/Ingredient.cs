using MasterMealWA.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int TypeId { get; set; }
        public virtual IngredientType Type { get; set; }
        [Required]
        public MeasurementType MeasurementType { get; set; }
    }
}
