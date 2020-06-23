using Autofac;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Infrastructure.Configuration.Data
{
    public class DataAccessModule : Module
    {
        private readonly string connectionString;

        public DataAccessModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", connectionString)
                .InstancePerLifetimeScope();

            builder
                .Register(c =>
                {
                    var dbContextOptionsBuilder = new DbContextOptionsBuilder<AppointmentContext>();
                    dbContextOptionsBuilder.UseSqlServer(connectionString);

                    return new AppointmentContext(dbContextOptionsBuilder.Options);
                })
                .AsSelf()
                .As<DbContext>()
                .InstancePerLifetimeScope();
        }
    }
}
