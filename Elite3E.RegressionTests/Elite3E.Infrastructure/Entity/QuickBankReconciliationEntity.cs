using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Entity
{
    public class QuickBankReconciliationEntity
    {
        public string BankGroup { get; set; }
        public string StatementDate { get; set; }
        public string StatementNumber { get; set; }
        public string Deposits { get; set; }
        public string Description { get; set; }
        public string DepositsOverWrite { get; set; }
    }
}
