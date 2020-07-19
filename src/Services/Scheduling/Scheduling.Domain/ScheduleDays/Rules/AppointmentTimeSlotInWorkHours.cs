using BuildingBlocks.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Scheduling.Domain.ScheduleDays.Rules
{
    public class AppointmentTimeSlotInWorkHours : IBusinessRule
    {
        private readonly IReadOnlyCollection<WorkHours> _workHours;
        private readonly AppointmentTimeSlot _appointmentTimeSlot;

        public AppointmentTimeSlotInWorkHours(List<WorkHours> workHours, AppointmentTimeSlot appointmentTimeSlot)
        {
            this._workHours = workHours;
            this._appointmentTimeSlot = appointmentTimeSlot;
        }

        public bool IsValid()
        {
            var result = this._workHours
                .FirstOrDefault(x => this._appointmentTimeSlot.AppointmentStart <= x.WorkHoursEnd &&
                    this._appointmentTimeSlot.AppointmentStart + x.AppointmentLength >= x.WorkHoursStart);

            if (result == null)
            {
                return false;
            }

            if (result.WorkHoursEnd < _appointmentTimeSlot.AppointmentStart + result.AppointmentLength)
            {
                return false;
            }

            return true;
        }

        public string Message => "Appointment time slot is not in schedule day work hours";
    }
}
