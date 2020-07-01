using BuildingBlocks.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Scheduling.Domain.ScheduleDayAggregate.Rules
{
    public class AppointmentTimeSlotNotTaken : IBusinessRule
    {
        private readonly IReadOnlyCollection<Appointment> _existingAppointments;
        private readonly AppointmentTimeSlot _newAppointmentTimeSLot;

        public AppointmentTimeSlotNotTaken(
            IReadOnlyCollection<Appointment> existingAppointments,
            AppointmentTimeSlot newAppointmentTimeSLot)
        {
            this._existingAppointments = existingAppointments;
            this._newAppointmentTimeSLot = newAppointmentTimeSLot;
        }

        public bool IsValid()
        {
            return !this._existingAppointments.
                    Any(x => x.AppointmentTimeSlot.Start == this._newAppointmentTimeSLot.Start &&
                             x.AppointmentTimeSlot.End == this._newAppointmentTimeSLot.End);
        }

        public string Message => "Appointment already exists with the same time slot.";
    }
}
