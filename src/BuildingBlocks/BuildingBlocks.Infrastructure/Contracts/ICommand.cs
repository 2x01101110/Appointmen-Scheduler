using MediatR;
using System;

namespace BuildingBlocks.Infrastructure.Contracts
{
    public interface ICommand : IRequest
    {
        Guid CommandId { get; }
        DateTime CommandTime { get; }
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
        Guid CommandId { get; }
        DateTime CommandTime { get; }
    }
}
