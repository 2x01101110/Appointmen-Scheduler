using BuildingBlocks.Infrastructure.Contracts;
using MediatR;
using Scheduling.Infrastructure.Idempotency;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.Application.Commands
{
    public class CommandRequestHandler<TRequest, TResponse> : IRequestHandler<CommandBase<TResponse>, TResponse> where TRequest : IRequest<TRequest>
    {
        private readonly ICommandRequestManager _requestManager;

        public CommandRequestHandler(ICommandRequestManager requestManager)
        {
            this._requestManager = requestManager;
        }

        public virtual TResponse CreateResultForDuplicateRequest() => default(TResponse);

        public async Task<TResponse> Handle(CommandBase<TResponse> request, CancellationToken cancellationToken)
        {
            var commandAlreadyExecuted = await _requestManager.CommandAlreadyExecuted(request.CommandParametersHash());

            return CreateResultForDuplicateRequest();
        }
    }
}
