using BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore;
using Scheduling.Domain.ScheduleDayAggregate;
using System;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Domain.ScheduleDayAggregate
{
    public class ScheduleDayRepository : IScheduleDayRepository
    {
        private readonly SchedulingContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ScheduleDayRepository(SchedulingContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ScheduleDay> FindByDayAsync(DateTime day)
        {
            return await _context.ScheduleDays.FirstOrDefaultAsync();
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
