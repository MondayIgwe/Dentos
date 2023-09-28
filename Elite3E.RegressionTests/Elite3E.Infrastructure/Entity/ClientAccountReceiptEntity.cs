using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Entity
{
    public class ClientAccountReceiptEntity
    {
        public string MatterNumber { get; set; }
        public string ClientAccountReceiptType { get; set; }
        public string IntendedUse { get; set; }
        public string TransactionDate { get; set; }
        public string Amount { get; set; }
        public string Reason { get; set; }
        public string ClientAccountAcct { get; set; }
        public string DocumentNumber { get; set; }
    }
}
