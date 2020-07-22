using System;
using System.Data;
using System.Data.SqlClient;

namespace BuildingBlocks.Infrastructure.Data
{
    public class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        private readonly string connectionString;
        private IDbConnection connection;

        public SqlConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Dispose()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Dispose();
            }
        }

        public IDbConnection GetOpenConnection()
        {
            if (connection == null || connection.State != ConnectionState.Open)
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }

            return connection;
        }
    }
}
