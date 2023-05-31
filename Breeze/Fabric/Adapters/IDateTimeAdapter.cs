using System;

namespace Dywham.Breeze.Fabric.Adapters
{
    public interface IDateTimeAdapter : IAdapter
    {
        DateTime GetUtcNow();
    }
}