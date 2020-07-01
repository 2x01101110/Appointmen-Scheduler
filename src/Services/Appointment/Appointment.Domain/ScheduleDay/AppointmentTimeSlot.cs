using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;

namespace Appointment.Domain.ScheduleDay
{
    public class AppointmentTimeSlot : ValueObject
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public AppointmentTimeSlot(DateTime start, DateTime end)
        {
            this.Start = start;
            this.End = end;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Start;
            yield return this.End;
        }
    }
}
