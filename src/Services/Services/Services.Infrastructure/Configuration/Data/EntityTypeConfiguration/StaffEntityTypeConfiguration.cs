using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Domain.Organization;
using Services.Domain.Staff;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Infrastructure.Configuration.Data.EntityTypeConfiguration
{
    public class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("Staff");
            builder.HasKey(x => x.Id);

            builder.HasOne<Organization>()
                .WithOne()
                .HasForeignKey<Staff>(x => x.OrganizationId)
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
