using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Domain.Staff.Repository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> GetEmployeeAsync(Guid id);
        void AddEmployee(Employee staff);
        void UpdateEmployee(Employee staff);
    }
}
