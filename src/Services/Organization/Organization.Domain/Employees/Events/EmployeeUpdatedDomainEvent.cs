using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organization.Domain.Staff.Events
{
    public class EmployeeUpdatedDomainEvent : INotification
    {
        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }

        public EmployeeUpdatedDomainEvent(Employee staff)
        {
            this.Id = staff.Id;
            this.FirstName = staff.FirstName;
            this.LastName = staff.LastName;
        }
    }
}
