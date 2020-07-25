using MediatR;
using Organization.Domain.Services.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.Application.Commands.AssignEmployeeToService
{
    class AssignEmployeeToServiceCommandHandler : IRequestHandler<AssignEmployeeToServiceCommand>
    {
        private readonly IServiceRepository _serviceRepository;
        public AssignEmployeeToServiceCommandHandler(
            IServiceRepository serviceRepository)
        {
            this._serviceRepository = serviceRepository;
        }

        public async Task<Unit> Handle(AssignEmployeeToServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await this._serviceRepository.GetServiceAsync(request.ServiceId);

            // handle exception if null

            service.AssignStaffToService(request.StaffId);

            this._serviceRepository.UpdateService(service);

            await this._serviceRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
