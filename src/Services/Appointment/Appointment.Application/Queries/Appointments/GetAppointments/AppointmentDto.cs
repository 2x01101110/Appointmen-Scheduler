using System;
using System.Collections.Generic;
using System.Text;

namespace Appointment.Application.Queries.Appointments.GetAppointments
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }
}
