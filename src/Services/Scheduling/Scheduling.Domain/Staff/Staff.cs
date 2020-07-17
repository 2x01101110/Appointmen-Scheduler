using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.Staff
{
    public class Staff : Entity<Guid>, IAggregateRoot
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        private Staff(Guid staffId, string firstName, string lastName)
        {
            this.Id = staffId;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public static Staff Create(Guid staffId, string firstName, string lastName)
        {
            return new Staff(staffId, firstName, lastName);
        }

        public void UpdateStaff(string firstName, string lastName) 
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}
