using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Domain.Services.Repository
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<Service> GetServiceAsync(Guid id);
        void AddService(Service service);
        void UpdateService(Service service);
    }
}
