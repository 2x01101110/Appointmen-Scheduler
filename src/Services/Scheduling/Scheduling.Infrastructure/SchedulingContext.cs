using BuildingBlocks.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Scheduling.Domain.ScheduleDayAggregate;
using Scheduling.Infrastructure.Domain.ScheduleDayAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure
{
    public class SchedulingContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public SchedulingContext(DbContextOptions<SchedulingContext> options) : base(options)
        {

        }

        public SchedulingContext(DbContextOptions<SchedulingContext> options, IMediator mediator) : base(options)
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
