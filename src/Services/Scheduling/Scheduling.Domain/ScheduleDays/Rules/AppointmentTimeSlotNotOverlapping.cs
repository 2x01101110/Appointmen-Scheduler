using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Domain.ScheduleDays.Rules
{
    class AppointmentTimeSlotNotOverlapping : IBusinessRule
    {
        private readonly IReadOnlyCollection<Appointment> _appointments;
        private readonly AppointmentTimeSlot _appointmentTimeSlot;

        public AppointmentTimeSlotNotOverlapping(List<Appointment> appointments, AppointmentTimeSlot appointmentTimeSlot)
        {
            this._appointments = appointments;
            this._appointmentTimeSlot = appointmentTimeSlot;
        }

        public bool IsValid()
        {
            return !this._appointments
                .Any(x => 
                    x.AppointmentTimeSlot.AppointmentStart > this._appointmentTimeSlot.AppointmentStart &&
                    x.AppointmentTimeSlot.AppointmentEnd < this._appointmentTimeSlot.AppointmentEnd ||
                    x.AppointmentTimeSlot.AppointmentStart == this._appointmentTimeSlot.AppointmentStart &&
                    x.AppointmentTimeSlot.AppointmentEnd == this._appointmentTimeSlot.AppointmentEnd
                );
        }
        
        public string Message => "Appointments are overlapping";
    }
}
