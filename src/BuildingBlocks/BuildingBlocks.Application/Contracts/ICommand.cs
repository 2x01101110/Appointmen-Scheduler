using MediatR;
using System;

namespace BuildingBlocks.Infrastructure.Contracts
{
    public interface ICommand : IRequest
    {
        DateTime CommandTime { get; }
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
        DateTime CommandTime { get; }
    }
}
