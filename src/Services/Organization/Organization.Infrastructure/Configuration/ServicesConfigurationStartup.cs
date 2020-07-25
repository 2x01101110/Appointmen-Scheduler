using Autofac;
using Organization.Infrastructure.Configuration.Data;
using Organization.Infrastructure.Configuration.Mediator;

namespace Organization.Infrastructure.Configuration
{
    public static class ServicesConfigurationStartup
    {
        public static void Initialize(ContainerBuilder builder, string connectionString)
        {
            builder.RegisterModule(new DataAccessModule(connectionString));
            builder.RegisterModule(new MediatorModule());
        }
    }
}
