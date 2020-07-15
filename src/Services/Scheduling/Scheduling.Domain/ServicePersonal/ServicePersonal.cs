using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.ServicePersonal
{
    public class ServicePersonal : Entity<Guid>, IAggregateRoot
    {
        public Guid ServiceId { get; set; }

        private ServicePersonal(Guid serviceId)
        {
            this.ServiceId = serviceId;
        }

        public static ServicePersonal Create(Guid serviceId)
        {
            return new ServicePersonal(serviceId);
        }
    }
}
