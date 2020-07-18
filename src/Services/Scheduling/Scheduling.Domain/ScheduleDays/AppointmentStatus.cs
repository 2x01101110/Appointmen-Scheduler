using BuildingBlocks.Domain;
using System.Collections.Generic;

namespace Scheduling.Domain.ScheduleDays
{
    public class AppointmentStatus : ValueObject
    {
        public static AppointmentStatus StaffConfirmationPending => new AppointmentStatus("StaffConfirmationPending");
        public static AppointmentStatus ClientConfirmationPending => new AppointmentStatus("ClientConfirmationPending");
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
