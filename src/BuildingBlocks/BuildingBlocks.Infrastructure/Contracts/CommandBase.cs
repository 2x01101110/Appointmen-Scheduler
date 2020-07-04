using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Infrastructure.Contracts
{
    public abstract class CommandBase : ICommand
    {
        public Guid CommandId { get; private set; }
        public DateTime CommandTime { get; private set; }
        

        protected CommandBase()
        {
            this.CommandId = Guid.NewGuid();
            this.CommandTime = DateTime.UtcNow;
        }

        protected CommandBase(Guid commandId, DateTime commandTime)
        {
            this.CommandId = commandId;
            this.CommandTime = commandTime;
        }
    }

    public abstract class CommandBase<TResult> : ICommand<TResult>
    {
        public Guid CommandId { get; private set; }
        public DateTime CommandTime { get; private set; }

        protected CommandBase()
        {
            this.CommandId = Guid.NewGuid();
            this.CommandTime = DateTime.UtcNow;
        }

        protected CommandBase(Guid commandId, DateTime commandTime)
        {
            this.CommandId = commandId;
            this.CommandTime = commandTime;
        }
    }
}
