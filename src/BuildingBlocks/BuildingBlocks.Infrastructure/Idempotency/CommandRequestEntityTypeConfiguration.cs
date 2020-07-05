using BuildingBlocks.Infrastructure.Idempotency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuildingBlocks.Infrastructure.Idempotency
{ 

    public class CommandRequestEntityTypeConfiguration : IEntityTypeConfiguration<IdempotentCommandRequest>
    {
        public void Configure(EntityTypeBuilder<IdempotentCommandRequest> builder)
        {
            builder.ToTable("CommandRequests");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnName("Name");

            builder.Property(x => x.Time).HasColumnName("Time");
        }
    }
}
