using Elite3E.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Database
{
    public interface ITE_3E_DB
    {
        Task<IList<DbDoesTableExistEntity>> DoesTableExistInDB(string tableName);
        Task<IList<DbBOAReportEntity>> QueryBOATable_MxMatterWIP_ccc(string tableName);
        Task<IList<DbBOAReportEntity>> QueryBOATable_MxClientManagement_ccc(string tableName);
        Task<IList<DbBOAReportEntity>> QueryBOATable_MxInvestment_ccc(string tableName);
        Task<IList<DbBOAReportEntity>> QueryBOATable_MxMatterAgedAR_ccc(string tableName);
    }
}
