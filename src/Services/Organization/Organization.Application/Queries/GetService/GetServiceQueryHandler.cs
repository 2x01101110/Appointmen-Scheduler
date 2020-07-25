using BuildingBlocks.Infrastructure.Data;
using Dapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.Application.Queries.GetService
{
    public class GetServiceQueryHandler : IRequestHandler<GetServiceQuery, ServiceDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetServiceQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<ServiceDto> Handle(GetServiceQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var sql = "SELECT [Services].[Id], [Services].[Name] FROM [Services] WHERE [Services].[Id] = @Id";

            var service = await connection.QuerySingleAsync<ServiceDto>(sql, new { request.Id });

            return service;
        }
    }
}
