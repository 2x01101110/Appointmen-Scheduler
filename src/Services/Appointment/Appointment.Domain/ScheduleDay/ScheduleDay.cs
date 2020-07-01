using Appointment.Domain.ScheduleDay.Rules;
using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;

namespace Appointment.Domain.ScheduleDay
{
    public class ScheduleDay : Entity<Guid>, IAggregateRoot 
    {
        public DateTime Day { get; }
        public bool Available { get; private set; }
        public ContactInformation ContactInformation { get; private set; }

        private List<Appointment> _appointments;
        public IReadOnlyCollection<Appointment> Appointments => _appointments;

        private ScheduleDay() { }

        public Appointment CreateAppointment(AppointmentTimeSlot appointmentTimeSlot)
        {
            this.CheckBusinessRule(new AppointmentTimeSlotNotTaken(this.Appointments, appointmentTimeSlot));

            return Appointment.CreateNew(appointmentTimeSlot);
        }
    }
}
