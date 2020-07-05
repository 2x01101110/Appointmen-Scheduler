using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuildingBlocks.Infrastructure.Contracts
{
    public abstract class CommandBase : ICommand
    {
        // Ignore duplicate requests for processing order
        public bool Idempotent { get; }

        public CommandBase(bool idempotent) 
        {
            this.Idempotent = idempotent;
        }
        public CommandBase() 
        {
            this.Idempotent = false;
        }

        public virtual IEnumerable<object> GetAtomicValues() => new object[] { this.GetHashCode() };
        public int CommandParametersHash() => GetAtomicValues().Select(x => x != null ? x.GetHashCode() : 0).Aggregate((x, y) => x ^ y);
    }

    //public abstract class CommandBase<TResult> : ICommand<TResult>
    //{
    //    public DateTime CommandTime { get; private set; }

    //    protected CommandBase()
    //    {
    //        this.CommandTime = DateTime.UtcNow;
    //    }

    //    public virtual string CommandParametersHash() => 
    //        Convert.ToBase64String(Encoding.UTF8.GetBytes(this.GetHashCode().ToString()));
    //}
}
