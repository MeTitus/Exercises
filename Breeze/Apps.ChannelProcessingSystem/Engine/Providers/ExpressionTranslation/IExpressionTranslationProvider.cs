using System.Threading;
using System.Threading.Tasks;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Providers.ExpressionTranslation
{
    public interface IExpressionTranslationProvider : IProvider
    {
        Task<ChannelsTranslationResult> GenerateExecutionAsync(CancellationToken token);
    }
}