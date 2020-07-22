using BuildingBlocks.Domain;
using Services.Domain.Staff.Events;
using System;
using System.Collections.Generic;

namespace Services.Domain.Staff
{
    public class Staff : Entity<Guid>, IAggregateRoot
    {
        public Guid OrganizationId { get; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }

        private Staff(Guid origanizationId, string firstName, string lastName, string email, string phone)
        {
            this.Id = Guid.NewGuid();
            this.OrganizationId = origanizationId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;

            this.AddDomainEvent(new StaffMemberCreatedDomainEvent(this));
        }

        public static Staff CreateStaff(Guid origanizationId, string firstName, string lastName, string email, string phone)
        {
            return new Staff(origanizationId, firstName, lastName, email, phone);
        }

        public void UpdateStaff(string firstName, string lastName, string email, string phone)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;

            this.AddDomainEvent(new StaffMemberUpdatedDomainEvent(this));
        }
    }
}
