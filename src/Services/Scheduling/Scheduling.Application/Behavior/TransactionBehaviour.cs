using MediatR;
using Scheduling.Domain.ScheduleDayAggregate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Application.Behavior
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IScheduleDayRepository _scheduleDayRepository;

        public TransactionBehaviour(IScheduleDayRepository scheduleDayRepository)
        {
            this._scheduleDayRepository = scheduleDayRepository;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            return await next();
        }
    }
}
