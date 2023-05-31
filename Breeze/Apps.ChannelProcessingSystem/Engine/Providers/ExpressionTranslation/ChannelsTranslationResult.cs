using System.Collections.Generic;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Providers.ExpressionTranslation
{
    public class ChannelsTranslationResult
    {
        public List<TranslatedExpression> Translations { get; set; }

        public List<string> Invalid { get; set; }
    }
}