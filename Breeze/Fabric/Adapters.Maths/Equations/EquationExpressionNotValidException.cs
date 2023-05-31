using System;

namespace Dywham.Breeze.Fabric.Adapters.Maths.Equations
{
    [Serializable]
    public class EquationExpressionNotValidException : Exception
    {
        public EquationExpressionNotValidException(string error) : base(error)
        { }
    }
}