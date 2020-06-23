using Appointment.Infrastructure.Configuration.Data;
using Appointment.Infrastructure.Configuration.Mediator;
using Autofac;

namespace Appointment.Infrastructure.Configuration
{
    public class AppointmentConfigurationStartup
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
