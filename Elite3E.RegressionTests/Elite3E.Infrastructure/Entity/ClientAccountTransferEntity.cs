using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Entity
{
    public class ClientAccountTransferEntity
    {
        public string TransferType { get; set; }

        public string FromAccount { get; set; }

        public string ToAccount { get; set; }
        public string Amount { get; set; }

        public string IntendedUse { get; set; }

        public string FromMatter { get; set; }
        public string TransferNumber { get; set; }

        public string ToMatter { get; set; }
    }
}
