using Microsoft.EntityFrameworkCore;
using Scheduling.Domain.ScheduleDays;
using System;

namespace Scheduling.Infrastructure.Configuration.EntityTypeConfiguration
{
    public class ScheduleDayEntityTypeConfiguration : IEntityTypeConfiguration<ScheduleDay>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ScheduleDay> builder)
        {
            builder.ToTable("Schedule");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CalendarDay);

            builder.Ignore(x => x.DomainEvents);

            builder.HasMany(x => x.Appointments)
                .WithOne()
                .HasForeignKey("ScheduleDayId")
                .OnDelete(DeleteBehavior.Cascade);

            var navigation = builder.Metadata.FindNavigation(nameof(ScheduleDay.Appointments));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
