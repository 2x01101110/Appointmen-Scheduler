using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduling.Domain.ScheduleDayAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Infrastructure.Domain.ScheduleDayAggregate
{
    public class AppointmentEntityTypeConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");

            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.DomainEvents);

            builder.OwnsOne(x => x.AppointmentStatus, y =>
            {
                y.Property(p => p.Status)
                    .HasColumnName("Status")
                    .IsRequired(true);
            });

            builder.OwnsOne(x => x.ContactInformation, y =>
            {
                y.Property(p => p.Email)
                    .HasColumnName("Email")
                    .IsRequired(true);

                y.Property(p => p.FirstName)
                    .HasColumnName("FirstName")
                    .IsRequired(true);

                y.Property(p => p.LastName)
                    .HasColumnName("LastName")
                    .IsRequired(true);

                y.Property(p => p.Phone)
                    .HasColumnName("Phone")
                    .IsRequired(true);
            });

            builder.OwnsOne(x => x.AppointmentTimeSlot, y =>
            {
                y.Property(p => p.Start)
                    .HasColumnName("Start")
                    .IsRequired(true);
                y.Property(p => p.End)
                    .HasColumnName("End")
                    .IsRequired(true);
            });
        }
    }
}
