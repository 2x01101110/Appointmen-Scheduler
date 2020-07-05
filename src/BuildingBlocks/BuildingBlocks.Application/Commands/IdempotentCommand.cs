using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Application.Commands
{
    public class IdempotentCommand<TCommand> : IRequest where TCommand : IRequest
    {
        public Guid Id { get; }
        public TCommand Command { get; }

        public IdempotentCommand(TCommand command, Guid id)
        {
            this.Command = command;
            this.Id = id;
        }
    }

    public class IdempotentCommand<TCommand, TResult> : IRequest<TResult> where TCommand : IRequest<TResult>
    {
        public Guid Id { get; }
        public TCommand Command { get; }

        public IdempotentCommand(TCommand command, Guid id)
        {
            this.Command = command;
            this.Id = id;
        }
    }
}
