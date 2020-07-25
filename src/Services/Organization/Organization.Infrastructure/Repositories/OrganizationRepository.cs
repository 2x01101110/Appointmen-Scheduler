using BuildingBlocks.Domain;
using Organization.Domain.Organizations.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organization.Infrastructure.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ServiceContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public OrganizationRepository(ServiceContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddOrganization(Domain.Organizations.Organization organization)
        {
            _context.Add(organization);
        }
    }
}
