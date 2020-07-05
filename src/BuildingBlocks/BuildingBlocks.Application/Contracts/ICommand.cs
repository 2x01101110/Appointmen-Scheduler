using MediatR;
using System;

namespace BuildingBlocks.Infrastructure.Contracts
{
    public interface ICommand : IRequest
    {
        bool Idempotent { get; }
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
        bool Idempotent { get; }
    }
}
