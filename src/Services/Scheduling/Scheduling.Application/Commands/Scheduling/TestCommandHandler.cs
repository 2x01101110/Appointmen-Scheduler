using BuildingBlocks.Infrastructure.Contracts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Application.Commands.Scheduling
{
    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        public Task<Unit> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
