using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Elite3E.Infrastructure.Database
{
    public class BaseDBConnection
    {
        private readonly IDbConnection _connection;
        public BaseDBConnection(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            Console.WriteLine("SQL Server DB Connection String: " + connectionString);
        }

        public IDbConnection Connection => _connection;

    }
}
