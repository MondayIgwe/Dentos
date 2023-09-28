using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Entity
{
    public class ClientEntity
    {
        public string StatusDate { get; set; }
        public string ClientNumber { get; set; }
        public string ClientName { get; set; }  
        public string  BillingRulesValidationList { get; set; }
        public string DateOpened { get; set; }
        public string Status { get; set; }
        public string GlobalClientNumber { get; set; }

        public CreditDetailsEntity CreditDetailsEntity { get; set; }
        public ClientDefaultsEntity ClientDefaultsEntity { get; set; }

    }
}
