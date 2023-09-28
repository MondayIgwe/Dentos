
using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class FiscalInvoiceSetupEntity
    {
        public string FiscalSetupId { get; set; }
        public ValueUnion Unit { get; set; } 
        public ValueUnion BillGlTypeValue { get; set; }
        public string BillGlTypeAlias { get; set; }
        public string SuspenseGlTypeAlias { get; set; }
        public ValueUnion SuspenseGlTypeValue { get; set; }
        public string FiscalInvoicePrefix { get; set; }
        public string NextFiscalInvoiceNumber { get; set; }
        public string UnitDescription { get; set; }

    }
}
