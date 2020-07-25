using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.Idempotency;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Organization.Domain.Services;
using Organization.Domain.Staff;
using Organization.Infrastructure.Configuration.Data.EntityTypeConfiguration;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.Infrastructure
{
    public class ServiceContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        public ServiceContext(DbContextOptions<ServiceContext> options) : base(options) { }
        public ServiceContext(DbContextOptions<ServiceContext> options, IMediator mediator) : base(options)
        {
            this._mediator = mediator;
        }

        public DbSet<Domain.Organizations.Organization> Organizations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Employee> Staff { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrganizationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StaffEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new CommandRequestEntityTypeConfiguration());
        }
        
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

    public class ServicesContextDesignFactory : IDesignTimeDbContextFactory<ServiceContext>
    {
        public ServiceContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ServiceContext>()
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppointmentScheduler.Services;");

            return new ServiceContext(optionsBuilder.Options, new NoMediator());
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
