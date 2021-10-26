using MasterMealWA.Shared.Enums;
using MasterMealWA.Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterMealWA.Shared.Models;

namespace MasterMealWA.Server.Services
{
    public class MeasurementService : IMeasurementService
    {
        public ShoppingIngredient GetMeasurementForShoppingIngredient(ShoppingIngredient ingredient) 
        {
            if (ingredient.MeasurementType == MeasurementType.Volume)
            {
                ingredient.QuantityString = $"{DecodeVolumeMeasurement(ingredient.Quantity)} {ingredient.Ingredient.Name}";
                ingredient = ApplyVolumeMeasurement(ingredient);
            }
            else if (ingredient.MeasurementType == MeasurementType.Mass)
            {
                ingredient.QuantityString = $"{DecodeMassMeasurement(ingredient.Quantity)} {ingredient.Ingredient.Name}";
                ingredient = ApplyMassMeasurement(ingredient);

            }
            else if (ingredient.MeasurementType == MeasurementType.Count)
            {
                ingredient.QuantityString = $"{DecodeUnitMeasurement(ingredient.Quantity)} {ingredient.Ingredient.Name}";
                ingredient = ApplyUnitMeasurement(ingredient);
            }
            return ingredient;
        }

        private ShoppingIngredient ApplyVolumeMeasurement(ShoppingIngredient ingredient)
        {
            VolumeMeasurementUnit unit;
            int conversionFactor;
            int fracTSP = ingredient.Quantity;
            if (fracTSP >= 4 * 2 * 2 * 8 * 2 * 3 * 24)
            {
                unit = VolumeMeasurementUnit.Gallon;
                conversionFactor = 4 * 2 * 2 * 8 * 2 * 3 * 24;
            }
            else if (fracTSP >= 2 * 2 * 8 * 2 * 3 * 24)
            {
                unit = VolumeMeasurementUnit.Quart;
                conversionFactor = 2 * 2 * 8 * 2 * 3 * 24;
            }
            else if (fracTSP >= 2 * 8 * 2 * 3 * 24)
            {
                unit = VolumeMeasurementUnit.Pint;
                conversionFactor = 2 * 8 * 2 * 3 * 24;
            }
            else if (fracTSP >= 4 * 3 * 24)
            {
                unit = VolumeMeasurementUnit.Cup;
                conversionFactor = 8 * 2 * 3 * 24;

            }
            else if (fracTSP >= 24 * 3 || fracTSP == 18 || fracTSP == 36 || fracTSP == 54)
            {
                unit = VolumeMeasurementUnit.Tablespoon;
                conversionFactor = 3 * 24;
            }
            else //Must be Teaspoon or less
            {
                unit = VolumeMeasurementUnit.Teaspoon;
                conversionFactor = 24;

            }
            //Get whats left from remainder
            ingredient.VolumeMeasurementUnit = unit;
            int remainder = fracTSP % conversionFactor;
            if (remainder > 0)
            {
                //round down to whole unit
                fracTSP -= remainder;
                //if fraction > .75, just round to next whole number.
                if (remainder / conversionFactor > 0.75)
                {
                    fracTSP += conversionFactor;
                    ingredient.QuantityNumber = fracTSP / conversionFactor;
                }
                else
                {
                    //Round up to fraction
                    ingredient.Fraction = DoubleToFraction(remainder/(double)conversionFactor);
                    ingredient.QuantityNumber = fracTSP / conversionFactor;
                }

            }
            else
            {
                ingredient.QuantityNumber = fracTSP / conversionFactor;

            }
            return ingredient;
        }

        private ShoppingIngredient ApplyMassMeasurement(ShoppingIngredient ingredient)
        {
            MassMeasurementUnit unit;
            int conversionFactor;
            int fracOz = ingredient.Quantity;
            if (fracOz >= 24 * 16 || fracOz == 24 * 8 || fracOz == 24 * 4 || fracOz == 24 * 12)
            {
                unit = MassMeasurementUnit.pound;
                conversionFactor = 24 * 16;
            }
            else //Must be Ounce or less
            {
                unit = MassMeasurementUnit.ounce;
                conversionFactor = 24;

            }
            //Get whats left from remainder
            ingredient.MassMeasurementUnit = unit;
            int remainder = fracOz % conversionFactor;
            if (remainder > 0)
            {
                //round down to whole unit
                fracOz -= remainder;
                //if fraction > .75, just round to next whole number.
                if (remainder / conversionFactor > 0.75)
                {
                    fracOz += conversionFactor;
                    ingredient.QuantityNumber = fracOz / conversionFactor;
                }
                else
                {
                    //Round up to fraction
                    ingredient.Fraction = DoubleToFraction(remainder / (double)conversionFactor);
                    ingredient.QuantityNumber = fracOz / conversionFactor;
                }

            }
            else
            {
                ingredient.QuantityNumber = fracOz / conversionFactor;

            }
            return ingredient;
        }

        private ShoppingIngredient ApplyUnitMeasurement(ShoppingIngredient ingredient)
        {
            var numberOfUnits = ingredient.Quantity;
            int remainder = numberOfUnits % 24;
            ingredient.QuantityNumber= (numberOfUnits - remainder) / 24;
            double fractionDec = remainder / 24d;
            ingredient.Fraction = DoubleToFraction(fractionDec);
            return ingredient;
        }

        public string DecodeVolumeMeasurement(int fracTSP)
        {
            VolumeMeasurementUnit unit;
            int conversionFactor;
            string unitString;
            float ozConversion = (float)fracTSP / (24f * 3f * 2f);
            if (fracTSP >= 4 * 2 * 2 * 8 * 2 * 3 * 24)
            {
                unitString = "Gallon";
                unit = VolumeMeasurementUnit.Gallon;
                conversionFactor = 4 * 2 * 2 * 8 * 2 * 3 * 24;
            }
            else if (fracTSP >= 2 * 2 * 8 * 2 * 3 * 24)
            {
                unitString = "Quart";
                unit = VolumeMeasurementUnit.Quart;
                conversionFactor = 2 * 2 * 8 * 2 * 3 * 24;
            }
            else if (fracTSP >= 2 * 8 * 2 * 3 * 24)
            {
                unitString = "Pint";
                unit = VolumeMeasurementUnit.Pint;
                conversionFactor = 2 * 8 * 2 * 3 * 24;
            }
            else if (fracTSP >= 4 * 3 * 24)
            {
                unitString = "Cup";
                unit = VolumeMeasurementUnit.Cup;
                conversionFactor = 8 * 2 * 3 * 24;

            }
            //else if (fracTSP >= 24 * 3 * 2)
            //{
            //    unitString = "Ounce";
            //    unit = VolumeMeasurementUnit.Ounce;
            //    conversionFactor = 2 * 3 * 24;
            //}
            else if (fracTSP >= 24 * 3 || fracTSP == 18 || fracTSP == 36 || fracTSP == 54)
            {
                unitString = "Tablespoon";
                unit = VolumeMeasurementUnit.Tablespoon;
                conversionFactor = 3 * 24;
            }
            else if (fracTSP < 6)
            {
                //Only used if we end up with < 1/4 tsp due to serving size conversion!!!!
                unitString = "A Dash/Pinch (less than 1/4 TSP) ";
                var dashMeasure = unitString + "(" + ozConversion + " oz.)";
                return dashMeasure;
            }
            else //Must be Teaspoon or less
            {
                unitString = "Teaspoon";
                unit = VolumeMeasurementUnit.Teaspoon;
                conversionFactor = 24;

            }
            //Get whats left from remainder
            string measurement = "";
            int remainder = fracTSP % conversionFactor;
            int howMany;
            if (remainder > 0)
            {
                //Add s to unit
                unitString += "s";
                //round down to whole unit
                fracTSP -= remainder;
                //if fraction > .75, just round to next whole number.
                if (remainder / conversionFactor > 0.75)
                {
                    fracTSP += conversionFactor;
                    howMany = fracTSP / conversionFactor;
                    measurement = howMany + " " + unitString + "(" + ozConversion + " oz.)";
                }
                else
                {
                    //Round up to fraction
                    string fraction = RemainderToFraction(remainder, unit);
                    howMany = fracTSP / conversionFactor;
                    if (howMany == 0)
                    {
                        measurement = fraction + " " + unitString + "(" + ozConversion + " oz.)";

                    }
                    else
                    {

                        measurement = howMany + " " + fraction + " " + unitString + "(" + ozConversion + " oz.)";
                    }
                }

            }
            else
            {
                howMany = fracTSP / conversionFactor;
                //add S to plurals
                if (howMany != 1)
                {
                    unitString += "s";
                }
                measurement = howMany + " " + unitString + "(" + ozConversion + " oz.)";

            }
            return measurement;
        }

        private string RemainderToFraction(int remainder, MassMeasurementUnit unit)
        {
            if (unit == MassMeasurementUnit.ounce)
            {
                return FractionToString(DoubleToFraction(remainder / 24d));
            }
            else
            {
                double fractionDouble = remainder / (24d*16d);
                return FractionToString(DoubleToFraction(fractionDouble));

            }
        }
        private string RemainderToFraction(int remainder, VolumeMeasurementUnit unit)
        {
            //TODO What if 1/3 And 3/4 are combined, or 1/3 1/2???
            if (unit == VolumeMeasurementUnit.Teaspoon)
            {
                return FractionToString(DoubleToFraction(remainder / 24d));
            }
            else if (unit == VolumeMeasurementUnit.Tablespoon)
            {
                var fraction = DoubleToFraction(remainder / (24d * 3d));
                return FractionToString(fraction);

            }
            else if (unit == VolumeMeasurementUnit.Ounce)
            {
                Fraction fraction = DoubleToFraction(remainder / (24d * 3d * 2d));
                return FractionToString(fraction);

            }
            else if (unit == VolumeMeasurementUnit.Cup)
            {
                Fraction fraction = DoubleToFraction(remainder / (24d * 3d * 2d * 8d));
                return FractionToString(fraction);

            }
            else if (unit == VolumeMeasurementUnit.Pint)
            {
                Fraction fraction = DoubleToFraction(remainder / (24d * 3d * 2d * 8d * 2d));
                return FractionToString(fraction);
            }
            else if (unit == VolumeMeasurementUnit.Quart)
            {
                Fraction fraction = DoubleToFraction(remainder / (24d * 3d * 2d * 8d * 2d * 2d));
                return FractionToString(fraction);

            }
            else if (unit == VolumeMeasurementUnit.Gallon)
            {
                Fraction fraction = DoubleToFraction(remainder / (24d * 3d * 2d * 8d * 2d * 2d * 4d));
                return FractionToString(fraction);

            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// Rounds up to ensure you have enough ingredient
        /// </summary>
        /// <param name="input">double to be converted</param>
        /// <returns></returns>
        public Fraction DoubleToFraction(double input)
        {
            if (input == 0)
            {
                return Fraction.NoFraction;
            }
            else if (input <= .25)
            {
                return Fraction.OneQuarter;
            }
            else if (input <= .34)
            {
                return Fraction.OneThird;
            }
            else if (input <= .5)
            {
                return Fraction.Half;
            }
            else if (input <= .68)
            {
                return Fraction.TwoThirds;
            }
            else
            {
                return Fraction.ThreeQuarters;
            }
        }
        /// <summary>
        /// Converts a number of 1/2 ounce into real measurements
        /// </summary>
        /// <param name="fracOz"></param>
        /// <returns></returns>
        public string DecodeMassMeasurement(int fracOz)
        {
            string unitString;
            MassMeasurementUnit unit;
            int conversionFactor;
            if (fracOz >= 24*16 || fracOz == 24*8 || fracOz == 24*4 || fracOz == 24*12)
            {
                unitString = "Pound";
                unit = MassMeasurementUnit.pound;
                conversionFactor = 24*16;
            }
            else //Must be Ounce or less
            {
                unitString = "Ounce";
                unit = MassMeasurementUnit.ounce;
                conversionFactor = 24;

            }
            float ozConversion = (float)fracOz / 24f;
            //Get whats left from remainder
            string measurement;
            int remainder = fracOz % conversionFactor;
            int howMany;
            if (remainder > 0d)
            {
                //Add s to unit
                unitString += "s";
                //round down to whole unit
                fracOz -= remainder;
                if (remainder / conversionFactor > 0.75d)
                {
                    fracOz += conversionFactor;
                    howMany = fracOz / conversionFactor;
                    if (unitString != "Ounces")
                    {
                        measurement = howMany + " " + unitString + "(" + ozConversion + " oz.)";
                    }
                    else
                    {
                        measurement = howMany + " " + unitString;
                    }
                }
                else
                {

                    //Round up to fraction
                    string fraction = RemainderToFraction(remainder, unit);
                    howMany = fracOz / conversionFactor;
                    if (howMany == 0)
                    {
                        if (unitString != "Ounces")
                        {
                            measurement = fraction + " " + unitString + "(" + ozConversion + " oz.)";
                        }
                        else
                        {
                            measurement = fraction + " " + unitString;
                        }
                    }
                    else
                    {

                        if (unitString != "Ounces")
                        {
                            measurement = howMany + " " + fraction + " " + unitString + "(" + ozConversion + " oz.)";
                        }
                        else
                        {
                            measurement = howMany + " " + fraction + " " + unitString;
                        }
                    }
                }

            }
            else //no remainder
            {
                howMany = fracOz / conversionFactor;
                //add S to plurals
                if (howMany != 1)
                {
                    unitString += "s";
                }
                if (unitString != "Ounces")
                {
                    measurement = howMany + " " + unitString + "(" + ozConversion + " oz.)";
                }
                else
                {
                    measurement = howMany + " " + unitString;
                }

            }
            return measurement;
        }

        public int EncodeVolumeMeasurement(int wholeNumber, Fraction fraction, VolumeMeasurementUnit unit)
        {
            int conversionFactor;
            if (unit == VolumeMeasurementUnit.Teaspoon)
            {
                conversionFactor = 24;
            }
            else if (unit == VolumeMeasurementUnit.Tablespoon)
            {
                conversionFactor = 3 * 24;
            }
            else if (unit == VolumeMeasurementUnit.Ounce)
            {
                conversionFactor = 2 * 3 * 24;
            }
            else if (unit == VolumeMeasurementUnit.Cup)
            {
                conversionFactor = 8 * 2 * 3 * 24;
            }
            else if (unit == VolumeMeasurementUnit.Pint)
            {
                conversionFactor = 2 * 8 * 2 * 3 * 24;
            }
            else if (unit == VolumeMeasurementUnit.Quart)
            {
                conversionFactor = 2 * 2 * 8 * 2 * 3 * 24;
            }
            else //Must be Gallon
            {
                conversionFactor = 4 * 2 * 2 * 8 * 2 * 3 * 24;

            }
            int convertedFraction = Convert.ToInt32(FractionToDouble(fraction) * conversionFactor);
            int convertedWhole = wholeNumber * conversionFactor;
            return convertedFraction + convertedWhole;
        }

        public int EncodeMassMeasurement(int wholeNumber, Fraction fraction, MassMeasurementUnit unit)
        {
            int conversionFactor;
            if (unit == MassMeasurementUnit.ounce)
            {
                conversionFactor = 24;
            }
            else //Unit must be pounds
            {
                conversionFactor = 24 * 16;
            }
            int convertedFraction = (int)(FractionToDouble(fraction) * conversionFactor);
            int convertedWhole = wholeNumber * conversionFactor;
            return convertedFraction + convertedWhole;
        }
        public double FractionToDouble(Fraction fraction)
        {
            double res;
            if (fraction == Fraction.OneQuarter)
            {
                res = 1d / 4d;
            }
            else if (fraction == Fraction.OneThird)
            {
                res = 1d / 3d;
            }
            else if (fraction == Fraction.Half)
            {
                res = 1d / 2d;
            }
            else if (fraction == Fraction.TwoThirds)
            {
                res = 2d / 3d;
            }
            else if (fraction == Fraction.ThreeQuarters)
            {
                res = 3d / 4d;
            }
            else
            {
                res = 0d;
            }
            return res;
        }
        public string FractionToString(Fraction fraction)
        {
            string res;
            if (fraction == Fraction.OneQuarter)
            {
                res = "1/4";
            }
            else if (fraction == Fraction.OneThird)
            {
                res = "1/3";
            }
            else if (fraction == Fraction.Half)
            {
                res = "1/2";
            }
            else if (fraction == Fraction.TwoThirds)
            {
                res = "2/3";
            }
            else if (fraction == Fraction.ThreeQuarters)
            {
                res = "3/4";
            }
            else
            {
                res = "";
            }
            return res;
        }

        public int EncodeUnitMeasurement(int quantityNumber, Fraction fraction)
        {
            int conversionFactor= 24;
            
            int convertedFraction = (int)(FractionToDouble(fraction) * conversionFactor);
            int convertedWhole = quantityNumber * conversionFactor;
            return convertedFraction + convertedWhole;
        }

        public string DecodeUnitMeasurement(int numberOfUnits)
        {
            string measurement;
            int remainder = numberOfUnits % 24;
            int howMany = (numberOfUnits -remainder)/24;
            double fractionDec = remainder/24d;
            string fraction = FractionToString(DoubleToFraction(fractionDec));
            if (fraction == "")
            {
                measurement = howMany.ToString();
            }
            else
            {
            measurement = $"{howMany} {fraction}";
            }
            return measurement;
        }
    }
}
