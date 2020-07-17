using BuildingBlocks.Domain;
using System;
using System.Threading.Tasks;

namespace Scheduling.Domain.ScheduleDays
{
    public interface IScheduleDayRepository : IRepository<ScheduleDay>
    {
        void AddScheduleDay(ScheduleDay scheduleDay);
        void UpdateAsync(ScheduleDay scheduleDay);

        Task<ScheduleDay> FindByDayAsync(DateTime day);
        Task<ScheduleDay> FindByIdAsync(Guid id);
    }
}
