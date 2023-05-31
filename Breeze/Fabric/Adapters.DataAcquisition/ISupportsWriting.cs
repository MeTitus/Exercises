using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Dywham.Breeze.Fabric.Adapters.DataAcquisition
{
    public interface ISupportsWriting : IDataAcquisitionAccess
    {
        Task WriteMetricsAsync(IDictionary<string, double> metrics, CancellationToken token = default);

        Task WriteChannelDataAsync(string name, IList<double> data, CancellationToken token = default);

        Task WriteComputedChannelDataAsync(IDictionary<string, IList<double>> channelsData, CancellationToken token = default);
    }
}