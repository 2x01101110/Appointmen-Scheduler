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

        private Service(Guid origanizationId, string name, bool canSelectStaff)
        {
            this.Id = Guid.NewGuid();
            this.OrganizationId = origanizationId;
            this.Name = name;
            this.CanSelectStaff = canSelectStaff;
            
            this.AddDomainEvent(new ServiceCreatedDomainEvent(this));
        }

        public static Service CreateService(Guid origanizationId, string name, bool canSelectStaff)
        {
            return new Service(origanizationId, name, canSelectStaff);
        }

        public void UpdateService(string name, bool canSelectStaff)
        {
            this.Name = name;
            this.CanSelectStaff = canSelectStaff;

            this.AddDomainEvent(new ServiceUpdatedDomainEvent(this));
        }
    }
}
