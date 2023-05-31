using System;

namespace Dywham.Breeze.Fabric.Adapters
{
    public class DateTimeAdapter : IDateTimeAdapter
    {
        public DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}