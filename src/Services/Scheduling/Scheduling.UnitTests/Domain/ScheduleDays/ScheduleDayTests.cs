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
        // Creating reccuring schedule day
        public void create_reccuring_schedule_day_success()
        { 
            var scheduleDay = ValidReccuringScheduleDay(true);
            Assert.NotNull(scheduleDay);
        }

        [Fact]
        // Creating one time schdule
        public void create_one_time_schedule_day_success()
        {
            var scheduleDay = ValidOneTimeScheduleDay(true);
            Assert.NotNull(scheduleDay);
        }

        [Fact]
        // Cannot create a new appointment for the schedule day if new appointment day (datetime) does not match schedule day (datetime)
        public void cannot_create_new_appointment_with_incorrect_day_for_schedule_day()
        {
            var scheduleDay = ValidReccuringScheduleDay(true);

            Assert.Throws<Exception>(() =>
                scheduleDay.CreateAppointment(new AppointmentTimeSlot(
                    // Passing invalid date
                    DateTime.UtcNow.AddDays(+1).Date, 60 * 9 + 20),
                    ValidContactInformation())
                );
        }

        [Fact]
        // Cannot create a new appointment with invalid time slot - time slot is not within workhours/invalid start time
        public void cannot_create_new_appointment_with_invalid_time_slot()
        {
            var scheduleDay = ValidReccuringScheduleDay(true);

            Assert.Throws<Exception>(() =>
                scheduleDay.CreateAppointment(new AppointmentTimeSlot(
                    DateTime.UtcNow.Date, 60 * 19),
                    ValidContactInformation())
                );

            Assert.Throws<Exception>(() =>
                scheduleDay.CreateAppointment(new AppointmentTimeSlot(
                    DateTime.UtcNow.Date, 9 * 61),
                    ValidContactInformation())
                );
        }

        [Fact]
        // Cannot create a new appointment if there is already appointment for the schedule day in the same time slot
        public void cannot_create_new_appointment_because_of_existing_appointment_with_same_timeslot()
        {
            var scheduleDay = ValidReccuringScheduleDay(true);

            scheduleDay.CreateAppointment(new AppointmentTimeSlot(
                    DateTime.UtcNow.Date, 60 * 9 + 20),
                    ValidContactInformation());

            Assert.Throws<Exception>(() =>
                scheduleDay.CreateAppointment(new AppointmentTimeSlot(
                    DateTime.UtcNow.Date, 60 * 9 + 20),
                    ValidContactInformation())
                );
        }

        [Fact]
        // Cannot create aa new appointment if provided timeslot is null or 
        public void cannot_create_new_appointment_with_invalid_or_null_appointment_time_slot()
        {
            var scheduleDay = ValidReccuringScheduleDay(true);

            Assert.Throws<Exception>(() => scheduleDay.CreateAppointment(null, ValidContactInformation()));
        }

        [Fact]
        // cannot create anew appointment when client provides appointment time slot but time slot is not selectable for schedule day
        public void cannot_create_new_appointment_if_time_slot_provided_but_time_slot_not_selectable()
        {
            var scheduleDay = ValidReccuringScheduleDay(false);

            Assert.Throws<Exception>(() =>
                scheduleDay.CreateAppointment(new AppointmentTimeSlot(
                    // Passing invalid date
                    DateTime.UtcNow.Date, 60 * 9 + 20),
                    ValidContactInformation())
                );
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

        private ContactInformation ValidContactInformation()
        {
            return new ContactInformation
            (
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty
            );
        }
    }
}
