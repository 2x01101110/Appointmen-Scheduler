using MediatR;

namespace Scheduling.Domain.ScheduleDays.Events
{
    public class AppointmentCreatedDomainEvent : INotification
    {
        public readonly Appointment Appointment;

        public AppointmentCreatedDomainEvent(Appointment appointment)
        {
            this.Appointment = appointment;
        }
    }
}
