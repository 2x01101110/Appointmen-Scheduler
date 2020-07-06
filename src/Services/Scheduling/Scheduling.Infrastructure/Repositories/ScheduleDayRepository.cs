using BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore;
using Scheduling.Domain.ScheduleDayAggregate;
using System;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Repositories
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
            return await _context
                .ScheduleDays
                .Include(x => x.Appointments)
                .FirstOrDefaultAsync(x => x.Day == day);
        }

        public async Task<ScheduleDay> FindByIdAsync(Guid id)
        {
            return await _context
                .ScheduleDays
                .Include(x => x.Appointments)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void UpdateAsync(ScheduleDay scheduleDay)
        {
            _context.Entry(scheduleDay).State = EntityState.Modified;
        }
    }
}
