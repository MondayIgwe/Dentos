using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Entity
{
    public class BankAccountClientAccountEntity
    {
        public string Entity { get; set; }
        public string BankName { get; set; }
        public string BankSite { get; set; }
        public string ABARoutingNumber { get; set; }

        public string PositivePayTemplate { get; set; }
        public string Bank { get; set; }
        public string AccountName { get; set; }

        public string Description { get; set; }
        public string MoneyType { get; set; }

        public string BankAccountType { get; set; }
        public string BankGroup { get; set; }
        public string Status { get; set; }
        public string AccountNumber { get; set; }
        public string Office { get; set; }
        public string Language { get; set; }
        
        public string Currency { get; set; }

        public string GLType { get; set; }

        public string CashGLAccount { get; set; }

        public string ContraGLAccount { get; set; }

        public string ModuleType { get; set; }

        public string ReconciliationRuleSet { get; set; }

        public string IsNewBankGroup { get; set; }

        public string BankGroupName { get; set; }
        public string AnchorMask { get; set; }
    }
}
