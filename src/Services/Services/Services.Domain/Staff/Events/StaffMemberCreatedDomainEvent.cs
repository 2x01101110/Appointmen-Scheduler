using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Domain.Staff.Events
{
    public class StaffMemberCreatedDomainEvent : INotification
    {
        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }

        public StaffMemberCreatedDomainEvent(Staff staff)
        {
            this.Id = staff.Id;
            this.FirstName = staff.FirstName;
            this.LastName = staff.LastName;
        }
    }
}
