using BuildingBlocks.Application.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using Scheduling.Infrastructure.Idempotency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Application.Commands.Scheduling
{
    public class TestCommandHandler : IRequestHandler<TestCommand>
    {
        private readonly ILogger<TestCommandHandler> _logger;

        public TestCommandHandler(ILogger<TestCommandHandler> logger)
        {
            this._logger = logger;
        }

        public async Task<Unit> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Executing some command");

            return await Task.FromResult(Unit.Value);
        }
    }

    public class TestCommandIdempotentCommandHandler : IdempotentCommandHandler<TestCommand>
    {
        public TestCommandIdempotentCommandHandler(
            IIdempotentCommandRequestManager idempotentCommandRequestManager,
            ILogger<IdempotentCommandHandler<TestCommand>> logger,
            IMediator mediator
            ) : base(idempotentCommandRequestManager, logger, mediator)
        {

        }
    }
}
