using BuildingBlocks.Domain;
using Services.Domain.Organization;
using Services.Domain.Organization.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Infrastructure.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ServiceContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public OrganizationRepository(ServiceContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddOrganization(Organization organization)
        {
            _context.Add(organization);
        }
    }
}
