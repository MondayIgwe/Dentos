using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiTimeModifyEntity
    {
        public string Id { get; set; }
        public string FeeEranerName { get;set; }
        public string FeeEarnerId { get;set;}
        public ValueUnion FeeEranerRowKey { get;set; }
        public string MatterNumber { get;set; }
        public ValueUnion MatterRowKey { get;set; }
        public string TimeType { get;set; }
        public ValueUnion TimeTypeCode { get;set; }
        public string Narrative { get;set; }
        public ValueUnion Taxcode { get;set; }
        public string TaxCodeDescription { get;set; }
        public string WorkHours { get; set; }
        public string WorkDate { get; set; }
        public string Currency { get; set; }
    }
}
