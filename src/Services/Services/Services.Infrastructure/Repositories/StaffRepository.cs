using BuildingBlocks.Domain;
using Services.Domain.Staff;
using Services.Domain.Staff.Repository;
using System;
using System.Threading.Tasks;

namespace Services.Infrastructure.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ServiceContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public StaffRepository(ServiceContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddStaff(Staff staff)
        {
            throw new NotImplementedException();
        }

        public Task<Staff> GetStaffAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateStaff(Staff staff)
        {
            throw new NotImplementedException();
        }
    }
}
