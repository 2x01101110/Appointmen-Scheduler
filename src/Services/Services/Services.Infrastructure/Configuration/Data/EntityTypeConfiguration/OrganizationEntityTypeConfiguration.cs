using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Domain.Organization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Infrastructure.Configuration.Data.EntityTypeConfiguration
{
    public class OrganizationEntityTypeConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            throw new NotImplementedException();
        }
    }
}
