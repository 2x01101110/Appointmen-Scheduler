using BuildingBlocks.Domain;
using System.Collections.Generic;

namespace Appointment.Domain.ScheduleDay
{
    public class AppointmentStatus : ValueObject
    {
        public static AppointmentStatus ConfirmationPending => new AppointmentStatus("ApprovalPending");
        public static AppointmentStatus Confirmed => new AppointmentStatus("Confirmed");
        public static AppointmentStatus Canceled => new AppointmentStatus("Canceled");

        public string Status { get; }

        private AppointmentStatus(string status)
        {
            this.Status = status;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Status;
        }
    }
}
