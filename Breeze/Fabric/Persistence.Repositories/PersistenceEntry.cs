using Autofac;
using Dywham.Breeze.Fabric.Persistence.Repositories.Entities;

namespace Dywham.Breeze.Fabric.Persistence.Repositories
{
    public class PersistenceEntry : IPersistenceEntry
    {
        private readonly IComponentContext _componentContext;


        public PersistenceEntry(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }


        public IRepository<T> ResolveForEntity<T>() where T : Entity, new()
        {
            return _componentContext.Resolve(typeof(IRepository<T>)) as IRepository<T>;
        }
    }
}