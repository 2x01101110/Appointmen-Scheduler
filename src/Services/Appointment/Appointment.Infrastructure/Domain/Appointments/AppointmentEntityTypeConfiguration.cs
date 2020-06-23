using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointment.Infrastructure.Domain.Appointments
{
    class AppointmentEntityTypeConfiguration : IEntityTypeConfiguration<Appointment.Domain.Appointments.Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment.Domain.Appointments.Appointment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.domainEvents);

            builder.OwnsOne(x => x.ContactInformation, y => 
            {
                y.Property(p => p.FirstName)
                    .HasColumnName("FirstName")
                    .IsRequired(true);
                y.Property(p => p.LastName)
                    .HasColumnName("LastName")
                    .IsRequired(true);
                y.Property(p => p.Phone)
                    .HasColumnName("Phone");
                y.Property(p => p.Email)
                    .HasColumnName("Email")
                    .IsRequired(true);
            });

            builder.OwnsOne(x => x.AppointmentStatus, y =>
            {
                y.Property(p => p.Status)
                    .HasColumnName("Status");
            });
        }
    }
}
