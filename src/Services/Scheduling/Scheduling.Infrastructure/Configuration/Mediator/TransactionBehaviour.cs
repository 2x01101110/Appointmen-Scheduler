using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Configuration.Mediator
{
    public class TransactionBehaviour<IRequest, TResponse> : IPipelineBehavior<IRequest, TResponse>
    {
        private readonly ILogger<TransactionBehaviour<IRequest, TResponse>> _logger;
        private readonly SchedulingContext _schedulingContext;

        public TransactionBehaviour(
            ILogger<TransactionBehaviour<IRequest, TResponse>> logger,
            SchedulingContext schedulingContext)
        {
            _schedulingContext = schedulingContext ?? throw new ArgumentException(nameof(SchedulingContext));
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }

        public async Task<TResponse> Handle(IRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            var transactionId = Guid.NewGuid();

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
                        _logger.LogInformation($"Beginning transaction {transactionId} for {typeof(IRequest).Name} {request}");

                        response = await next();

                        _logger.LogInformation($"Commiting transaction {transactionId} for {typeof(IRequest).Name} {request}");

                        await _schedulingContext.CommitTransactionAsync(transaction);
                    }
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error handling transaction {transactionId} for {typeof(IRequest).Name} {request}");

                throw;
            }
        }
    }
}
