using Dywham.Breeze.Fabric.Persistence.Repositories.Entities;

namespace Dywham.Breeze.Apps.ChannelProcessingSystem.Engine.Persistence.Entities
{
    public class FunctionEntity : Entity
    {
        public string Name { get; set; }

        public string Expression { get; set; }

        public string Description { get; set; }

        public string Metrics { get; set; }
    }
}