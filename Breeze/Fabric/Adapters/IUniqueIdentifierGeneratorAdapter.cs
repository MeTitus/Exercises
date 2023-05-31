using System;

namespace Dywham.Breeze.Fabric.Adapters
{
    public interface IUniqueIdentifierGeneratorAdapter : IAdapter
    {
        Guid Generate();
    }
}