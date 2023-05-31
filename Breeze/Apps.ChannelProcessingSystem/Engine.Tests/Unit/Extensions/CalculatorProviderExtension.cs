using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Providers.Calculator;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Providers.ExpressionTranslation;
using Dywham.Breeze.Fabric.Adapters.Maths.Equations;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Tests.Unit.Extensions
{
    public class CalculatorProviderExtension : CalculatorProvider
    {
        public CalculatorProviderExtension(IExpressionTranslationProvider expressionTranslationProviderExtension, IChannelProcessingEngineSettings settings)
            : base(expressionTranslationProviderExtension, new MathEquationsAdapter(), settings)
        { }
    }
}