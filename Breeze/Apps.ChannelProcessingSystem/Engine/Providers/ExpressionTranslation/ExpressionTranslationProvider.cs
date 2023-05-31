using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Persistence.Entities;
using Dywham.Breeze.Fabric.Adapters.Maths.Equations;
using Dywham.Breeze.Fabric.Adapters.Regex;
using Dywham.Breeze.Fabric.Adapters.Serialization.Json;
using Dywham.Breeze.Fabric.Persistence.Repositories;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Providers.ExpressionTranslation
{
    public class ExpressionTranslationProvider : IExpressionTranslationProvider
    {
        private readonly IPersistenceEntry _persistenceEntry;
        private readonly IRegexAdapter _regexAdapter;
        private readonly IJsonSerializerAdapter _jsonSerializerAdapter;
        private readonly IMathEquationsAdapter _mathEquationsAdapter;


        public ExpressionTranslationProvider(IPersistenceEntry persistenceEntry, IRegexAdapter regexAdapter,
            IJsonSerializerAdapter jsonSerializerAdapter, IMathEquationsAdapter mathEquationsAdapter)
        {
            _persistenceEntry = persistenceEntry;
            _regexAdapter = regexAdapter;
            _jsonSerializerAdapter = jsonSerializerAdapter;
            _mathEquationsAdapter = mathEquationsAdapter;
        }


        public async Task<ChannelsTranslationResult> GenerateExecutionAsync(CancellationToken token = default)
        {
            var functions = await _persistenceEntry.ResolveForEntity<FunctionEntity>().AllAsync(token);
            var result = TranslateChannels(functions);

            TranslateParameters(result.Translations);

            return result;
        }

        private ChannelsTranslationResult TranslateChannels(IList<FunctionEntity> functions)
        {
            var result = new ChannelsTranslationResult
            {
                Translations = new List<TranslatedExpression>(),
                Invalid = new List<string>()
            };

            foreach (var function in functions)
            {
                var functionTranslation = new TranslatedExpression
                {
                    Name = function.Name,
                    RequiredChannels = new List<string>(),
                    RequiredScalars = new List<string>(),
                    Metrics = !string.IsNullOrEmpty(function.Metrics)
                        ? _jsonSerializerAdapter.Deserialize<Dictionary<MetricType, string>>(function.Metrics)
                        : new Dictionary<MetricType, string>()
                };

                var matches = _regexAdapter.MatchesAsStrings(function.Expression, "[A-Z]")
                    //Excludes itself and only persisted output channels
                    .Where(x => !x.Equals(function.Name) && functions.Any(y => y.Name == x));

                foreach (var match in matches)
                {
                    var referenceFunction = functions.Single(x => x.Name.Equals(match));

                    function.Expression = function.Expression.Replace(match, referenceFunction.Expression);
                }

                //Finds all the native channels used in the expression
                var nativeChannels = _regexAdapter.MatchesAsStrings(function.Expression, "[A-Z]")
                    .Where(x => functions.All(y => y.Name != x))
                    .Distinct();

                functionTranslation.RequiredChannels.AddRange(nativeChannels);
                functionTranslation.Expression = function.Expression;

                //Finds if any of the provided functions has cyclic references
                functionTranslation.ContainsCyclic = _regexAdapter
                    .MatchesAsStrings(function.Expression, "[A-Z]").Any(x => functions.Any(y => x == y.Name));

                if (functionTranslation.ContainsCyclic || !_mathEquationsAdapter.IsValid(functionTranslation.Expression))
                {
                    result.Invalid.Add(functionTranslation.Name);
                }
                else
                {
                    result.Translations.Add(functionTranslation);
                }
            }

            return result;
        }

        private void TranslateParameters(IEnumerable<TranslatedExpression> translatedExpressions)
        {
            foreach (var translatedExpression in translatedExpressions)
            {
                foreach (var match in _regexAdapter.MatchesAsStrings(translatedExpression.Expression, "[a-z]"))
                {
                    translatedExpression.RequiredScalars.Add(match);
                }
            }
        }
    }
}