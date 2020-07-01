using Appointment.Domain.ScheduleDay;
using Appointment.Infrastructure.Domain.ScheduleDay;
using BuildingBlocks.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Appointment.Infrastructure
{
    public class AppointmentContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public AppointmentContext(DbContextOptions<AppointmentContext> options) : base(options)
        {

        }

        public AppointmentContext(DbContextOptions<AppointmentContext> options, IMediator mediator) : base(options)
        {
            this._mediator = mediator;
        }

        public DbSet<ScheduleDay> ScheduleDays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ScheduleDayEntityTypeConfiguration());
        }
        
        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
