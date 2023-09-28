using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Entity
{
    public class PaymentSelectionGenerationEntity
    {
        public string Description { get; set; }
        public string BankAccount { get; set; }
        public string PaymentDate { get; set; }
        public string ChequeTemplate { get; set; }
        public string ChequePrinter { get; set; }

    }
}
