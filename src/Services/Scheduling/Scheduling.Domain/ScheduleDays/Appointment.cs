using BuildingBlocks.Domain;
using Scheduling.Domain.ScheduleDays.Events;
using System;

namespace Scheduling.Domain.ScheduleDays
{
    public class Appointment : Entity<Guid>
    {
        public AppointmentTimeSlot AppointmentTimeSlot { get; private set; }
        public AppointmentStatus AppointmentStatus { get; private set; }
        public ContactInformation ContactInformation { get; private set; }

        private Appointment(AppointmentTimeSlot appointmentTimeSlot, ContactInformation contactInformation)
        {
            this.AppointmentTimeSlot = appointmentTimeSlot;
            this.AppointmentStatus = AppointmentStatus.ConfirmationPending;
            this.ContactInformation = contactInformation;

            this.AddDomainEvent(new AppointmentCreatedDomainEvent());
        }

        public static Appointment CreateNew(AppointmentTimeSlot appointmentTimeSlot, ContactInformation contactInformation)
        {
            return new Appointment(appointmentTimeSlot, contactInformation);
        }

        public void ConfirmAppointment()
        {
            this.AppointmentStatus = AppointmentStatus.Confirmed;

            this.AddDomainEvent(new AppointmentConfirmedDomainEvent());
        }
        public void CancelAppointment()
        {
            this.AppointmentStatus = AppointmentStatus.Canceled;

            this.AddDomainEvent(new AppointmentCanceledDomainEvent());
        }
    }
}
