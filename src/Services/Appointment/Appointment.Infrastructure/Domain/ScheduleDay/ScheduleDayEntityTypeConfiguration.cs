using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appointment.Infrastructure.Domain.ScheduleDay
{
    public class ScheduleDayEntityTypeConfiguration : IEntityTypeConfiguration<Appointment.Domain.ScheduleDay.Appointment>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Appointment.Domain.ScheduleDay.Appointment> builder)
        {
            throw new NotImplementedException();
        }
    }
}
