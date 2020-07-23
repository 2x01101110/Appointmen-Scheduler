using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.Idempotency;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using Scheduling.Domain.ScheduleDays;
using Scheduling.Domain.Services;
using Scheduling.Domain.Staff;
using Scheduling.Infrastructure.Configuration.EntityTypeConfiguration;
using System;
using System.Data;
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

        public DbSet<Staff> Staff { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ScheduleDay> ScheduleDays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ScheduleDayEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StaffEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new CommandRequestEntityTypeConfiguration());
        }
        
        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }


        private IDbContextTransaction _currentTransaction;
        public IDbContextTransaction CurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;
        public async Task<IDbContextTransaction> BeginTransaction()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (transaction != _currentTransaction)
            {
                throw new InvalidOperationException("Transaction is not valid");
            }

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                DisposeTransaction();
            }
        }
        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                DisposeTransaction();
            }
        }
        private void DisposeTransaction()
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public class SchedulingContextDesignFactory : IDesignTimeDbContextFactory<SchedulingContext>
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
