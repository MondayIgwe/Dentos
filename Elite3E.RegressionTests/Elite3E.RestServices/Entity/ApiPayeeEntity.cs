using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiPayeeEntity
    {
        public string PayeeId { get; set; }
        public ValueUnion Name { get; set; }
        public ValueUnion VendorValue { get; set; }
        public ValueUnion EntityValue { get; set; }
        public string EntityName { get; set; }
        public string VendorName { get; set; }
        public ValueUnion PaymentTermvalue { get; set; }
        public string PaymentTerm { get; set; }
        public ValueUnion OfficeCode { get; set; }
        public ValueUnion UnitCode { get; set; }

        public ValueUnion CurrencyCode { get; set; }
         public string Office { get; set; }
        public string Unit { get; set; }

        public string Currency { get; set; }
        public string Status { get; set; }
        public ValueUnion StatusCode { get; set; }

    }
}
