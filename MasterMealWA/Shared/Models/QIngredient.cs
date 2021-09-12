using MasterMealWA.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Models
{
    public class QIngredient
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        public string Notes { get; set; } = "";
        public string Quantity { get; set; }
        [Required]
        public MeasurementType MeasurementType { get; set; }
        public MassMeasurementUnit? MassMeasurementUnit { get; set; }
        public VolumeMeasurementUnit? VolumeMeasurementUnit { get; set; }
        public Fraction Fraction { get; set; }
        public int QuantityNumber { get; set; }
        public int NumberOfUnits { get; set; }
        [NotMapped]
        public string ShoppingNotes
        {
            get
            {
                return Quantity + " " + Notes;
            }
        }
    }
}
