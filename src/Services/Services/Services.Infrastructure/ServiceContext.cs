using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.Idempotency;
using Microsoft.EntityFrameworkCore;
using Services.Domain.Organization;
using Services.Domain.Services;
using Services.Domain.Staff;
using Services.Infrastructure.Configuration.Data.EntityTypeConfiguration;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Infrastructure
{
    public class ServiceContext : DbContext, IUnitOfWork
    {
        public ServiceContext(DbContextOptions<ServiceContext> options) : base(options)
        {

        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Staff> Staff { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrganizationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StaffEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new CommandRequestEntityTypeConfiguration());
        }
        
        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
