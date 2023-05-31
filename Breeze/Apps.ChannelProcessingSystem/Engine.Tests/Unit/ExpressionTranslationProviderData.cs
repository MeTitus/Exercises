using System.Collections.Generic;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Tests.Unit
{
    public class ExpressionTranslationProviderData
    {
        public IDictionary<string, string> Functions { get; set; }

        public IList<string> InvalidFunctions { get; set; }

        public IList<string> TranslatedFunctions { get; set; }
    }
}