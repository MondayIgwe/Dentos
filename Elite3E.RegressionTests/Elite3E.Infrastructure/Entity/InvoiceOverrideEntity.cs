using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Entity
{
    public class InvoiceOverrideEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string Office { get; set; }
        public string OverrideValue { get; set; }

        public string InvoiceType { get; set; }

        public string NextInvoiceNumber { get; set; }

        public string NextTaxInvoiceNumber { get; set; }
        public string NextCreditNoteNumber { get; set; }

        public string NextCreditNoteTaxNumber { get; set; }
    }
}
