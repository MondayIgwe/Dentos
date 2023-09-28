using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiPayerEntity
    {
        public string PayerId { get; set; }
        public string PayerName { get; set; }
        public string Entity { get; set; }
        public string Site { get; set; }
        public string EntityName { get; set; }
        public ValueUnion EntityCode { get; set; }
        public ValueUnion SiteCode { get; set; }
        public ValueUnion TaxAreaCode { get; set; }
        public string TaxArea { get; set; }


    }
}
