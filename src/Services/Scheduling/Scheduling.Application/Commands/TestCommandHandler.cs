using BuildingBlocks.Infrastructure.Idempotency;
using MediatR;
using Scheduling.Domain.ScheduleDayAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduling.Application.Commands
{
    public class TestCommandHandler : IRequestHandler<TestCommand, bool>
    {
        private readonly IScheduleDayRepository _repository;

        public TestCommandHandler(IScheduleDayRepository repository)
        {
            this._repository = repository;
        }

        public async Task<bool> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            var test = await _repository.FindByDayAsync(DateTime.Now);

            return await Task.FromResult(true);
        }
    }

    public class TestIdentidiedCommandHandler : IdentifiedCommandHandler<TestCommand, bool>
    {
        public TestIdentidiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {

        }

        public override bool CreateResultForDuplicateRequest() => true;
    }
}
