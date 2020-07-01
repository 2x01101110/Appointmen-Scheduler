using BuildingBlocks.Domain;
using System;
using System.Threading.Tasks;

namespace Scheduling.Domain.ScheduleDayAggregate
{
    public interface IScheduleDayRepository : IRepository<ScheduleDay>
    {
        Task<ScheduleDay> FindByDayAsync(DateTime day);
        Task<ScheduleDay> FindByIdAsync(Guid id);
        Task UpdateAsync(ScheduleDay scheduleDay);
    }
}
