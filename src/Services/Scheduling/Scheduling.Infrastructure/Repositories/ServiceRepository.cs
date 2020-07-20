using BuildingBlocks.Domain;
using Scheduling.Domain.Services;
using Scheduling.Domain.Services.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly SchedulingContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ServiceRepository(SchedulingContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddService(Service service)
        {
            this._context.Services.Add(service);
        }

        public void UpdateService(Service service)
        {
            throw new NotImplementedException();
        }
    }
}
