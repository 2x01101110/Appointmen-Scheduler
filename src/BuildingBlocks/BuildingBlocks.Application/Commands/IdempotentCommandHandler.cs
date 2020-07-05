using MediatR;
using Scheduling.Infrastructure.Idempotency;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.Application.Commands
{
    public class IdempotentCommandHandler<TCommand> : 
        IRequestHandler<IdempotentCommand<TCommand>> where TCommand : IRequest
    {
        private readonly IIdempotentCommandRequestManager _idempotentCommandRequestManager;
        private readonly IMediator _mediator;

        public IdempotentCommandHandler(IIdempotentCommandRequestManager idempotentCommandRequestManager, IMediator mediator)
        {
            this._idempotentCommandRequestManager = idempotentCommandRequestManager;
            this._mediator = mediator;
        }

        public async Task<Unit> Handle(IdempotentCommand<TCommand> request, CancellationToken cancellationToken)
        {
            if (await _idempotentCommandRequestManager.CommandExistsAsync(request.Id))
            {
                return Unit.Value;
            }
            else 
            {
                try
                {
                    await _idempotentCommandRequestManager.SaveCommandAsync<TCommand>(request.Id);

                    var result = await this._mediator.Send(request.Command, cancellationToken);

                    return result;
                }
                catch
                {
                    return Unit.Value;
                }
            }
        }
    }

    public class IdempotentCommandHandler<TCommand, TResult> : 
        IRequestHandler<IdempotentCommand<TCommand, TResult>, TResult> where TCommand : IRequest<TResult>
    {
        private readonly IIdempotentCommandRequestManager _idempotentCommandRequestManager;
        private readonly IMediator _mediator;

        public IdempotentCommandHandler(IIdempotentCommandRequestManager idempotentCommandRequestManager, IMediator mediator)
        {
            this._idempotentCommandRequestManager = idempotentCommandRequestManager;
            this._mediator = mediator;
        }

        protected virtual TResult ResultForDuplicateCommand() => default(TResult);

        public async Task<TResult> Handle(IdempotentCommand<TCommand, TResult> request, CancellationToken cancellationToken)
        {
            if (await _idempotentCommandRequestManager.CommandExistsAsync(request.Id))
            {
                return ResultForDuplicateCommand();
            }
            else
            {
                try
                {
                    await _idempotentCommandRequestManager.SaveCommandAsync<TCommand, TResult>(request.Id);

                    var result = await this._mediator.Send(request.Command, cancellationToken);

                    return result;
                }
                catch
                {
                    return default(TResult);
                }
            }
        }
    }
}
