using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.ScheduleDays.Rules
{
    public class WorkHoursHaveStartEndAndLengthTimes : IBusinessRule
    {
        private readonly int _workHoursStart;
        private readonly int _workHoursEnd;
        private readonly int? _appointmentLenght;

        public WorkHoursHaveStartEndAndLengthTimes(int workHoursStart, int workHoursEnd, int? appointmentLenght)
        {
            this._workHoursStart = workHoursStart;
            this._workHoursEnd = workHoursEnd;
            this._appointmentLenght = appointmentLenght;
        }

        public bool IsValid()
        {
            if (this._workHoursStart == this._workHoursEnd ||
                this._workHoursStart > this._workHoursEnd)

                return false;

            if (this._appointmentLenght != null)
            {
                if (this._workHoursEnd - this._workHoursStart < this._appointmentLenght || 
                    this._appointmentLenght == 0 || (this._workHoursEnd - this._workHoursStart) % this._appointmentLenght != 0)
                    return false;
            }

            return true;
        }
        
        public string Message => "Incorrectly defined work hours";
    }
}
