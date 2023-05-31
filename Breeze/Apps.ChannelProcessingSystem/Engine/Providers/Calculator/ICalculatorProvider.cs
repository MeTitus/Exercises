using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Providers.Calculator
{
    public interface ICalculatorProvider : IProvider
    {
        Task InitializeAsync(CancellationToken token = default);

        //Method signature was provided in the exercise but not definition in relation to the parameters type was given
        CalculationsResult PerformCalculations(IDictionary<string, double> parameters, IDictionary<string, IList<double>> inputs);
    }
}