using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Entity
{
    public class ClientAccountDisbursementEntity
    {
        public string MatterNumber { get; set; }
        public string DisbursementType { get; set; }
        public string ClientAccountAcct { get; set; }
        public string Matter { get; set; }
        public string IntendedUse { get; set; }
        public string TransactionDate { get; set; }
        public string Amount { get; set; }
        public bool IsPaymentInformationVerified { get; set; }
        public bool IsClientApprovalObtained { get; set; }
        public string File { get; set; }
        public string PaymentName { get; set; }
        public string DocumentNumber { get; set; }
        public string UseDetails { get; set; }

    }
}
