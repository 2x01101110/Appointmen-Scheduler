using BuildingBlocks.Infrastructure.Idempotency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Infrastructure.Domain
{
    public class CommandRequestEntityTypeConfiguration : IEntityTypeConfiguration<CommandRequest>
    {
        public void Configure(EntityTypeBuilder<CommandRequest> builder)
        {
            builder.ToTable("CommandRequests");

            builder.HasKey(x => x.HashCode);

            builder.Property(x => x.Name).HasColumnName("Name");

            builder.Property(x => x.Time).HasColumnName("Time");
        }
    }
}
