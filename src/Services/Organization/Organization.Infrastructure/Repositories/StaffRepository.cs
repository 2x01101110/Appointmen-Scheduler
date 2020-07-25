using BuildingBlocks.Domain;
using Organization.Domain.Staff;
using Organization.Domain.Staff.Repository;
using System;
using System.Threading.Tasks;

namespace Organization.Infrastructure.Repositories
{
    public class StaffRepository : IEmployeeRepository
    {
        private readonly ServiceContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public StaffRepository(ServiceContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddEmployee(Employee Employee)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployeeAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee(Employee staff)
        {
            throw new NotImplementedException();
        }
    }
}
