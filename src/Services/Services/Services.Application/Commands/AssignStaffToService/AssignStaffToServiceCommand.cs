using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Application.Commands.AssignStaffToService
{
    public class AssignStaffToServiceCommand : IRequest
    {
        public Guid ServiceId { get; set; }
        public Guid StaffId { get; set; }
    }
}
