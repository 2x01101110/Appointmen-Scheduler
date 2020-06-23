using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Domain.Appointments
{
    public interface IAppointmentRepository
    {
        Task AddAsync(Appointment appointment);
        Task<Appointment> GetByIdAsync(Guid appointmentId);
    }
}
