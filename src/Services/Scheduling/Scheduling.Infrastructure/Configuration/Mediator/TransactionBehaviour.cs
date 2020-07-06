using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Configuration.Mediator
{
    public class TransactionBehaviour<IRequest, TResponse> : IPipelineBehavior<IRequest, TResponse>
    {
        private readonly SchedulingContext _schedulingContext;

        public TransactionBehaviour(SchedulingContext schedulingContext)
        {
            _schedulingContext = schedulingContext;
        }

        public async Task<TResponse> Handle(IRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);

            try
            {
                if (_schedulingContext.HasActiveTransaction)
                {
                    return await next();
                }

                var executionStrategy = _schedulingContext.Database.CreateExecutionStrategy();

                await executionStrategy.ExecuteAsync(async () =>
                {
                    using (var transaction = await _schedulingContext.BeginTransaction())
                    {
                        response = await next();

                        await _schedulingContext.CommitTransactionAsync(transaction);
                    }
                });

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
