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
            if (this._appointmentLenght != null)
            {
                // Appointment duration cannot be equal or less than 0
                if (this._appointmentLenght.Value <= 0) 
                    return false;

                // Must fit all appointment intervals into workhours
                // Appointment length cannot be greater than work hours length
                var duration = this._workHoursEnd - this._workHoursStart;
                if (duration < this._appointmentLenght.Value || duration % this._appointmentLenght.Value != 0)
                    return false;
            }

            // Work hours start and end cannot be equal
            // Workhour start cannot be greater than work hour end
            if (this._workHoursStart == this._workHoursEnd || this._workHoursStart > this._workHoursEnd)
                return false;

            return true;
        }
        
        public string Message => "Incorrectly defined work hours";
    }
}
