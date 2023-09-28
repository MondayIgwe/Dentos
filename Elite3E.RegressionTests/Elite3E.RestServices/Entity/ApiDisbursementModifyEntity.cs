using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiDisbursementModifyEntity
    {
        public string Id { get; set; }
        public ValueUnion MatterRowKey { get; set; }
        public string MatterNumber { get; set; }
        public string Narrative { get; set; }
        public ValueUnion Taxcode { get; set; }
        public string TaxCodeDescription { get; set; }
        public ValueUnion DisbursementTypeCode { get; set; }
        public string DisbursementType { get; set; }
        public string WorkRate { get; set; }
        public ValueUnion CurrencyCode { get; set; }
        public string Currency { get; set; }
        public  string WorkDate { get; set; }
        public string WorkQuantity { get; set; }
        
    }
}
