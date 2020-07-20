using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Scheduling.Domain.ScheduleDays;
using Scheduling.Domain.Services;
using Scheduling.Domain.Staff;
using System.Collections.Generic;

namespace Scheduling.Infrastructure.Configuration.EntityTypeConfiguration
{
    public class ScheduleDayEntityTypeConfiguration : IEntityTypeConfiguration<ScheduleDay>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ScheduleDay> builder)
        {
            builder.ToTable("Schedule");
            builder.HasKey(x => x.Id);

            builder.HasOne<Service>()
                .WithOne()
                .HasForeignKey<ScheduleDay>(x => x.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Staff>()
                .WithOne()
                .HasForeignKey<ScheduleDay>(x => x.StaffId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.CalendarDay)
                .HasColumnName("CalendarDay");

            builder.Property(x => x.DayOfWeek)
                .HasColumnName("DayOfWeek");

            builder.Property(x => x.ClientCanSelectTimeSlot)
                .HasColumnName("ClientCanSelectTimeSlot");

            builder.Property(x => x.WorkHours)
                .HasConversion(
                    w => JsonConvert.SerializeObject(w),
                    w => JsonConvert.DeserializeObject<IReadOnlyCollection<WorkHours>>(w)
                );
                //.Metadata.SetValueComparer(null);

            builder.OwnsMany<Appointment>("Appointments", x =>
            {
                x.ToTable("Appointments");
                x.WithOwner().HasForeignKey("ScheduleDayId");
                x.Ignore(x => x.DomainEvents);

                x.OwnsOne(y => y.AppointmentTimeSlot, z =>
                {
                    z.Property(p => p.AppointmentDay)
                        .HasColumnName("AppointmentDay");
                    z.Property(p => p.AppointmentStart)
                        .HasColumnName("AppointmentStart")
                        .IsRequired(false);
                });

                x.OwnsOne(y => y.ContactInformation, z =>
                {
                    z.Property(p => p.Email)
                        .HasColumnName("Email")
                        .IsRequired(true);

                    z.Property(p => p.FirstName)
                        .HasColumnName("FirstName")
                        .IsRequired(true);

                    z.Property(p => p.LastName)
                        .HasColumnName("LastName")
                        .IsRequired(true);

                    z.Property(p => p.Phone)
                        .HasColumnName("PhoneNumber")
                        .IsRequired(true);
                });

                x.OwnsOne(y => y.AppointmentStatus, z =>
                {
                    z.Property(p => p.Status)
                        .HasColumnName("Status")
                        .IsRequired(true);
                });
            });
        }
    }
}
