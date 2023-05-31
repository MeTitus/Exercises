using System;

namespace Dywham.Breeze.Fabric.Adapters
{
    public class UniqueIdentifierGeneratorAdapter : IUniqueIdentifierGeneratorAdapter
    {
        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}