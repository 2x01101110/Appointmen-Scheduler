using Scheduling.Domain.ScheduleDays;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Scheduling.UnitTests.Domain.ScheduleDays
{
    public class WorkHoursTests
    {
        [Fact]
        public void create_workhours_success_with_defined_appointment_length()
        {
            // Arrange
            var appointmentLenght = 20;
            var workHoursStart = 60 * 9;
            var workHoursEnd = 60 * 12;

            // Act
            var workHours = WorkHours.Create(appointmentLenght, workHoursStart, workHoursEnd, string.Empty);

            // Assert
            Assert.NotNull(workHours);
        }

        [Fact]
        public void create_workhours_success_with_undefined_appointment_length()
        {
            int? appointmentLenght = null;
            var workHoursStart = 60 * 9;
            var workHoursEnd = 60 * 12;

            var workHours = WorkHours.Create(appointmentLenght, workHoursStart, workHoursEnd, string.Empty);

            Assert.NotNull(workHours);
        }

        [Fact]
        public void create_workhours_fail_with_equal_start_and_end_times()
        {
            var appointmentLenght = 20;
            var workHoursStart = 60 * 9;
            var workHoursEnd = 60 * 9;

            Assert.Throws<Exception>(() => WorkHours.Create(appointmentLenght, workHoursStart, workHoursEnd, string.Empty));
        }

        [Fact]
        public void create_workhours_fail_with_equal_start_greater_than_end_time()
        {
            var appointmentLenght = 20;
            var workHoursStart = 60 * 10;
            var workHoursEnd = 60 * 9;

            Assert.Throws<Exception>(() => WorkHours.Create(appointmentLenght, workHoursStart, workHoursEnd, string.Empty));
        }

        [Fact]
        public void create_workhours_fail_negative_appointment_length()
        {
            // Arrange
            var appointmentLenght = -1;
            var workHoursStart = 60 * 9;
            var workHoursEnd = 60 * 12;

            // Act - Assert
            Assert.Throws<Exception>(() => WorkHours.Create(appointmentLenght, workHoursStart, workHoursEnd, string.Empty));
        }

        [Fact]
        public void create_workhours_fail_with_appointment_length_greater_than_workhours_duration()
        {
            var appointmentLenght = (60 * 12 - 60 * 1) + 100;
            var workHoursStart = 60 * 9;
            var workHoursEnd = 60 * 12;

            Assert.Throws<Exception>(() => WorkHours.Create(appointmentLenght, workHoursStart, workHoursEnd, string.Empty));
        }
    }
}
