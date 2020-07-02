using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Infrastructure.Idempotency
{
    public class IdentifiedCommand<T, R> : IRequest<R> where T : IRequest<R>
    {
        public T Command { get; }
        public Guid Id { get; }

        public IdentifiedCommand(T command, Guid id)
        {
            this.Command = command;
            this.Id = id;
        }
    }
}
