using System.Data;

namespace BuildingBlocks.Infrastructure.Data
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
