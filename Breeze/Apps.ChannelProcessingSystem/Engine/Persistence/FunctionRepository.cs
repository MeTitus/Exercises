using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Persistence.Entities;
using Dywham.Breeze.Fabric.Adapters.IO;
using Dywham.Breeze.Fabric.Persistence.Repositories;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Persistence
{
    internal class FunctionRepository : IRepository<FunctionEntity>
    {
        private readonly IChannelProcessingEngineSettings _settings;
        private readonly IInputOutputAdapter _inputOutputAdapter;


        public FunctionRepository(IChannelProcessingEngineSettings settings, IInputOutputAdapter inputOutputAdapter)
        {
            _settings = settings;
            _inputOutputAdapter = inputOutputAdapter;
        }


        public async Task<IList<FunctionEntity>> AllAsync(CancellationToken token = default)
        {
            var data = await _inputOutputAdapter.ReadAllLinesAsync(_settings.FunctionsRepositorySourceFile, token);

            return data
                .Select(line => line.Split(","))
                .Select(lineValues => new FunctionEntity
                {
                    Name = lineValues[0],
                    Expression = lineValues[1],
                    Description = lineValues[2],
                    Metrics = lineValues[3]
                }).ToList();
        }
    }
}