﻿using BuildingBlocks.Domain;
using Scheduling.Domain.ScheduleDays.Rules;
using System.Collections.Generic;

namespace Scheduling.Domain.ScheduleDays
{
    public class WorkHours : ValueObject
    {
        public int? AppointmentLength { get; }
        public int WorkHoursStart { get; }
        public int WorkHoursEnd { get; }
        public string Note { get; set; }

        private WorkHours(int? appointmentLenght, int start, int end, string note)
        {
            this.CheckBusinessRule(new WorkHoursHaveStartEndAndLengthTimes(start, end, appointmentLenght));

            this.AppointmentLength = appointmentLenght;
            this.WorkHoursStart = start;
            this.WorkHoursEnd = end;
            this.Note = note;
        }

        public static WorkHours Create(int? appointmentLength, int workHoursStart, int workHoursEnd, string note)
        {
            return new WorkHours(appointmentLength, workHoursStart, workHoursEnd, note);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return this.AppointmentLength;
            yield return this.WorkHoursStart;
            yield return this.WorkHoursEnd;
            yield return this.Note;
        }
    }
}
