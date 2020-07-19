using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Domain.ScheduleDays.Rules
{
    class AppointmentTimeSlotNotOverlapping : IBusinessRule
    {
        private readonly IReadOnlyCollection<WorkHours> _workHours;
        private readonly IReadOnlyCollection<Appointment> _appointments;
        private readonly AppointmentTimeSlot _appointmentTimeSlot;

        public AppointmentTimeSlotNotOverlapping(
            List<WorkHours> workHours,
            List<Appointment> appointments, 
            AppointmentTimeSlot appointmentTimeSlot)
        {
            this._workHours = workHours;
            this._appointments = appointments;
            this._appointmentTimeSlot = appointmentTimeSlot;
        }

        public bool IsValid()
        {
            // Select work hours for the new appointment
            var appointmentTimeSlotWorkHours = this._workHours
                .FirstOrDefault(x => this._appointmentTimeSlot.AppointmentStart <= x.WorkHoursEnd &&
                    this._appointmentTimeSlot.AppointmentStart + x.AppointmentLength >= x.WorkHoursStart);

            // Null means new appointment is not withing work hours
            if (appointmentTimeSlotWorkHours == null)
            {
                return false;
            }

            var appointmentDuration = appointmentTimeSlotWorkHours.AppointmentLength;

            // check if trying to insert duplicate appointment - matching start time slot times
            if (this._appointments.Any(x => x.AppointmentTimeSlot.AppointmentStart == this._appointmentTimeSlot.AppointmentStart))
            {
                return false;
            }

            return true;
        }
        
        public string Message => "Appointments are overlapping";
    }
}
