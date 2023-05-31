using System.Net;
using Dywham.Breeze.Fabric.Adapters.DataAcquisition.File;
using Dywham.Breeze.Fabric.Adapters.DataAcquisition.RealTime;

namespace Dywham.Breeze.Fabric.Adapters.DataAcquisition
{
    public interface IDataAcquisitionAdapter
    {
        IFileDataAcquisitionAccess Open(string identifier);

        IRealTimeDataAcquisitionAccess Open(IPAddress identifier);
    }
}