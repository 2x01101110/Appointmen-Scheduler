using Autofac;
using Scheduling.Infrastructure.Configuration.Data;
using Scheduling.Infrastructure.Configuration.Mediator;

namespace Scheduling.Infrastructure.Configuration
{
    public class SchedulingConfigurationStartup
    {
        public static void Initialize(
            ContainerBuilder builder,
            string connectionString)
        {
            builder.RegisterModule(new DataAccessModule(connectionString));
            builder.RegisterModule(new MediatorModule());
        }
    }
}
