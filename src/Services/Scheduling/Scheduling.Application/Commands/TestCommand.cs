using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Application.Commands
{
    public class TestCommand : IRequest<bool>
    {
        public TestCommand()
        {

        }
    }
}
