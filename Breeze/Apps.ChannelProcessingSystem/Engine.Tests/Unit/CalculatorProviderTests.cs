using System;
using System.Linq;
using System.Threading.Tasks;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Providers.Calculator;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Tests.Unit.Extensions;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Tests.Unit.Mocks;
using Newtonsoft.Json;
using Xunit;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Tests.Unit
{
    public class CalculatorProviderTests
    {
        [Theory]
        [InlineData("CalculatorProviderTests_1")]
        public async Task GivenASetOfChannels_WhenTheDataAndRegisteredFunctionsAreValid_ThenAllTheFunctionsNeedToBeCalculated(string name)
        {
            var results = await ExecuteAsync(name);

            Assert.True(results.Item1.ComputedChannels.Count == results.Item2.Functions.Count);
        }

        [Theory]
        [InlineData("CalculatorProviderTests_1")]
        public async Task GivenASetOfChannels_WhenThereMissingChannels_ThenFailingFunctionsShouldBeRegistered(string name)
        {
            var results = await ExecuteAsync(name);

            Assert.True(results.Item1.ComputedChannels.Count == results.Item2.Functions.Count);
        }

        [Theory]
        [InlineData("CalculatorProviderTests_2")]
        public async Task GivenASetOfChannels_WhenTheDataIsValid_ThenMetricsShouldBeGeneratedIfRequest(string name)
        {
            var results = await ExecuteAsync(name);

            Assert.True(results.Item1.InvalidFunctions.All(x => results.Item2.InvalidFunctions.Any(y => y == x)));
        }

        [Theory]
        [InlineData("CalculatorProviderTests_3")]
        public async Task GivenASetOfChannels_WhenTheDataIsValid_ThenComputedChannelsShouldMatchExpectedValues(string name)
        {
            var results = await ExecuteAsync(name);
            var expectedSampleCount = results.Item1.ComputedChannels.First().Value.Count;

            foreach (var computedChannel in results.Item1.ComputedChannels)
            {
                var referenceValue = results.Item2.ComputedChannels[computedChannel.Key];

                for (var sample = 0; sample < expectedSampleCount; sample++)
                {
                    Assert.True(referenceValue[sample] == computedChannel.Value[sample]);
                }
            }
        }

        private static async Task<Tuple<CalculationsResult, CalculatorProviderTestsData>> ExecuteAsync(string name)
        {
            var data = Properties.Resources.ResourceManager.GetString(name)!;
            var context = JsonConvert.DeserializeObject<CalculatorProviderTestsData>(data);
            var expressionTranslationProvider = new ExpressionTranslationProviderExtension(new PersistenceEntryMock(new FunctionRepositoryMock(context.Functions)));
            var settings = new ChannelProcessingEngineSettingsMock { CalculationsSampleTarget = context.Channels.Values.First().Count };
            var stu = new CalculatorProviderExtension(expressionTranslationProvider, settings);

            await stu.InitializeAsync();

            return new Tuple<CalculationsResult, CalculatorProviderTestsData>(stu.PerformCalculations(context.Parameters, context.Channels), context);
        }
    }
}