using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using BuildingBlocks.Infrastructure;
using BuildingBlocks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Organization.Infrastructure.Configuration.Data
{
    public class DataAccessModule : Autofac.Module
    {
        private readonly string _connectionString;

        public DataAccessModule(string connectionString)
        {
            this._connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", this._connectionString)
                .InstancePerLifetimeScope();

            builder
                .Register(c =>
                {
                    var dbContextOptionsBuilder = new DbContextOptionsBuilder<ServiceContext>();
                    dbContextOptionsBuilder.UseSqlServer(this._connectionString);

                    return new ServiceContext(dbContextOptionsBuilder.Options);
                })
                .AsSelf()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            var infrastructureAssembly = typeof(ServiceContext).Assembly;

            builder.RegisterAssemblyTypes(infrastructureAssembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());
        }
    }
}
