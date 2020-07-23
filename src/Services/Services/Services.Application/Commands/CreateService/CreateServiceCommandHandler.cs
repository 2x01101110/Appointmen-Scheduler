using MediatR;
using Microsoft.Extensions.Logging;
using Services.Domain.Services;
using Services.Domain.Services.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Application.Commands.CreateService
{
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand>
    {
        private readonly ILogger<CreateServiceCommandHandler> _logger;
        private readonly IServiceRepository _serviceRepository;

        public CreateServiceCommandHandler(
            ILogger<CreateServiceCommandHandler> logger,
            IServiceRepository serviceRepository)
        {
            this._serviceRepository = serviceRepository;
            this._logger = logger;
        }

        public async Task<Unit> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            this._logger.LogInformation($"{DateTime.UtcNow} | {nameof(request)}");

            var service = Service.CreateService(request.OrganizationId, request.Name, request.CanSelectStaff, request.Available);

            this._serviceRepository.AddService(service);

            await this._serviceRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
