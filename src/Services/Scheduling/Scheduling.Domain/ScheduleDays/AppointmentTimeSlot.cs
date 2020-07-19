using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;

namespace Scheduling.Domain.ScheduleDays
{
    public class AppointmentTimeSlot : ValueObject
    {
        public DateTime AppointmentDay { get; }
        public int? AppointmentStart { get; }

        public AppointmentTimeSlot(DateTime appointmentDay, int appointmentStart)
        {
            this.AppointmentDay = appointmentDay;
            this.AppointmentStart = appointmentStart;
        }

        public AppointmentTimeSlot(DateTime appointmentDay)
        {
            this.AppointmentDay = appointmentDay;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return this.AppointmentDay;
            yield return this.AppointmentStart;
        }
    }
}
