using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduling.Domain.Services;
using System;

namespace Scheduling.Infrastructure.Configuration.EntityTypeConfiguration
{
    public class ServiceEntityTypeConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services");
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);

            builder.Property(x => x.Name).HasColumnName("Name");
        }
    }
}
