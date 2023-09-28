using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Entity
{
    public class CreateBankStatement
    {
        public string BankGroup { get; set; }
        public string Description { get; set; }
        public string StatementDate { get; set; }
        public string StatementNumber { get; set; }
        public string ClearDate { get; set; }
        public string BeginningBalance { get; set; }

        public string Deposit { get; set; }

        public string Withdrawal { get; set; }

        public string EndingBalance { get; set; }
        public string DepositReference { get; set; }

        public string WithdrawalReference { get; set; }
        public string WorksheetId { get; set; }

    }
}
