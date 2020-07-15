using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.ScheduleDays.Rules
{
    public class AppointmentCannotBeAddedToClosedSchedule : IBusinessRule
    {
        private readonly bool _scheduleOpen;

        public AppointmentCannotBeAddedToClosedSchedule(bool scheduleOpen)
        {
            this._scheduleOpen = scheduleOpen;
        }

        public bool IsValid() => this._scheduleOpen;

        public string Message => "Cannot add a new appointment to closed schedule";
    }
}
