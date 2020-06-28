using BuildingBlocks.Domain;
using System.Collections.Generic;

namespace Appointment.Domain.Appointments
{
    public class AppointmentStatus : ValueObject
    {
        public static AppointmentStatus ConfirmationPending => new AppointmentStatus("ApprovalPending");
        public static AppointmentStatus Confirmed => new AppointmentStatus("Confirmed");
        public static AppointmentStatus Canceled(string canceledReason) 
        {
            return new AppointmentStatus("Canceled", canceledReason);
        }

        public string Status { get; }
        public string StatusDetails { get; }

        private AppointmentStatus(string status, string statusDetails = null)
        {
            this.Status = status;
            if (!string.IsNullOrEmpty(statusDetails)) 
            {
                this.StatusDetails = statusDetails;
            }
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Status;
            yield return this.StatusDetails;
        }
    }
}
