using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Infrastructure.Contracts
{
    public abstract class CommandBase : ICommand
    {
        public DateTime CommandTime { get; private set; }
        

        protected CommandBase()
        {
            this.CommandTime = DateTime.UtcNow;
        }

        public virtual string CommandParametersHash() => Convert.ToBase64String(Encoding.UTF8.GetBytes(this.GetHashCode().ToString()));
    }

    public abstract class CommandBase<TResult> : ICommand<TResult>
    {
        public DateTime CommandTime { get; private set; }

        protected CommandBase()
        {
            this.CommandTime = DateTime.UtcNow;
        }

        public virtual string CommandParametersHash() => Convert.ToBase64String(Encoding.UTF8.GetBytes(this.GetHashCode().ToString()));
    }
}
