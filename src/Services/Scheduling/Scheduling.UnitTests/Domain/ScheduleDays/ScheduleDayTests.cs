using Scheduling.Domain.ScheduleDays;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Scheduling.UnitTests.Domain.ScheduleDays
{
    public class ScheduleDayTests
    {
        [Fact]
        public void create_reccuring_schedule_day_success()
        {
            var scheduleDay = ScheduleDay.CreateReccuringWeeklySchedule(
                Guid.NewGuid(),
                null,
                Scheduling.Domain.ScheduleDays.DayOfWeek.Monday,
                new List<WorkHours>
                {
                    WorkHours.Create(20, 60*9, 60*17, string.Empty)
                },
                true);

            Assert.NotNull(scheduleDay);
        }

        [Fact]
        public void create_reccuring_schedule_day_fail_with_overlapping_workhours()
        {
            Assert.Throws<Exception>(() => ScheduleDay.CreateReccuringWeeklySchedule(
                Guid.NewGuid(),
                null,
                Scheduling.Domain.ScheduleDays.DayOfWeek.Monday,
                new List<WorkHours>
                {
                    WorkHours.Create(20, 540, 1020, string.Empty),
                    WorkHours.Create(20, 960, 1080, string.Empty)
                },
                true));
        }
    }
}
