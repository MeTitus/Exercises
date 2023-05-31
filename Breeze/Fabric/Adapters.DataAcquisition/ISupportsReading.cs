using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Dywham.Breeze.Fabric.Adapters.DataAcquisition
{
    public interface ISupportsReading : IDataAcquisitionAccess
    {
        Task<IDictionary<string, double>> ReadParametersAsync(CancellationToken token = default);

        Task<IDictionary<string, IList<double>>> ReadChannelDataAsync(CancellationToken token = default);

        Task<IDictionary<string, double>> ReadMetricsAsync(CancellationToken token = default);

        Task<IDictionary<string, IList<double>>> ReadComputedChannelDataAsync(CancellationToken token = default);
    }
}