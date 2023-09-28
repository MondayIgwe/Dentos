using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiMatterEntity
    {
        public string FeeEarnerFullName { get; set; }
        public string Client { get; set; }
        public string Status { get; set; }
        public string OpenDate { get; set; }
        public string MatterId { get; set; }
        public string MatterName { get; set; }
        public string Currency { get; set; }
        public string MatterCurrencyMethod { get; set; }
        public string Office { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public string Rate { get; set; }
        public string EDIPractice { get; set; }
        public List<string> SiteId { get; set; }
        public List<Guid> ChargeTypeGroup { get; set; }
        public List<Guid> CostTypeGroup { get; set; }
        public string BillingGroupDescription { get; set; }
        public ValueUnion BillingGroupCode { get; set; }
        public string ClientAlias { get; set; }
        public ValueUnion ClientRowKey { get; set; }
        public ValueUnion StatusCode { get; set; }
        public ValueUnion CurrencyCode { get; set; }
        public ValueUnion CurrencyMethodCode { get; set; }
        public ValueUnion OfficeKey { get; set; }
        public ValueUnion RateCode { get; set; }
        public ValueUnion DepartmentKey { get; set; }
        public ValueUnion SectionKey { get; set; }
        public ValueUnion EDIPracticeKey { get; set; }
        public string RateAlias { get; set; }
        public string ChargeTypeGroupName { get; set; }
        public string CostTypeGroupName { get; set; }
        public List<string> ChargeTypeGroupList { get; set; }
        public List<string> CostTypeGroupList { get; set; }
        public List<string> BillingGroupList { get; set; }

        public string BillingOffice { get; set; }
        public ValueUnion BillingOfficeCode { get; set; }
        public ValueUnion AdditionalMatterNumber { get; internal set; }
        public ValueUnion CostMarkUp { get; set; }
        public ValueUnion GrossMarkUp { get; set; }
        public ValueUnion PayorCode { get;  set; }
        public string PayorName { get;  set; }
    }
}
