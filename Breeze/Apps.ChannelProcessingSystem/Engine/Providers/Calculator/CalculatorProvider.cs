using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Providers.ExpressionTranslation;
using Dywham.Breeze.Fabric.Adapters.Maths.Equations;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Providers.Calculator
{
    public class CalculatorProvider : ICalculatorProvider
    {
        private readonly IExpressionTranslationProvider _expressionTranslationProvider;
        private readonly IMathEquationsAdapter _mathEquationsAdapter;
        private readonly IChannelProcessingEngineSettings _settings;
        private IList<TranslatedExpression> _expressionTranslationResult;


        public CalculatorProvider(IExpressionTranslationProvider expressionTranslationProvider,
            IMathEquationsAdapter mathEquationsAdapter, IChannelProcessingEngineSettings settings)
        {
            _expressionTranslationProvider = expressionTranslationProvider;
            _mathEquationsAdapter = mathEquationsAdapter;
            _settings = settings;
        }


        //The whole purpose of this extra step is to avoid the overhead of having to expand the functions every time a calculation is executed.
        //In case the underlying functions are updated, this method needs to be called again
        public async Task InitializeAsync(CancellationToken token = default)
        {
            _expressionTranslationResult = (await _expressionTranslationProvider.GenerateExecutionAsync(token)).Translations;
        }

        //Calculations assume that if more than one channel is provided, all the data has already been normalized to a target sample count
        public CalculationsResult PerformCalculations(IDictionary<string, double> parameters, IDictionary<string, IList<double>> inputs)
        {
            var calculationsResult = new CalculationsResult
            {
                ComputedChannels = new Dictionary<string, IList<double>>(),
                ComputedMetrics = new Dictionary<string, double>(),
                InvalidFunctions = new List<string>()
            };

            //In the future it would probably be a good idea to create a different way to identify parameters/metrics to make the process simpler
            var metrics = _expressionTranslationResult.SelectMany(x => x.Metrics.Values).Distinct().ToList();

            Parallel.ForEach(_expressionTranslationResult.Where(x => !x.RequiredScalars.Intersect(metrics).Any()), x =>
            {
                if (!x.RequiredChannels.All(inputs.ContainsKey) || !TryCalculateValues(parameters, inputs, x, out var values))
                {
                    calculationsResult.InvalidFunctions.Add(x.Name);

                    return;
                }

                foreach (var statistic in x.Metrics)
                {
                    switch (statistic.Key)
                    {
                        case MetricType.Mean:

                            var value = _mathEquationsAdapter.Round(_mathEquationsAdapter.Mean(values), _settings.CalculationResultMaxDecimals);

                            calculationsResult.ComputedMetrics.Add(statistic.Value, value);

                            parameters.Add(statistic.Value, value);

                            break;

                        default:

                            throw new ArgumentOutOfRangeException();
                    }
                }

                calculationsResult.ComputedChannels.Add(x.Name, values);
            });

            Parallel.ForEach(_expressionTranslationResult.Where(x => x.RequiredScalars.Intersect(metrics).Any()), x =>
            {
                if (!TryCalculateValues(parameters, inputs, x, out var values)) return;

                calculationsResult.ComputedChannels.Add(x.Name, values);
            });

            calculationsResult.InvalidFunctions = calculationsResult.InvalidFunctions.Distinct().ToList();

            return calculationsResult;
        }

        private bool TryCalculateValues(IDictionary<string, double> parameters, IDictionary<string, IList<double>> inputs, TranslatedExpression translatedExpression, out List<double> values)
        {
            values = new List<double>();

            var symbols = translatedExpression.RequiredScalars
                .ToDictionary(requiredScalar => requiredScalar, requiredScalar => parameters[requiredScalar]);

            for (var index = 0; index < _settings.CalculationsSampleTarget; index++)
            {
                var expression = translatedExpression.Expression;

                foreach (var requiredChannel in translatedExpression.RequiredChannels)
                {
                    symbols[requiredChannel] = inputs[requiredChannel][index];
                }

                if (_mathEquationsAdapter.TryExecute(expression, symbols, out var value))
                {
                    values.Add(_mathEquationsAdapter.Round(value, _settings.CalculationResultMaxDecimals));
                }
                else
                {
                    //Not point in continuing the execution because the function is invalid

                    return false;
                }
            }

            return true;
        }
    }
}