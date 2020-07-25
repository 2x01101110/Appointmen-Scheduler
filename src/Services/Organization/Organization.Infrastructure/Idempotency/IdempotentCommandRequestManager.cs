using BuildingBlocks.Infrastructure.Idempotency;
using MediatR;
using Scheduling.Infrastructure.Idempotency;
using System;
using System.Threading.Tasks;

namespace Organization.Infrastructure.Idempotency
{
    public class IdempotentCommandRequestManager : IIdempotentCommandRequestManager
    {
        private readonly ServiceContext _context;

        public IdempotentCommandRequestManager(ServiceContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CommandExistsAsync(Guid id)
        {
            var command = await _context.FindAsync<IdempotentCommandRequest>(id);
            return command != null;
        }

        public async Task SaveCommandAsync<TCommand>(Guid id) where TCommand : IRequest
        {
            var commandAlreadyExecuted = await CommandExistsAsync(id);

            if (commandAlreadyExecuted)
            {
                throw new Exception($"Command {typeof(TCommand).Name} was already executed");
            }
            else
            {
                _context.Add(new IdempotentCommandRequest
                {
                    Id = id,
                    Name = typeof(TCommand).Name,
                    Time = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task SaveCommandAsync<TCommand, TResult>(Guid id) where TCommand : IRequest<TResult>
        {
            var commandAlreadyExecuted = await CommandExistsAsync(id);

            if (commandAlreadyExecuted)
            {
                throw new Exception($"Command {typeof(TCommand).Name} was already executed");
            }
            else
            {
                _context.Add(new IdempotentCommandRequest
                {
                    Id = id,
                    Name = typeof(TCommand).Name,
                    Time = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
