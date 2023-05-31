using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Bootstrap
{
    public class EndpointSettings : IChannelProcessingEngineSettings
    {
        public string FunctionsRepositorySourceFile { get; set; }

        public int CalculationsSampleTarget { get; set; }

        public string UnprocessedTelemetryFilesLocation { get; set; }

        public string ProcessedTelemetryFilesLocation { get; set; }

        public int CalculationResultMaxDecimals { get; set; }
    }
}