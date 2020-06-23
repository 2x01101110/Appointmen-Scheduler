using Appointment.Infrastructure.Domain.Appointments;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Infrastructure
{
    public class AppointmentContext : DbContext
    {
        public AppointmentContext(DbContextOptions<AppointmentContext> options) : base(options)
        {

        }

        public DbSet<Appointment.Domain.Appointments.Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppointmentEntityTypeConfiguration());
        }
    }
}
