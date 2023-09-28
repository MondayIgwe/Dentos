
using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public  class ApiClientMaintenanceEntity
    {
        public string Id { set; get; }
        public string EntityName { get; set; }
        public string FeeEarnerFullName { get; set; }
        public string Entity { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public ValueUnion CountryCode { get; set; }
        public ValueUnion CurrencyCode { get; set; }
        public string Status { get; set; }
        public ValueUnion StatusCode { get; set; }
        public string Office { get; set; }
        public string OfficeKey { get; set; }
        public string BillingFeeEarnerAlias { get; set; }
        public string ResponsibleFeeEarnerAlias { get; set; }
        public string SupervisingFeeEarnerAlias { get; set; }
        public string BillingFeeEarnerName { get; set; }
        public ValueUnion ResponsibleFeeEarnerRowKey { get; set; }
        public ValueUnion BillingFeeEarnerRowKey { get; set; }
        public string ResponsibleFeeEarnerName { get; set; }
        public string SupervisingFeeEarnerName { get; set; }
        public string OpeningFeeEarnerName { get; set; }
        public string OpeningFeeEarnerAlias { get; set; }
        public string InvoiceSiteName { get; set; }
        public ValueUnion InvoiceSite { get; set; }
        public ValueUnion OpeningFeeEarnerKey { get; set; }
        public ValueUnion SupervisingFeeEarnerRowKey { get; set; }
        public ValueUnion Name { get; set; }
        public string ClientNumber { get; set; }
        public string ClientNumberRowKeyValue { get; set; }
        public string BillingRulesValidationName { get; set; }
        public List<Guid> BillingRulesValidationKey { get; set; }
        public List<string> BillingRulesValidationList { get; set; }
        public string DateOpened { get; set; }
        public string EDIStartDate { get; set; }
    }
}
