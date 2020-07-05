using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Application.Commands.Scheduling
{
    public class TestCommandHandler : IRequestHandler<TestCommand>
    {
        public Task<Unit> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
