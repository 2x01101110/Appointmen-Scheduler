using BuildingBlocks.Domain;
using System;
using System.Threading.Tasks;

namespace Appointment.Domain.ScheduleDay
{
    public interface IScheduleDayRepository : IRepository<ScheduleDay>
    {
        Task<ScheduleDay> FindByDayAsync(DateTime day);
        Task<ScheduleDay> FindByIdAsync(Guid id);
        Task AddAppointmentAsync(Appointment appointment);
        Task UpdateAsync(ScheduleDay scheduleDay);
    }
}
