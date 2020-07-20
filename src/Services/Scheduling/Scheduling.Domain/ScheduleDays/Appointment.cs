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

        private Appointment() { }

        private Appointment(AppointmentTimeSlot appointmentTimeSlot, ContactInformation contactInformation)
        {
            this.AppointmentTimeSlot = appointmentTimeSlot;
            this.AppointmentStatus = AppointmentStatus.ClientConfirmationPending;
            this.ContactInformation = contactInformation;

            this.AddDomainEvent(new AppointmentCreatedDomainEvent(this));
        }

        private Appointment(DateTime appointmentDay, ContactInformation contactInformation)
        {
            this.AppointmentTimeSlot = new AppointmentTimeSlot(appointmentDay.Date);
            this.AppointmentStatus = AppointmentStatus.StaffConfirmationPending;
            this.ContactInformation = contactInformation;

            this.AddDomainEvent(new AppointmentCreatedDomainEvent(this));
        }

        public static Appointment CreateAppointmentWithTimeSlot(AppointmentTimeSlot appointmentTimeSlot, ContactInformation contactInformation)
        {
            return new Appointment(appointmentTimeSlot, contactInformation);
        }

        public static Appointment CreateAppointmentWithoutTimeSlot(DateTime appointmentDay, ContactInformation contactInformation)
        {
            return new Appointment(appointmentDay, contactInformation);
        }

        public void ConfirmAppointment()
        {
            this.AppointmentStatus = AppointmentStatus.Confirmed;
            this.AddDomainEvent(new AppointmentStatusChangedDomainEvent(this));
        }

        public void ConfirmAppointment(AppointmentTimeSlot appointmentTimeSlot)
        {
            // Check if not null
            this.AppointmentTimeSlot = appointmentTimeSlot;
            this.AppointmentStatus = AppointmentStatus.Confirmed;
            this.AddDomainEvent(new AppointmentStatusChangedDomainEvent(this));
        }

        // Todo: change day and time slot of appointment
        public void ChangeAppointmentTime()
        { 
        
        }

        public void CancelAppointment()
        {
            this.AppointmentStatus = AppointmentStatus.Canceled;
            this.AddDomainEvent(new AppointmentStatusChangedDomainEvent(this));
        }
    }
}
