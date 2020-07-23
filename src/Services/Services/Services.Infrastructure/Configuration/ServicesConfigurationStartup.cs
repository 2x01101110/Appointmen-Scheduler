using Autofac;
using Services.Infrastructure.Configuration.Data;
using Services.Infrastructure.Configuration.Mediator;

namespace Services.Infrastructure.Configuration
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
