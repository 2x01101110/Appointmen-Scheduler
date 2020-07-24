using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Application.Commands.CreateStaff
{
    public class CreateStaffCommand : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
