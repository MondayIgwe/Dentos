using Elite3E.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Entity
{
    public class InvoiceDistributionMethodEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public InvoiceDispatchOptions DisptchOption { get; set; }
        public string Default { get; set; }

    }
}
