using System.Collections.Generic;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Persistence.Entities;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Tests.Unit
{
    public class CalculatorProviderTestsData
    {
        public IList<FunctionEntity> Functions { get; set; }

        public IDictionary<string, double> Parameters { get; set; }

        public IDictionary<string, IList<double>> Channels { get; set; }

        public IDictionary<string, IList<double>> ComputedChannels { get; set; }

        public IDictionary<string, double> CalculatedMetrics { get; set; }

        public IList<string> InvalidFunctions { get; set; }
    }
}