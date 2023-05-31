using System;
using System.Linq;
using System.Threading.Tasks;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Persistence.Entities;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Providers.ExpressionTranslation;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Tests.Unit.Extensions;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Tests.Unit.Mocks;
using Newtonsoft.Json;
using Xunit;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Tests.Unit
{
    public class ExpressionTranslationProviderTests
    {
        [Theory]
        [InlineData("ExpressionTranslationProviderTests_1")]
        [InlineData("ExpressionTranslationProviderTests_2")]
        public async Task GivenASetFunctions_WhenThereAreCyclicFunctions_ThenTheGenerationShouldContainInvalidFunctions(string name)
        {
            var results = await ExecuteAsync(name);

            Assert.True(results.Item1.Invalid.Select(x => x).Intersect(results.Item2.InvalidFunctions).Count() == results.Item1.Invalid.Count);
        }

        [Theory]
        [InlineData("ExpressionTranslationProviderTests_2")]
        [InlineData("ExpressionTranslationProviderTests_3")]
        public async Task GivenASetFunctions_WhenThereAreValidFunctions_TranslationsShouldBeGenerated(string name)
        {
            var results = await ExecuteAsync(name);

            Assert.True(results.Item1.Translations.Select(x => x.Name).Intersect(results.Item2.TranslatedFunctions).Count() == results.Item1.Translations.Count);
        }

        [Theory]
        [InlineData("ExpressionTranslationProviderTests_4")]
        public async Task GivenASetFunctions_WhenThereAreMalformedFunctions_ThenTheGenerationShouldContainInvalidFunctions(string name)
        {
            var results = await ExecuteAsync(name);

            Assert.True(results.Item1.Invalid.Select(x => x).Intersect(results.Item2.InvalidFunctions).Count() == results.Item1.Invalid.Count);
        }

        private static async Task<Tuple<ChannelsTranslationResult, ExpressionTranslationProviderData>> ExecuteAsync(string name)
        {
            var data = Properties.Resources.ResourceManager.GetString(name)!;
            var context = JsonConvert.DeserializeObject<ExpressionTranslationProviderData>(data);
            var functions = context.Functions.Select(x => new FunctionEntity
            {
                Name = x.Key,
                Expression = x.Value
            }).ToList();
            var stu = new ExpressionTranslationProviderExtension(new PersistenceEntryMock(new FunctionRepositoryMock(functions)));

            return new Tuple<ChannelsTranslationResult, ExpressionTranslationProviderData>(await stu.GenerateExecutionAsync(), context);
        }
    }
}