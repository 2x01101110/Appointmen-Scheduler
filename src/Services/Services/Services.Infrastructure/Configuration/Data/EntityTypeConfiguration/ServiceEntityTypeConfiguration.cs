using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Domain.Organization;
using Services.Domain.Services;
using Services.Domain.Staff;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Infrastructure.Configuration.Data.EntityTypeConfiguration
{
    public class ServiceEntityTypeConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services");
            builder.HasKey(x => x.Id);

            builder.HasOne<Organization>()
                .WithOne()
                .HasForeignKey<Service>(x => x.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Name)
                .HasColumnName("Name");

            builder.Property(x => x.CanSelectStaff)
                .HasColumnName("CanSelectStaff");

            builder.Property(x => x.Available)
                .HasColumnName("Available");

            builder.OwnsMany<ServiceStaff>("Staff", x =>
            {
                x.ToTable("ServiceStaff");
                x.WithOwner().HasForeignKey("ServiceId");

                x.Property(x => x.StaffId).HasColumnName("StaffId");

                x.HasOne<Staff>()
                    .WithOne()
                    .HasForeignKey<ServiceStaff>(x => x.StaffId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
