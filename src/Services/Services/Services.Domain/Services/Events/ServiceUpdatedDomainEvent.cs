using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Domain.Services.Events
{
    public class ServiceUpdatedDomainEvent : INotification
    {
        public Guid Id { get; }
        public string Name { get; }

        public ServiceUpdatedDomainEvent(Service service)
        {
            this.Id = service.Id;
            this.Name = service.Name;
        }
    }
}
