using Appointment.Domain.ScheduleDay.Events;
using BuildingBlocks.Domain;
using System;

namespace Appointment.Domain.ScheduleDay
{
    public class Appointment : Entity<Guid>
    {
        public AppointmentTimeSlot AppointmentTimeSlot { get; private set; }
        public AppointmentStatus AppointmentStatus { get; private set; }

        private Appointment(AppointmentTimeSlot appointmentTimeSlot)
        {
            this.AppointmentTimeSlot = appointmentTimeSlot;
            this.AppointmentStatus = AppointmentStatus.ConfirmationPending;

            this.AddDomainEvent(new AppointmentCreatedDomainEvent());
        }

        public static Appointment CreateNew(AppointmentTimeSlot appointmentTimeSlot)
        {
            return new Appointment(appointmentTimeSlot);
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
