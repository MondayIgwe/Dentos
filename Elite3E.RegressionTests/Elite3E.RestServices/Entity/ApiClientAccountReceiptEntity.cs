using Elite3E.RestServices.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.RestServices.Entity
{
    public class ApiClientAccountReceiptEntity
    {
        public ValueUnion ClientAccountReceiptType { get; set; }
        public ValueUnion ClientAccountAcct { get; set; }
        public ValueUnion DocumentNumber { get; set; }
        public ValueUnion Amount { get; set; }
        public ValueUnion MatterNumber { get; set; }
        public string ClientAccountReceiptId { get; set; }
    }
}
