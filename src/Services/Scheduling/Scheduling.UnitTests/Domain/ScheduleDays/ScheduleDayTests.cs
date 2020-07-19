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
                    WorkHours.Create(20, 60*9, 60*13, string.Empty),
                    WorkHours.Create(20, 60*13, 60*17, string.Empty)
                },
                true);

            Assert.NotNull(scheduleDay);
        }
        [Fact]
        public void create_reccuring_staff_schedule_day_success()
        {
            var scheduleDay = ScheduleDay.CreateReccuringWeeklySchedule(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Scheduling.Domain.ScheduleDays.DayOfWeek.Monday,
                new List<WorkHours>
                {
                    WorkHours.Create(20, 60*9, 60*17, string.Empty)
                },
                true);

            Assert.NotNull(scheduleDay);
        }
        [Fact]
        public void create_one_time_schedule_day_success()
        {
            var scheduleDay = ScheduleDay.CreateOneTimeSchedule(
                Guid.NewGuid(),
                null,
                DateTime.UtcNow.Date,
                new List<WorkHours>
                {
                    WorkHours.Create(20, 60*9, 60*17, string.Empty)
                },
                false);

            Assert.NotNull(scheduleDay);
        }
        [Fact]
        public void create_one_time_staff_schedule_day_success()
        {
            var scheduleDay = ScheduleDay.CreateOneTimeSchedule(
                Guid.NewGuid(),
                Guid.NewGuid(),
                DateTime.UtcNow.Date,
                new List<WorkHours>
                {
                    WorkHours.Create(20, 60*9, 60*17, string.Empty)
                },
                false);

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


        [Fact]
        public void adding_appointment_to_reccuring_schedule_day_fails_with_appointment_not_in_workhours()
        {
            var scheduleDay = ValidReccuringScheduleDay(true);
            var invalidTimeSlot = new AppointmentTimeSlot(DateTime.UtcNow.Date, 12 * 60 + 20);
            var contactInformation = new ContactInformation(string.Empty, string.Empty, string.Empty, string.Empty);

            Assert.Throws<Exception>(() => scheduleDay.CreateAppointment(invalidTimeSlot, contactInformation));
        }

        [Fact]
        public void adding_appointment_to_reccuring_schedule_day_fails_because_of_appointment_overlap()
        {
            var scheduleDay = ValidReccuringScheduleDay(true);

            var timeslot1 = new AppointmentTimeSlot(DateTime.UtcNow.Date, 11 * 60);
            var timeslot2 = new AppointmentTimeSlot(DateTime.UtcNow.Date, 11 * 60);

            var contactInformation = new ContactInformation(string.Empty, string.Empty, string.Empty, string.Empty);
            
            scheduleDay.CreateAppointment(timeslot1, contactInformation);

            Assert.Throws<Exception>(() => scheduleDay.CreateAppointment(timeslot2, contactInformation));
        }


        private ScheduleDay ValidReccuringScheduleDay(bool canSelectSchedule)
        {
            return ScheduleDay.CreateReccuringWeeklySchedule(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Scheduling.Domain.ScheduleDays.DayOfWeek.Friday,
                new List<WorkHours>
                {
                    WorkHours.Create(20, 60*9, 60*12, string.Empty),
                    WorkHours.Create(20, 60*13, 60*17, string.Empty)
                },
                canSelectSchedule);
        }
        private ScheduleDay ValidOneTimeScheduleDay(bool canSelectSchedule)
        {
            return ScheduleDay.CreateOneTimeSchedule(
                Guid.NewGuid(),
                Guid.NewGuid(),
                DateTime.UtcNow.Date,
                new List<WorkHours>
                {
                    WorkHours.Create(20, 60*9, 60*12, string.Empty),
                    WorkHours.Create(20, 60*13, 60*17, string.Empty)
                },
                canSelectSchedule);
        }
    }
}
