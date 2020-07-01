using Microsoft.EntityFrameworkCore;
using Scheduling.Domain.ScheduleDayAggregate;
using System;

namespace Scheduling.Infrastructure.Domain.ScheduleDayAggregate
{
    public class ScheduleDayEntityTypeConfiguration : IEntityTypeConfiguration<ScheduleDay>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ScheduleDay> builder)
        {
            throw new NotImplementedException();
        }
    }
}
