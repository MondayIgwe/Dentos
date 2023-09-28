using Elite3E.Infrastructure.Configuration;
using Elite3E.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace Elite3E.Infrastructure.Database
{
    public class TE_3E_DB : BaseDBConnection, ITE_3E_DB
    {
        public TE_3E_DB() : base(ApplicationConfigurationBuilder.Instance.ConnectionString) { }

        public async Task<IList<DbDoesTableExistEntity>> DoesTableExistInDB(string tableName)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@tableName", tableName, DbType.String, ParameterDirection.Input);

            string sql = "SELECT t.name 'TableName', t.type_desc 'TableDesc', t.create_date 'CreatedDate' FROM sys.tables t "
            + " WHERE name = @tableName";

            /*
            --Does Table Exist?
Declare @tableName varchar(100) = 'FredBrother';

SELECT t.name 'TableName', t.type_desc 'TableDesc', t.create_date 'CreatedDate' FROM sys.tables t
WHERE name = @tableName
             */

            return (List<DbDoesTableExistEntity>) await Connection.QueryAsync<DbDoesTableExistEntity>(sql,parameters);
        }

        public async Task<IList<DbBOAReportEntity>> QueryBOATable_MxMatterWIP_ccc(string tableName)
        {
            string sql = "SELECT mt.DisplayName as 'MatterName', mt.Number as 'MatterNumber', BOA as 'BOA' "
            + " FROM " + tableName + " as fb inner join Matter as mt on fb.matter=mt.MattIndex";
            /*
                -- Query for MxMatterWIP_ccc
SELECT mt.DisplayName as 'MatterName', mt.Number as 'MatterNumber', BOA as 'BOA' 
FROM FredBrother as fb inner join Matter as mt on fb.matter=mt.MattIndex
             */

            return (List<DbBOAReportEntity>) await Connection.QueryAsync<DbBOAReportEntity>(sql);
        }

        public async Task<IList<DbBOAReportEntity>> QueryBOATable_MxClientManagement_ccc(string tableName)
        {
            string sql = "SELECT mt.DisplayName as 'MatterName', mt.Number as 'MatterNumber', BOA as 'BOA' "
            + " FROM " + tableName + " as cmt inner join MattDate as md on cmt.MattDate = md.MattDateID inner join matter mt on md.MatterLkUp = mt.MattIndex";
            /*
                -- Query for MxClientManagement_ccc
SELECT mt.DisplayName as 'MatterName', mt.Number as 'MatterNumber', BOA as 'BOA'
FROM ClientManagementTable as cmt inner join MattDate as md on cmt.MattDate=md.MattDateID inner join matter mt on md.MatterLkUp=mt.MattIndex
             */

            return (List<DbBOAReportEntity>) await Connection.QueryAsync<DbBOAReportEntity>(sql);
        }

        public async Task<IList<DbBOAReportEntity>> QueryBOATable_MxInvestment_ccc(string tableName)
        {
            string sql = "SELECT mt.DisplayName as 'MatterName', mt.Number as 'MatterNumber', BOA as 'BOA' "
            + " FROM " + tableName + " as imt inner join MattDate as md on imt.MattDate = md.MattDateID inner join matter mt on md.MatterLkUp = mt.MattIndex";
            /*
                -- Query for MxInvestment_ccc
SELECT mt.DisplayName as 'MatterName', mt.Number as 'MatterNumber', BOA as 'BOA'
FROM InvestmentMetricTable as imt inner join MattDate as md on imt.MattDate=md.MattDateID inner join matter mt on md.MatterLkUp=mt.MattIndex
             */

            return (List<DbBOAReportEntity>) await Connection.QueryAsync<DbBOAReportEntity>(sql);
        }

        public async Task<IList<DbBOAReportEntity>> QueryBOATable_MxMatterAgedAR_ccc(string tableName)
        {
            string sql = "SELECT mt.DisplayName as 'MatterName', mt.Number as 'MatterNumber', BOA as 'BOA' "
            + " FROM " + tableName + " as mat inner join Matter as mt on mat.matter = mt.MattIndex";
            /*
            -- Query for MxMatterAgedAR_ccc
SELECT mt.DisplayName as 'MatterName', mt.Number as 'MatterNumber', BOA as 'BOA'
FROM MatterAgedARTable as mat inner join Matter as mt on mat.matter=mt.MattIndex
             */

            return (List<DbBOAReportEntity>) await Connection.QueryAsync<DbBOAReportEntity>(sql);
        }


    }
}
