using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Domain.Organization.Repository
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
        void AddOrganization(Organization organization);
    }
}
