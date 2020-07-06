using MediatR;
using System;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Idempotency
{
    public interface IIdempotentCommandRequestManager
    {
        Task<bool> CommandExistsAsync(Guid id);
        Task SaveCommandAsync<TCommand>(Guid id) where TCommand : IRequest;
        Task SaveCommandAsync<TCommand, TResult>(Guid id) where TCommand : IRequest<TResult>;
    }
}
