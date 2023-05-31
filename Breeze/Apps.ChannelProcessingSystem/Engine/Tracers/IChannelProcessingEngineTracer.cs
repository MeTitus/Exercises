using System.Threading;
using System.Threading.Tasks;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Providers.Calculator;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Tracers
{
    public interface IChannelProcessingEngineTracer
    {
        Task OutputAsync(CalculationsResult result, CancellationToken token);
    }
}