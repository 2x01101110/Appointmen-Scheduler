using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.Staff.Repository
{
    public interface IStaffRepository
    {
        void AddStaff(Staff staff);
        void UpdateStaff(Staff staff);
    }
}
