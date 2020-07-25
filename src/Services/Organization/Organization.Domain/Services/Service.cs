using BuildingBlocks.Domain;
using Organization.Domain.Services.Events;
using System;
using System.Collections.Generic;

namespace Organization.Domain.Services
{
    public class Service : Entity<Guid>, IAggregateRoot
    {
        private readonly List<ServiceStaff> _staff;

        public Guid OrganizationId { get; }
        public string Name { get; private set; }
        public bool CanSelectStaff { get; private set; }
        public bool Available { get; private set; }
        public IReadOnlyCollection<ServiceStaff> Staff => _staff;

        private Service() { }

        private Service(Guid origanizationId, string name, bool canSelectStaff, bool available)
        {
            this.Id = Guid.NewGuid();
            this.OrganizationId = origanizationId;
            this.Name = name;
            this.CanSelectStaff = canSelectStaff;
            this.Available = available;
            this._staff = this._staff ?? new List<ServiceStaff>();

            this.AddDomainEvent(new ServiceCreatedDomainEvent(this));
        }

        public static Service CreateService(Guid origanizationId, string name, bool canSelectStaff, bool available)
        {
            return new Service(origanizationId, name, canSelectStaff, available);
        }

        public void AssignStaffToService(Guid staffId)
        {
            var staff = ServiceStaff.CreateServiceStaff(staffId);
            this._staff.Add(staff);
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
