using BuildingBlocks.Domain;
using Services.Domain.Staff.Events;
using System;

namespace Services.Domain.Staff
{
    public class Staff : Entity<Guid>, IAggregateRoot
    {
        public Guid OrganizationId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Phone { get; }

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

        public static Staff CreateStaffMember(Guid origanizationId, string firstName, string lastName, string email, string phone)
        {
            return new Staff(origanizationId, firstName, lastName, email, phone);
        }
    }
}
