using System;

namespace Dywham.Breeze.Fabric.Persistence.Repositories.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public int Version { get; set; }
    }
}