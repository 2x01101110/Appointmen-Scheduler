using BuildingBlocks.Application.Commands;
using BuildingBlocks.Infrastructure.EventBus;
using MediatR;
using Scheduling.Application.Commands.CreateService;
using Scheduling.Domain.Services.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Application.IntegrationEvents.ServiceCreatedEvent
{
    public class ServiceCreatedIntegrationEventHandler : IIntegrationEventHandler<ServiceCreatedIntegrationEvent>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMediator _mediator;

        public ServiceCreatedIntegrationEventHandler(
            IServiceRepository serviceRepository,
            IMediator mediator)
        {
            this._serviceRepository = serviceRepository;
            this._mediator = mediator;
        }

        public Task Handle(ServiceCreatedIntegrationEvent integrationEvent)
        {
            var command = new IdempotentCommand<CreateServiceCommand>(
                new CreateServiceCommand
                {

                },
                Guid.NewGuid()
                );

            throw new NotImplementedException();
        }
    }
}
