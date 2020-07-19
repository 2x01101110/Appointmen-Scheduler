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
            return !this._workHours
                .Any(x => x.WorkHoursStart >= this._appointmentTimeSlot.AppointmentStart &&
                    this._appointmentTimeSlot.AppointmentStart + x.AppointmentLength < x.WorkHoursEnd);

            //// Check if appointment has have valid time slot, if it is in workhours for the day
            //if (appointmentTimeSlotWorkHours == null) return false;

            //// Check if appointment time slot length matches to work hours appointment length
            ////if (appointmentTimeSlotWorkHours.AppointmentLength != this._appointmentTimeSlot.AppointmentEnd - this._appointmentTimeSlot.AppointmentStart)
            ////    return false;

            ////// Cannot create appointment after the workhours end
            ////if (this._appointmentTimeSlot.AppointmentEnd + timeSlotWorkHours.AppointmentLength >= timeSlotWorkHours.WorkHoursEnd)
            ////    return false;

            //return true;
        }

        public string Message => "Appointment time slot is not in schedule day work hours";
    }
}
