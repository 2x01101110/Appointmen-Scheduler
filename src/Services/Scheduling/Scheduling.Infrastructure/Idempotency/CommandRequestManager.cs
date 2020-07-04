using BuildingBlocks.Infrastructure.Idempotency;
using System;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Idempotency
{
    public class CommandRequestManager : ICommandRequestManager
    {
        private readonly SchedulingContext _context;

        public CommandRequestManager(SchedulingContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CommandAlreadyExecuted(string hashCode)
        {
            var command = await _context.FindAsync<CommandRequest>(hashCode);

            return command != null;
        }

        public async Task SaveCommand(string hashCode, string commandName, DateTime commandTime)
        {
            var commandAlreadyExecuted = await CommandAlreadyExecuted(hashCode);

            if (commandAlreadyExecuted)
            {
                throw new Exception($"Command {commandName} was already executed");
            }
            else 
            {
                _context.Add(new CommandRequest
                {
                    HashCode = hashCode,
                    Name = commandName,
                    Time = commandTime
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
