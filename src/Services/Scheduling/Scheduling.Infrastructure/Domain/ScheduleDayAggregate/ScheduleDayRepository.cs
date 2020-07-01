using BuildingBlocks.Domain;
using Scheduling.Domain.ScheduleDayAggregate;
using System;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Domain.ScheduleDayAggregate
{
    public class ScheduleDayRepository : IScheduleDayRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public Task<ScheduleDay> FindByDayAsync(DateTime day)
        {
            throw new NotImplementedException();
        }

        public Task<ScheduleDay> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ScheduleDay scheduleDay)
        {
            throw new NotImplementedException();
        }
    }
}
