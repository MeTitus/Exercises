using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Providers.Calculator;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Tracers;
using Dywham.Breeze.Fabric.Adapters;
using Dywham.Breeze.Fabric.Adapters.DataAcquisition;
using Dywham.Breeze.Fabric.Adapters.DataAcquisition.File;
using Dywham.Breeze.Fabric.Adapters.IO;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine
{
    public class ChannelProcessingEngine : IChannelProcessingEngine
    {
        private readonly IDataAcquisitionAdapter _dataAcquisitionAdapter;
        private readonly IChannelProcessingEngineSettings _settings;
        private readonly IInputOutputAdapter _inputOutputAdapter;
        private readonly ICalculatorProvider _calculatorProvider;
        private readonly IDateTimeAdapter _dateTimeAdapter;
        private readonly IFileDataAcquisitionConverter _fileDataAcquisitionConverter;
        private readonly IList<IChannelProcessingEngineTracer> _tracers = new List<IChannelProcessingEngineTracer>();


        public ChannelProcessingEngine(IDataAcquisitionAdapter dataAcquisitionAdapter, IChannelProcessingEngineSettings settings,
            IInputOutputAdapter inputOutputAdapter, ICalculatorProvider calculatorProvider, IDateTimeAdapter dateTimeAdapter,
            IFileDataAcquisitionConverter fileDataAcquisitionConverter)
        {
            _dataAcquisitionAdapter = dataAcquisitionAdapter;
            _settings = settings;
            _inputOutputAdapter = inputOutputAdapter;
            _calculatorProvider = calculatorProvider;
            _dateTimeAdapter = dateTimeAdapter;
            _fileDataAcquisitionConverter = fileDataAcquisitionConverter;
        }


        public void AddTracerExecution(IChannelProcessingEngineTracer tracer)
        {
            _tracers.Add(tracer);
        }

        public async Task<ChannelProcessingEngineProcessResult> ProcessAsync(string file, CancellationToken token)
        {
            var startTime = _dateTimeAdapter.GetUtcNow();
            var path = _inputOutputAdapter.Combine(_settings.UnprocessedTelemetryFilesLocation, file);
            var accessForReading = _dataAcquisitionAdapter.Open(path);
            var channelData = await accessForReading.ReadChannelDataAsync(token);
            var parameters = await accessForReading.ReadParametersAsync(token);

            //Method signature as requested in the spec 'function(parameters, inputs)'
            var calculationsResult = _calculatorProvider.PerformCalculations(parameters, channelData);
            var accessForWriting = _dataAcquisitionAdapter.Open(file);

            await accessForWriting.WriteComputedChannelDataAsync(calculationsResult.ComputedChannels, token);
            await accessForWriting.WriteMetricsAsync(calculationsResult.ComputedMetrics, token);

            var processedFile = accessForWriting.Save(_settings.ProcessedTelemetryFilesLocation);
            var finishTime = _dateTimeAdapter.GetUtcNow();

            await RunTracersAsync(calculationsResult, token);

            return new ChannelProcessingEngineProcessResult
            {
                ProcessedOn = finishTime,
                ProcessDuration = (finishTime - startTime).Milliseconds,
                ProcessedFile = processedFile,
                UnprocessedFile = file
            };
        }

        //This method basically compacts the files to the format the FileDataAcquisitionAccess understand
        public Task<ChannelProcessingEngineProcessResult> ProcessAsync(string parameters, string channels, CancellationToken token = default)
        {
            return ProcessAsync(_fileDataAcquisitionConverter.GenerateTdfFile(parameters, channels), token);
        }

        private Task RunTracersAsync(CalculationsResult result, CancellationToken token)
        {
            if (!_tracers.Any()) return Task.CompletedTask;

            return Parallel.ForEachAsync(_tracers, token, async (tracer, _) =>
            {
                await tracer.OutputAsync(result, token);
            });
        }
    }
}