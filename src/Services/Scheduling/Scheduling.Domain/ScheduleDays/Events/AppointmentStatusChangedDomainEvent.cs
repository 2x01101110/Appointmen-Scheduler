using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.ScheduleDays.Events
{
    public class AppointmentStatusChangedDomainEvent : INotification
    {
        public Appointment Appointment { get; set; }

        public AppointmentStatusChangedDomainEvent(Appointment appointment)
        {
            this.Appointment = appointment;
        }
    }
}
