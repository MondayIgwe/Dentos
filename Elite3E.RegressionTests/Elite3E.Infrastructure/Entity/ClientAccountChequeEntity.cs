using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Entity
{
    public class ClientAccountChequeEntity
    {

        public string ClientAccountAcc { get; set; }
        public string ChequeNumber { get; set; }
        public string NameOnCheque { get; set; }
        public string Printer { get; set; }
        public string Template { get; set; }

    }
}