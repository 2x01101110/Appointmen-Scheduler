using BuildingBlocks.Domain;
using Organization.Domain.Services;
using Organization.Domain.Services.Repository;
using System;
using System.Threading.Tasks;

namespace Organization.Infrastructure.Repositories
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
            this._context.Services.Add(service);
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
