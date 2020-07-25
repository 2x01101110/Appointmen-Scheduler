using BuildingBlocks.Domain;
using Organization.Domain.Staff.Events;
using System;
using System.Collections.Generic;

namespace Organization.Domain.Staff
{
    public class Employee : Entity<Guid>, IAggregateRoot
    {
        public Guid OrganizationId { get; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }

        private Employee() { }

        private Employee(Guid origanizationId, string firstName, string lastName, string email, string phone)
        {
            this.Id = Guid.NewGuid();
            this.OrganizationId = origanizationId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;

            this.AddDomainEvent(new EmmployeeCreatedDomainEvent(this));
        }

        public static Employee CreateEmployee(Guid origanizationId, string firstName, string lastName, string email, string phone)
        {
            return new Employee(origanizationId, firstName, lastName, email, phone);
        }

        public void UpdateEmployee(string firstName, string lastName, string email, string phone)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;

            this.AddDomainEvent(new EmployeeUpdatedDomainEvent(this));
        }
    }
}
