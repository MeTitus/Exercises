using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Persistence.Entities;
using Dywham.Breeze.Fabric.Persistence.Repositories;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Tests.Unit.Mocks
{
    public class FunctionRepositoryMock : IRepository<FunctionEntity>
    {
        private readonly IList<FunctionEntity> _functions;


        public FunctionRepositoryMock(IList<FunctionEntity> functions)
        {
            _functions = functions;
        }


        public Task<IList<FunctionEntity>> AllAsync(CancellationToken token = new CancellationToken())
        {
            return Task.FromResult(_functions);
        }
    }
}