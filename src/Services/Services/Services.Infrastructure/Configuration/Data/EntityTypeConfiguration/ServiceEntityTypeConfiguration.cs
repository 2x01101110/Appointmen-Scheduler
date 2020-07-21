using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Infrastructure.Configuration.Data.EntityTypeConfiguration
{
    public class ServiceEntityTypeConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            throw new NotImplementedException();
        }
    }
}
