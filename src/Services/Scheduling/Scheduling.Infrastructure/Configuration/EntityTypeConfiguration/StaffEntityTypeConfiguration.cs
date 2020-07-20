using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduling.Domain.Staff;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Infrastructure.Configuration.EntityTypeConfiguration
{
    public class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("Staff");
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);

            builder.Property(x => x.FirstName)
                .HasColumnName("FirstName");

            builder.Property(x => x.LastName)
                .HasColumnName("LastName");
        }
    }
}
