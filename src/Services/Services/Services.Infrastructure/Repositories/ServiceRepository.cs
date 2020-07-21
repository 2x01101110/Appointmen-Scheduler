using BuildingBlocks.Domain;
using Services.Domain.Services;
using Services.Domain.Services.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ServiceContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ServiceRepository(ServiceContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddService(Service service)
        {
            throw new NotImplementedException();
        }

        public Task<Service> GetServiceAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateService(Service service)
        {
            throw new NotImplementedException();
        }
    }
}
