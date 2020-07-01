using BuildingBlocks.Domain;
using System.Collections.Generic;

namespace Appointment.Domain.ScheduleDay
{
    public class ContactInformation : ValueObject
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Phone { get; }

        public ContactInformation(
            string firstName,
            string lastName,
            string email, 
            string phone)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;
        }

        private ContactInformation() { }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return this.FirstName;
            yield return this.LastName;
            yield return this.Email;
            yield return this.Phone;
        }
    }
}
