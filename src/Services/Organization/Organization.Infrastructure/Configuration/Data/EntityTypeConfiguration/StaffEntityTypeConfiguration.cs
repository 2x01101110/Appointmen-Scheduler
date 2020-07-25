using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Organization.Domain.Staff;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organization.Infrastructure.Configuration.Data.EntityTypeConfiguration
{
    public class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Staff");
            builder.HasKey(x => x.Id);

            builder.HasOne<Domain.Organizations.Organization>()
                .WithOne()
                .HasForeignKey<Employee>(x => x.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.FirstName)
                .HasColumnName("FirstName");

            builder.Property(x => x.LastName)
                .HasColumnName("LastName");

            builder.Property(x => x.Email)
                .HasColumnName("Email");

            builder.Property(x => x.Phone)
                .HasColumnName("Phone");
        }
    }
}
