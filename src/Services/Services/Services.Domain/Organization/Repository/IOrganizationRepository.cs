using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Domain.Organization.Repository
{
    public interface IOrganizationRepository
    {
        void AddOrganization(Organization organization);
    }
}
