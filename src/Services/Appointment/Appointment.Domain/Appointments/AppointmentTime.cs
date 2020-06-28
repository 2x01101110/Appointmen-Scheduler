using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appointment.Domain.Appointments
{
    public class AppointmentTime : ValueObject
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public AppointmentTime(DateTime start, DateTime end)
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
