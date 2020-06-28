using Appointment.Domain.Appointments.Events;
using BuildingBlocks.Domain;
using System;

namespace Appointment.Domain.Appointments
{
    public class Appointment : Entity<Guid>, IAggregateRoot
    {
        public Guid Service { get; }
        public DateTime AppointmentCreated { get; }
        public AppointmentTime AppointmentTime { get; private set; }
        public ContactInformation ContactInformation { get; private set; }
        public AppointmentStatus AppointmentStatus { get; private set; }

        private Appointment() { }

        private Appointment(
            ContactInformation contactInformation,
            AppointmentTime appointmentTime)
        {
            this.Id = Guid.NewGuid();
            this.AppointmentCreated = DateTime.UtcNow;
            this.ContactInformation = contactInformation;
            this.AppointmentStatus = AppointmentStatus.ConfirmationPending;
            this.AppointmentTime = appointmentTime;

            this.AddDomainEvent(new AppointmentCreatedDomainEvent());
        }

        public static Appointment Create(
            ContactInformation contactInformation,
            AppointmentTime appointmentTime)
        {
            return new Appointment(contactInformation, appointmentTime);
        }

        public void Confirm()
        {
            this.AppointmentStatus = AppointmentStatus.Confirmed;

            this.AddDomainEvent(new AppointmentConfirmedDomainEvent());
        }

        public void Cancel(string cancelReason)
        {
            this.AppointmentStatus = AppointmentStatus.Canceled(cancelReason);

            this.AddDomainEvent(new AppointmentCanceledDomainEvent());
        }
    }
}
