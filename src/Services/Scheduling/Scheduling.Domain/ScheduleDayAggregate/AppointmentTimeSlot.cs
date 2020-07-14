using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;

namespace Scheduling.Domain.ScheduleDayAggregate
{
    public class AppointmentTimeSlot : ValueObject
    {
        public int AppointmentStart { get; }
        public int AppointmentEnd { get; }

        public AppointmentTimeSlot(int appointmentStart, int appointmentEnd)
        {
            this.AppointmentStart = appointmentStart;
            this.AppointmentEnd = appointmentEnd;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return this.AppointmentStart;
            yield return this.AppointmentEnd;
        }
    }
}
