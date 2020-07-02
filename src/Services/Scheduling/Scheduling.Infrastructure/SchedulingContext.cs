using BuildingBlocks.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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
            modelBuilder.ApplyConfiguration(new AppointmentEntityTypeConfiguration());
        }
        
        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }

    public class OrderingContextDesignFactory : IDesignTimeDbContextFactory<SchedulingContext>
    {
        public SchedulingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SchedulingContext>()
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppointmentScheduler;" +
                "Integrated Security=True;Connect Timeout=30;");

            return new SchedulingContext(optionsBuilder.Options, new NoMediator());
        }

        class NoMediator : IMediator
        {
            public Task Publish(object notification, CancellationToken cancellationToken = default)
            {
                throw new System.NotImplementedException();
            }

            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult(default(TResponse));
            }

            public Task<object> Send(object request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult(default(object));
            }
        }
    }
}
