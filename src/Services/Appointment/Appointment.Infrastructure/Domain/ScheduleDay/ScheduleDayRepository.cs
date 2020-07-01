using Appointment.Domain.ScheduleDay;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Infrastructure.Domain.ScheduleDay
{
    public class ScheduleDayRepository : IScheduleDayRepository
    {
        public Task AddAppointmentAsync(Appointment.Domain.ScheduleDay.Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public Task<Appointment.Domain.ScheduleDay.ScheduleDay> FindByDayAsync(DateTime day)
        {
            throw new NotImplementedException();
        }

        public Task<Appointment.Domain.ScheduleDay.ScheduleDay> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Appointment.Domain.ScheduleDay.ScheduleDay scheduleDay)
        {
            throw new NotImplementedException();
        }
    }
}
