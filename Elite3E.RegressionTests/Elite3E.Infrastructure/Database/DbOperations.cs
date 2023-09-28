using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Elite3E.Infrastructure.Configuration;
using Elite3E.Infrastructure.Entity;
using Microsoft.Data.SqlClient;

namespace Elite3E.Infrastructure.Database
{
    public class DbOperations
    {
        public List<int> InsertData(List<string> queries)
        {
            var ids = new List<int>();

            using (var connection = new SqlConnection(ApplicationConfigurationBuilder.Instance.ConnectionString))
            {
                foreach (var query in queries)
                {
                    ids.Add(connection.Query<int>($"{query}; SELECT CAST(SCOPE_IDENTITY() as int")
                        .FirstOrDefault());
                }
            }

            return ids.ToList();
            //var parameters = new DynamicParameters();
            //parameters.Add("@batchId", batchId, DbType.Int16, ParameterDirection.Input);
            //parameters.Add("@queryId", queryId, DbType.Int16, ParameterDirection.Input);
        }


        public void DeleteData(List<string> queries,List<int> ids)
        {
            if (queries.Count != ids.Count)
            {
                throw new Exception("The total count of queries and Ids do not match Cannot delete data");
            }

            var counter = 0; 
            using (var connection = new SqlConnection(ApplicationConfigurationBuilder.Instance.ConnectionString))
            {
                foreach (var query in queries)
                {
                    var id = ids[counter];
                    connection.Query(query, new {Id = id});
                    counter++;
                }
            }
        }
    }
}
