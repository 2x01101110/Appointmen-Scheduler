using Appointment.Application.Queries.Appointments.GetAppointments;
using MediatR;
using System.Collections.Generic;

namespace Appointment.Application.Queries.Appointments.GetAppointments
{
    public class GetAppointmentsQuery : IRequest<List<AppointmentDto>>
    {
    }
}
