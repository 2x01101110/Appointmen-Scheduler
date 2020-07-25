using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organization.Domain.Services.Events
{
    public class ServiceCreatedDomainEvent : INotification
    {
        public Guid Id { get; }
        public string Name { get; set; }

        public ServiceCreatedDomainEvent(Service service)
        {
            this.Id = service.Id;
            this.Name = service.Name;
        }
    }
}
