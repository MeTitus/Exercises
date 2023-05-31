using System;
using System.Collections.Generic;
using MathNet.Numerics.Statistics;

namespace Dywham.Breeze.Fabric.Adapters.Maths
{
    public class MathAdapter : IMathAdapter
    {
        public double Mean(IEnumerable<double> data)
        {
            return data.Mean();
        }

        public double Round(double number, int decimalPlaces)
        {
            return Math.Round(number, decimalPlaces);
        }
    }
}