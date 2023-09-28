using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiDisbursementEntryEntity
    {
        public string Id { get; set; }
        public ValueUnion MatterRowKey { get; set; }
        public string MatterNumber { get; set; }
        public string Narrative { get; set; }
        public ValueUnion Taxcode { get; set; }
        public string TaxCodeDescription { get; set; }
        public ValueUnion DisbursementTypeCode { get; set; }
        public string DisbursementType { get; set; }
        public ValueUnion WorkRateValue { get; set; }
        public ValueUnion CurrencyCode { get; set; }
        public string Currency { get; set; }
    }
}
