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
            var appointmentTimeSlotWorkHours = this._workHours
                .FirstOrDefault(x => x.WorkHoursStart >= this._appointmentTimeSlot.AppointmentStart &&
                    this._appointmentTimeSlot.AppointmentStart + x.AppointmentLength < x.WorkHoursEnd);

            if (appointmentTimeSlotWorkHours == null) return false;

            var appointmentDuration = appointmentTimeSlotWorkHours.AppointmentLength;

            return !this._appointments
                .Any(x => 
                    x.AppointmentTimeSlot.AppointmentStart > this._appointmentTimeSlot.AppointmentStart &&
                    x.AppointmentTimeSlot.AppointmentStart + appointmentDuration < this._appointmentTimeSlot.AppointmentStart + appointmentDuration ||
                    x.AppointmentTimeSlot.AppointmentStart == this._appointmentTimeSlot.AppointmentStart &&
                    x.AppointmentTimeSlot.AppointmentStart + appointmentDuration == this._appointmentTimeSlot.AppointmentStart + appointmentDuration
                );
        }
        
        public string Message => "Appointments are overlapping";
    }
}
