using BuildingBlocks.Domain;
using System;
using System.Threading.Tasks;

namespace Scheduling.Domain.ScheduleDays
{
    public interface IScheduleDayRepository : IRepository<ScheduleDay>
    {
        void AddScheduleDay(ScheduleDay scheduleDay);
        void UpdateScheduleDay(ScheduleDay scheduleDay);

        Task<ScheduleDay> FindScheduleDayByDayAsync(DateTime day);
        Task<ScheduleDay> FindScheduleDayByIdAsync(Guid id);
    }
}
