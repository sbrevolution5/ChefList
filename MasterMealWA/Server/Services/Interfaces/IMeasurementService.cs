using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterMealWA.Shared.Enums;
using MasterMealWA.Shared.Models;

namespace MasterMealWA.Server.Services.Interfaces
{
    public interface IMeasurementService
    {
        public ShoppingIngredient GetMeasurementForShoppingIngredient(ShoppingIngredient ingredient);
        public string DecodeVolumeMeasurement(int fracTSP);
        public int EncodeVolumeMeasurement(int wholeNumber, Fraction fraction, VolumeMeasurementUnit unit);
        public string DecodeMassMeasurement(int fracOz);
        public int EncodeMassMeasurement(int wholeNumber, Fraction fraction, MassMeasurementUnit unit);
        public double FractionToDouble(Fraction fraction);
        public Fraction DoubleToFraction(double input);
        public string FractionToString(Fraction fraction);
        int EncodeUnitMeasurement(int quantityNumber, Fraction fraction);
        string DecodeUnitMeasurement(int numberOfUnits);
    }
}
