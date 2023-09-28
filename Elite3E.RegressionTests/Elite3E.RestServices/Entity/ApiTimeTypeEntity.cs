using Elite3E.RestServices.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.RestServices.Entity
{
    public class ApiTimeTypeEntity
    {
        public string Id { get; set; }
        public string Code { get; set; }

        public string Description { get; set; }
        public string TransactionType { get; set; }      

        public ValueUnion TransactionTypeCode { get; set; }
        public string Currency { get; set; }
        public ValueUnion CurrencyCode { get; set; }
    }
}
