using System.Threading;
using System.Threading.Tasks;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Tracers;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine
{
    public interface IChannelProcessingEngine
    {
        void AddTracerExecution(IChannelProcessingEngineTracer tracer);

        Task<ChannelProcessingEngineProcessResult> ProcessAsync(string file, CancellationToken token = default);

        Task<ChannelProcessingEngineProcessResult> ProcessAsync(string parameters, string channels, CancellationToken token = default);
    }
}