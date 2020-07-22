using BuildingBlocks.Application.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using Scheduling.Domain.Services.Repository;
using Scheduling.Infrastructure.Idempotency;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Application.Commands.CreateService
{
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand>
    {
        public readonly IServiceRepository _serviceRepository;

        public CreateServiceCommandHandler(IServiceRepository serviceRepository)
        {
            this._serviceRepository = serviceRepository;
        }

        public Task<Unit> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class CreateServiceIdempotentCommandHandler : IdempotentCommandHandler<CreateServiceCommand>
    {
        public CreateServiceIdempotentCommandHandler(
            IIdempotentCommandRequestManager idempotentCommandRequestManager,
            ILogger<IdempotentCommandHandler<CreateServiceCommand>> logger,
            IMediator mediator) : base(idempotentCommandRequestManager, logger, mediator)
        {

        }
    }
}
