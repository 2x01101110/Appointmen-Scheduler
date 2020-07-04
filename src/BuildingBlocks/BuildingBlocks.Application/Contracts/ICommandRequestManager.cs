using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Idempotency
{
    public interface ICommandRequestManager
    {
        Task SaveCommand(string hashCode, string commandName, DateTime commandTime);
        Task<bool> CommandAlreadyExecuted(string hashCode);
    }
}
