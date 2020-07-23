using BuildingBlocks.Infrastructure.Data;
using Dapper;
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

        public async Task<List<ServiceDto>> Handle(GetServicesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var sql = "SELECT [Services].[Id], [Services].[Name] FROM [Services]";

            var services = await connection.QueryAsync<ServiceDto>(sql);

            return services.AsList();
        }
    }
}
