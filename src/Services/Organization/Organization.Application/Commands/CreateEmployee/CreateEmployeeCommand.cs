using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organization.Application.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
