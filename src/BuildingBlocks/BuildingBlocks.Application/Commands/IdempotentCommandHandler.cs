using MediatR;
using Microsoft.Extensions.Logging;
using Scheduling.Infrastructure.Idempotency;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.Application.Commands
{
    public class IdempotentCommandHandler<TCommand> : 
        IRequestHandler<IdempotentCommand<TCommand>> where TCommand : IRequest
    {
        private readonly IIdempotentCommandRequestManager _idempotentCommandRequestManager;
        private readonly ILogger<IdempotentCommandHandler<TCommand>> _logger;
        private readonly IMediator _mediator;

        public IdempotentCommandHandler(
            IIdempotentCommandRequestManager idempotentCommandRequestManager,
            ILogger<IdempotentCommandHandler<TCommand>> logger,
            IMediator mediator)
        {
            this._idempotentCommandRequestManager = idempotentCommandRequestManager;
            this._mediator = mediator;
            this._logger = logger;
        }

        public async Task<Unit> Handle(IdempotentCommand<TCommand> request, CancellationToken cancellationToken)
        {
            if (await _idempotentCommandRequestManager.CommandExistsAsync(request.Id))
            {
                _logger.LogInformation($"Command {nameof(request.Command)} with Id {request.Id} already exists, creating command request duplicate");
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
        private readonly ILogger<IdempotentCommandHandler<TCommand, TResult>> _logger;
        private readonly IMediator _mediator;

        public IdempotentCommandHandler(
            IIdempotentCommandRequestManager idempotentCommandRequestManager,
            ILogger<IdempotentCommandHandler<TCommand, TResult>> logger,
            IMediator mediator)
        {
            this._idempotentCommandRequestManager = idempotentCommandRequestManager;
            this._logger = logger;
            this._mediator = mediator;
        }

        protected virtual TResult ResultForDuplicateCommand() => default(TResult);

        public async Task<TResult> Handle(IdempotentCommand<TCommand, TResult> request, CancellationToken cancellationToken)
        {
            if (await _idempotentCommandRequestManager.CommandExistsAsync(request.Id))
            {
                _logger.LogInformation($"Command {nameof(request.Command)} with Id {request.Id} already exists, creating command request duplicate");
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
