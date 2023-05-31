using Dywham.Breeze.Fabric.Persistence.Repositories.Entities;

namespace Dywham.Breeze.Fabric.Persistence.Repositories
{
    public interface IPersistenceEntry
    {
        IRepository<T> ResolveForEntity<T>() where T : Entity, new();
    }
}