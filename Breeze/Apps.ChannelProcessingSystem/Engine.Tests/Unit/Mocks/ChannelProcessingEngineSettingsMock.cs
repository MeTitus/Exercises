namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Tests.Unit.Mocks
{
    public class ChannelProcessingEngineSettingsMock : IChannelProcessingEngineSettings
    {
        public int CalculationsSampleTarget { get; set; } = 100;

        public int CalculationResultMaxDecimals { get; set; } = 15;

        public string FunctionsRepositorySourceFile { get; set; }

        public string UnprocessedTelemetryFilesLocation { get; set; }
        
        public string ProcessedTelemetryFilesLocation { get; set; }
    }
}
