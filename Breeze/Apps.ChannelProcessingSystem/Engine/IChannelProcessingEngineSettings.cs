namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine
{
    public interface IChannelProcessingEngineSettings
    {
        string FunctionsRepositorySourceFile { get; set; }

        int CalculationsSampleTarget { get; set; }

        string UnprocessedTelemetryFilesLocation { get; set; }

        string ProcessedTelemetryFilesLocation { get; set; }

        int CalculationResultMaxDecimals { get; set; }
    }
}