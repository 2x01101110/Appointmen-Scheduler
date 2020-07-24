using MediatR;
using Services.Domain.Services.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Application.Commands.AssignStaffToService
{
    class AssignStaffToServiceCommandHandler : IRequestHandler<AssignStaffToServiceCommand>
    {
        private readonly IServiceRepository _serviceRepository;
        public AssignStaffToServiceCommandHandler(
            IServiceRepository serviceRepository)
        {
            this._serviceRepository = serviceRepository;
        }

        public async Task<Unit> Handle(AssignStaffToServiceCommand request, CancellationToken cancellationToken)
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
