using System.Collections.Generic;

namespace Dywham.Breeze.Fabric.Adapters.Maths
{
    public interface IMathAdapter : IAdapter
    {
        double Mean(IEnumerable<double> data);

        double Round(double number, int decimalPlaces);
    }
}