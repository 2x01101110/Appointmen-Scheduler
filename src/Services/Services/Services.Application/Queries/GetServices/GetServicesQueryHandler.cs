using BuildingBlocks.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Application.Queries.GetServices
{
    public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, List<ServiceDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetServicesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public Task<List<ServiceDto>> Handle(GetServicesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
