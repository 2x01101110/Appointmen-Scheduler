using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organization.Application.Commands.AssignEmployeeToService
{
    public class AssignEmployeeToServiceCommand : IRequest
    {
        public Guid ServiceId { get; set; }
        public Guid StaffId { get; set; }
    }
}
