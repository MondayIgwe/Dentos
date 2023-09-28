using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiChargeModifyEntity
    {
        public string Id { get; set; }
        public ValueUnion MatterRowKey { get; set; }
        public string MatterNumber { get; set; }
        public string Narrative { get; set; }
        public ValueUnion Taxcode { get; set; }
        public string TaxCodeDescription { get; set; }
        public ValueUnion ChargeTypeCode { get; set; }
        public string ChargeType { get; set; }
        public string Amount { get; set; }
        public ValueUnion CurrencyCode { get; set; }
        public string Currency { get; set; }
    }
}
