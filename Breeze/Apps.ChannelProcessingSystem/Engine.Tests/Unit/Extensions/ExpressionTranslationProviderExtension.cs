using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Providers.ExpressionTranslation;
using Dywham.Breeze.Fabric.Adapters.Maths.Equations;
using Dywham.Breeze.Fabric.Adapters.Regex;
using Dywham.Breeze.Fabric.Adapters.Serialization.Json;
using Dywham.Breeze.Fabric.Persistence.Repositories;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Tests.Unit.Extensions
{
    public class ExpressionTranslationProviderExtension : ExpressionTranslationProvider
    {
        public ExpressionTranslationProviderExtension(IPersistenceEntry persistenceEntry)
            : base(persistenceEntry, new RegexAdapter(), new JsonSerializerAdapter(), new MathEquationsAdapter())
        { }
    }
}