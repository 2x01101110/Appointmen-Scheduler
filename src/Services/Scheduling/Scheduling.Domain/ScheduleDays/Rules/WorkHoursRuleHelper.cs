using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Domain.ScheduleDays.Rules
{
    public static class WorkHoursRuleHelper
    {
        public static WorkHours GetAppointmentTimeSlotWorkHours(
            IReadOnlyCollection<WorkHours> scheduleDayWorkHours, 
            AppointmentTimeSlot appointmentTimeSlot)
        {
            var workHours = scheduleDayWorkHours
                .FirstOrDefault(x => appointmentTimeSlot.AppointmentStart <= x.WorkHoursEnd &&
                    appointmentTimeSlot.AppointmentStart + x.AppointmentLength >= x.WorkHoursStart);

            return workHours;
        }
    }
}
