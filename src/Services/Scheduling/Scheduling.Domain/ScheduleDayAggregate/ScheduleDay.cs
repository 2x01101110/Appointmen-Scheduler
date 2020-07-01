using BuildingBlocks.Domain;
using Scheduling.Domain.ScheduleDayAggregate.Rules;
using System;
using System.Collections.Generic;

namespace Scheduling.Domain.ScheduleDayAggregate
{
    public class ScheduleDay : Entity<Guid>, IAggregateRoot 
    {
        public DateTime Day { get; }
        public bool Available { get; private set; }
        public ContactInformation ContactInformation { get; private set; }

        private readonly List<Appointment> _appointments;
        public IReadOnlyCollection<Appointment> Appointments => _appointments;

        protected ScheduleDay() 
        {
            this._appointments = new List<Appointment>();
        }

        public void CreateAppointment(AppointmentTimeSlot appointmentTimeSlot)
        {
            this.CheckBusinessRule(new AppointmentTimeSlotNotTaken(this.Appointments, appointmentTimeSlot));

            this._appointments.Add(Appointment.CreateNew(appointmentTimeSlot));
        }
    }
}
