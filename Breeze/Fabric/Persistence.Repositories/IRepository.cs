using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Dywham.Breeze.Fabric.Persistence.Repositories
{
    //Just to make the ioc registration easier
    public interface IRepository
    { }

    public interface IRepository<T> : IRepository where T : class
    {
        Task<IList<T>> AllAsync(CancellationToken token = default);
    }
}