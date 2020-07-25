using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organization.Domain.Services
{
    public class ServiceStaff : Entity<Guid>
    {
        public Guid StaffId { get; }

        private ServiceStaff() { }

        private ServiceStaff(Guid staffId)
        {
            this.StaffId = staffId;
        }

        public static ServiceStaff CreateServiceStaff(Guid staffId)
        {
            return new ServiceStaff(staffId);
        }
    }
}
