using BuildingBlocks.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Scheduling.Domain.ScheduleDays.Rules
{
    public class AppointmentTimeSlotIsValid : IBusinessRule
    {
        private readonly IReadOnlyCollection<Appointment> _appointments;
        private readonly IReadOnlyCollection<WorkHours> _workHours;
        private readonly AppointmentTimeSlot _appointmentTimeSlot;

        public AppointmentTimeSlotIsValid(
            List<Appointment> appointments,
            List<WorkHours> workHours, 
            AppointmentTimeSlot appointmentTimeSlot)
        {
            this._workHours = workHours;
            this._appointments = appointments;
            this._appointmentTimeSlot = appointmentTimeSlot;
        }

        public bool IsValid()
        {
            var workHours = WorkHoursRuleHelper.GetAppointmentTimeSlotWorkHours(this._workHours, this._appointmentTimeSlot);

            // Appointment not in workhours
            if (workHours == null)
            {
                return false;
            }

            // Time slots client can create appointment
            var workHourTimeSlots = new List<int>();

            // populate workHourTimeSlots with all the possible appointment time slots within workhours
            for (int i = 0; i < (workHours.WorkHoursEnd - workHours.WorkHoursStart) / workHours.AppointmentLength; i++)
            {
                workHourTimeSlots.Add(workHours.WorkHoursStart + i * workHours.AppointmentLength.Value);
            }

            // Check if appointment is in one of the possible work hours time slots
            if (!workHourTimeSlots.Any(x => x == this._appointmentTimeSlot.AppointmentStart))
            {
                return false;
            }

            // Check if there isn't another appointment created and preset with the same appointment time slot
            if (this._appointments.Any(x => x.AppointmentTimeSlot.AppointmentStart == this._appointmentTimeSlot.AppointmentStart))
            {
                return false;
            }

            return true;
        }

        public string Message => "Appointment time slot is not in schedule day work hours";
    }
}
