using BuildingBlocks.Infrastructure.Idempotency;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Application.Commands
{
    public class IdentifiedCommandHandler<T, R> : IRequestHandler<IdentifiedCommand<T, R>, R> where T : IRequest<R>
    {
        private readonly IMediator _mediator;
        private readonly IRequestManager _requestManager;

        public IdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager)
        {
            this._mediator = mediator;
            this._requestManager = requestManager;
        }

        public virtual R CreateResultForDuplicateRequest() => default(R);

        public async Task<R> Handle(IdentifiedCommand<T, R> request, CancellationToken cancellationToken)
        {
            if (await _requestManager.ExistsAsync(request.Id))
            {
                return CreateResultForDuplicateRequest();
            }
            else
            {
                try
                {
                    await _requestManager.CreateRequestForCommandAsync<T>(request.Id);

                    var command = request.Command;

                    var result = await _mediator.Send(command, cancellationToken);

                    return result;
                }
                catch 
                {
                    return default(R);
                }
            }
        }
    }
}
