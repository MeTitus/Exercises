using System.Collections.Generic;
using System.Linq;
using MathNet.Symbolics;

namespace Dywham.Breeze.Fabric.Adapters.Maths.Equations
{
    public class MathEquationsAdapter : MathAdapter, IMathEquationsAdapter
    {
        public bool TryExecute(string expression, IDictionary<string, double> symbols, out double value)
        {
            value = 0;

            try
            {
                value = Execute(expression, symbols);

                return true;
            }
            catch
            {
                // ignored
            }

            return false;
        }

        public double Execute(string expression, IDictionary<string, double> symbols)
        {
            var result = Evaluate.Evaluate(symbols.ToDictionary(x => x.Key, x => FloatingPoint.NewReal(x.Value)), Infix.ParseOrThrow(expression));

            if (result.IsUndef || result.IsComplexInf) return 0;

            return result.RealValue;
        }

        public bool IsValid(string expression)
        {
            return Infix.TryParse(expression) != null;
        }
    }
}