using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Providers.Calculator;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Tracers
{
    public class ConsoleChannelProcessingEngineTracer : IChannelProcessingEngineTracer
    {
        public async Task OutputAsync(CalculationsResult result, CancellationToken token)
        {
            var streamWriter = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };

            await streamWriter.WriteLineAsync("#####################################");
            await streamWriter.WriteLineAsync("\tMetrics");
            await streamWriter.WriteLineAsync("#####################################");
            await streamWriter.WriteLineAsync(Environment.NewLine);

            foreach (var metric in result.ComputedMetrics)
            {
                await streamWriter.WriteLineAsync($"{metric.Key} - {metric.Value}");
            }

            await streamWriter.WriteLineAsync(Environment.NewLine);
            await streamWriter.WriteLineAsync("#####################################");
            await streamWriter.WriteLineAsync("\tComputed Channels");
            await streamWriter.WriteLineAsync("#####################################");
            await streamWriter.WriteLineAsync(Environment.NewLine);
            await streamWriter.WriteLineAsync(string.Join("\t\t", result.ComputedChannels.Keys));

            for (var index = 0; index < result.ComputedChannels.First().Value.Count; index++)
            {
                var stringBuilder = new StringBuilder();

                foreach (var channel in result.ComputedChannels)
                {
                    stringBuilder.Append($"{channel.Value[index]}\t");
                }

                await streamWriter.WriteLineAsync($"{stringBuilder}({index + 1})\t");
            }
        }
    }
}