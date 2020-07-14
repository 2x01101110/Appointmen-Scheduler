using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.ScheduleDayAggregate.Rules
{
    public class WorkHoursHaveStartEndAndLengthTimes : IBusinessRule
    {
        private readonly int _workHoursStart;
        private readonly int _workHoursEnd;
        private readonly int _appointmentLengthInMinutes;

        public WorkHoursHaveStartEndAndLengthTimes(int workHoursStart, int workHoursEnd, int appointmentLengthInMinutes)
        {
            this._workHoursStart = workHoursStart;
            this._workHoursEnd = workHoursEnd;
            this._appointmentLengthInMinutes = appointmentLengthInMinutes;
        }

        public bool IsValid()
        {
            if (this._workHoursStart == this._workHoursEnd ||
                this._workHoursStart > this._workHoursEnd)
                return false;

            if (this._workHoursEnd - this._workHoursStart < this._appointmentLengthInMinutes ||
                this._appointmentLengthInMinutes == 0 ||
                (this._workHoursEnd - this._workHoursStart) % this._appointmentLengthInMinutes != 0)
                return false;

            return true;
        }
        
        public string Message => "Incorrectly defined work hours";
    }
}
