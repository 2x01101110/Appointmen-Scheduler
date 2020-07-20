using BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore;
using Scheduling.Domain.Staff;
using Scheduling.Domain.Staff.Repository;
using System;

namespace Scheduling.Infrastructure.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly SchedulingContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public StaffRepository(SchedulingContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddStaff(Staff staff)
        {
            this._context.Add(staff);
        }

        public void UpdateStaff(Staff staff)
        {
            _context.Entry(staff).State = EntityState.Modified;
        }
    }
}
