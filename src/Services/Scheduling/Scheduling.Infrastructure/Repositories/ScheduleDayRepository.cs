using BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore;
using Scheduling.Domain.ScheduleDays;
using System;
using System.Linq;
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

        public void AddScheduleDay(ScheduleDay scheduleDay)
        {
            this._context.ScheduleDays.Add(scheduleDay);
        }

        public async Task<ScheduleDay> FindScheduleDayByDayAsync(DateTime day)
        {
            var dayOfWeek = (int)day.Date.DayOfWeek;

            return await _context.ScheduleDays
                .Where(x => x.CalendarDay == day || x.DayOfWeek == dayOfWeek)
                .Include(x => x.Appointments)
                // Order descending by Calendar Day so the firs result always is day schedule override (if present)
                .OrderByDescending(x => x.CalendarDay)
                .FirstOrDefaultAsync();
        }

        public async Task<ScheduleDay> FindScheduleDayByIdAsync(Guid id)
        {
            return await _context
                .ScheduleDays
                .Include(x => x.Appointments)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void UpdateScheduleDay(ScheduleDay scheduleDay)
        {
            _context.Entry(scheduleDay).State = EntityState.Modified;
        }
    }
}
