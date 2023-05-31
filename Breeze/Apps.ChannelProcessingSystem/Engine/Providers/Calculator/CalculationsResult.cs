using System.Collections.Generic;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Providers.Calculator
{
    public class CalculationsResult
    {
        public IDictionary<string, IList<double>> ComputedChannels { get; set; }

        public IDictionary<string, double> ComputedMetrics { get; set; }

        public IList<string> InvalidFunctions { get; set; }
    }
}