using System;
using System.Net;
using Dywham.Breeze.Fabric.Adapters.DataAcquisition.File;
using Dywham.Breeze.Fabric.Adapters.DataAcquisition.RealTime;

namespace Dywham.Breeze.Fabric.Adapters.DataAcquisition
{
    public class DataAcquisitionAdapter : IDataAcquisitionAdapter
    {
        private readonly Func<object, IDataAcquisitionAccess> _dataAcquisitionAccess;


        public DataAcquisitionAdapter(Func<object, IDataAcquisitionAccess> dataAcquisitionAccess)
        {
            _dataAcquisitionAccess = dataAcquisitionAccess;
        }


        public IFileDataAcquisitionAccess Open(string identifier)
        {
            var access = (FileDataAcquisitionAccess)_dataAcquisitionAccess(identifier);

            access.Initialize(identifier);

            return access;
        }

        //Not implemented but all the libs are still set to preview
        public IRealTimeDataAcquisitionAccess Open(IPAddress identifier)
        {
            throw new NotImplementedException();
        }
    }
}