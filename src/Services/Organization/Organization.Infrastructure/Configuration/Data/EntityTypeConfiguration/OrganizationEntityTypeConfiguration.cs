using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organization.Infrastructure.Configuration.Data.EntityTypeConfiguration
{
    public class OrganizationEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Organizations.Organization>
    {
        public void Configure(EntityTypeBuilder<Domain.Organizations.Organization> builder)
        {
            builder.ToTable("Organization");
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Name)
                .HasColumnName("Name");
        }
    }
}
