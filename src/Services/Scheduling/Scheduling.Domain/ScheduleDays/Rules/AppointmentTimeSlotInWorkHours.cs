using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            var timeSlotWorkHours = this._workHours
                .FirstOrDefault(x => x.WorkHoursStart < this._appointmentTimeSlot.AppointmentStart && x.WorkHoursEnd > this._appointmentTimeSlot.AppointmentEnd);

            // Check if appointment has have valid time slot, if it is in workhours for the day
            if (timeSlotWorkHours == null) 
                return false;

            // Check if appointment time slot length matches to work hours appointment length
            if (timeSlotWorkHours.AppointmentLengthInMinutes != this._appointmentTimeSlot.AppointmentEnd - this._appointmentTimeSlot.AppointmentStart)
                return false;

            // Cannot create appointment after the workhours end
            if (this._appointmentTimeSlot.AppointmentEnd + timeSlotWorkHours.AppointmentLengthInMinutes >= timeSlotWorkHours.WorkHoursEnd)
                return false;

            return true;
        }

        public string Message => "Appointment time slot is not in schedule day work hours";
    }
}
