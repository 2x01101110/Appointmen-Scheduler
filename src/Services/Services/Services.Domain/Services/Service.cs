using BuildingBlocks.Domain;
using Services.Domain.Services.Events;
using System;

namespace Services.Domain.Services
{
    public class Service : Entity<Guid>, IAggregateRoot
    {
        public Guid OrganizationId { get; }
        public string Name { get; private set; }
        public bool CanSelectStaff { get; private set; }
        public bool Available { get; private set; }

        private Service(Guid origanizationId, string name, bool canSelectStaff, bool available)
        {
            this.Id = Guid.NewGuid();
            this.OrganizationId = origanizationId;
            this.Name = name;
            this.CanSelectStaff = canSelectStaff;
            this.Available = available;
            
            this.AddDomainEvent(new ServiceCreatedDomainEvent(this));
        }

        public static Service CreateService(Guid origanizationId, string name, bool canSelectStaff, bool available)
        {
            return new Service(origanizationId, name, canSelectStaff, available);
        }

        public void UpdateService(string name, bool canSelectStaff, bool available)
        {
            this.Name = name;
            this.CanSelectStaff = canSelectStaff;
            this.Available = available;

            this.AddDomainEvent(new ServiceUpdatedDomainEvent(this));
        }
    }
}
