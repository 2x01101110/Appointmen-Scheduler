using BuildingBlocks.Domain;
using System;

namespace Scheduling.Domain.Services
{
    public class Service : Entity<Guid>, IAggregateRoot
    {
        public string Name { get; private set; }

        private Service(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public static Service CreateService(Guid id, string name)
        {
            return new Service(id, name);
        }

        public void UpdateService(string name)
        {
            this.Name = name;
        }
    }
}
