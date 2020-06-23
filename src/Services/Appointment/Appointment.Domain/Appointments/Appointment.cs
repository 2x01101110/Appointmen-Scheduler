using Appointment.Domain.Appointments.Events;
using BuildingBlocks.Domain;
using System;

namespace Appointment.Domain.Appointments
{
    public class Appointment : Entity<Guid>, IAggregateRoot
    {
        public DateTime AppointmentCreated { get; private set; }
        public ContactInformation ContactInformation { get; private set; }
        public AppointmentStatus AppointmentStatus { get; private set; }

        private Appointment() { }

        private Appointment(
            ContactInformation contactInformation, 
            AppointmentStatus appointmentStatus)
        {
            this.Id = Guid.NewGuid();
            this.AppointmentCreated = DateTime.UtcNow;
            this.ContactInformation = contactInformation;
            this.AppointmentStatus = appointmentStatus;

            this.AddDomainEvent(new AppointmentCreatedDomainEvent());
        }

        public static Appointment Create(
            ContactInformation contactInformation)
        {
            return new Appointment(contactInformation, AppointmentStatus.ConfirmationPending);
        }

        public void Cancel()
        { 
        
        }
    }
}
