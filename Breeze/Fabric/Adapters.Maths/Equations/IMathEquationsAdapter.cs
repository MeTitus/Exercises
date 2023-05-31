using System.Collections.Generic;

namespace Dywham.Breeze.Fabric.Adapters.Maths.Equations
{
    public interface IMathEquationsAdapter : IMathAdapter
    {
        bool IsValid(string expression);

        public double Execute(string expression, IDictionary<string, double> symbols);

        public bool TryExecute(string expression, IDictionary<string, double> symbols, out double value);
    }
}