using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
            throw new NotImplementedException();
        }
    }
}
