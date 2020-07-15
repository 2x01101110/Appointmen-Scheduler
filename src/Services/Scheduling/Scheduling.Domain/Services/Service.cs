using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.Services
{
    /// <summary>
    /// Replicated from Services.Service microservice
    /// </summary>
    public class Service : Entity<Guid>, IAggregateRoot
    {
        public string Name { get; }

        private Service(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public static Service CreateService(Guid id, string name)
        {
            return new Service(id, name);
        }
    }
}
