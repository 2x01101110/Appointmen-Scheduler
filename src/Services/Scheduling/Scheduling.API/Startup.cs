using Appointment.Infrastructure.Configuration;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Appointment.API
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<AppointmentContext>(options =>
            //{
            //    options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppointmentScheduler;Integrated Security=True;Connect Timeout=30;", 
            //        sqlServerOptionsAction: sqlOptions =>
            //    {
            //        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
            //    });
            //},
            //    ServiceLifetime.Scoped
            //);

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppointmentScheduler;" +
                "Integrated Security=True;Connect Timeout=30;";

            AppointmentConfigurationStartup.Initialize(builder, connectionString);
        }
    }
}
