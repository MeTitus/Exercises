using Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Persistence.Entities;
using Dywham.Breeze.Fabric.Persistence.Repositories;
using Dywham.Breeze.Fabric.Persistence.Repositories.Entities;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Tests.Unit.Mocks
{
    public class PersistenceEntryMock : IPersistenceEntry
    {
        private readonly FunctionRepositoryMock _functionRepositoryMock;


        public PersistenceEntryMock(FunctionRepositoryMock functionRepositoryMock)
        {
            _functionRepositoryMock = functionRepositoryMock;
        }


        public IRepository<T> ResolveForEntity<T>() where T : Entity, new()
        {
            if (typeof(T) == typeof(FunctionEntity))
            {
                return (IRepository<T>)_functionRepositoryMock;
            }

            return null;
        }
    }
}