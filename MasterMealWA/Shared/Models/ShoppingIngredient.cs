using MasterMealWA.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int? IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        public MeasurementType MeasurementType { get; set; }
        public MassMeasurementUnit? MassMeasurementUnit { get; set; }
        public VolumeMeasurementUnit? VolumeMeasurementUnit { get; set; }
        public Fraction Fraction { get; set; }
        public int QuantityNumber { get; set; }
        public string QuantityString { get; set; }
        public List<string> Notes { get; set; }
        public bool InPantry { get; set; }
        public bool InCart { get; set; }
        public bool UserString { get; set; }
    }
}
