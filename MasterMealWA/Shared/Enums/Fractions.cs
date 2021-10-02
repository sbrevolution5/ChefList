using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Shared.Enums
{
    public enum Fraction
    {
        [Description("none")]
        NoFraction,
        [Description("1/4")]
        OneQuarter,
        [Description("1/3")]
        OneThird,
        [Description("1/2")]
        Half,
        [Description("2/3")]
        TwoThirds,
        [Description("3/4")]
        ThreeQuarters,
    }
}
