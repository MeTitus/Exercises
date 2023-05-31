using System;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine
{
    public class ChannelProcessingEngineProcessResult
    {
        public string UnprocessedFile { get; set; }

        public string ProcessedFile { get; set; }

        public DateTime ProcessedOn { get; set; }

        public int ProcessDuration { get; set; }
    }
}