using System.Collections.Generic;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Providers.ExpressionTranslation
{
    public class TranslatedExpression
    {
        public string Name { get; set; }

        public string Expression { get; set; }

        public Dictionary<MetricType, string> Metrics { get; set; }

        public List<string> RequiredChannels { get; set; }

        public List<string> RequiredScalars { get; set; }

        public bool ContainsCyclic { get; set; }
    }
}