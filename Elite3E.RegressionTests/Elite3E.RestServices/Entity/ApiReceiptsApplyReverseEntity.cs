using Elite3E.RestServices.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.RestServices.Entity
{
    public class ApiReceiptsApplyReverseEntity
    {
        public string Id { get; set; }
        public string ReceiptTypeAlias { get; set; }
        public string ReceiptTypeValue { get; set; }
        public string ReceiptAmount { get; set; }
        public string DocumentNumber { get; set; }
        public ValueUnion Payer { get; set; }
        public string InvoiceNumber { get; set; }
        public string Narrative { get; set; }
        public string MatterNumber { get; set; }
        public List<Guid> InvoiceKey { get; set; }
    }
}
