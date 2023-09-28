using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiFeeEarnerEntity
    {
        public string EntityFirstName { get; set; }
        public string EntityLastName { get; set; }
        public string FeeEarnerId { get; set; }
        public ValueUnion Entity { get; set; }
        public string EntityName { get; set; }
        public string EntityType { get; set; }
        public string Office { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public string Title { get; set; }
        public string OfficeKey { get; set; }
        public string DepartmentKey { get; set; }
        public string SectionKey { get; set; }
        public string TitleKey { get; set; }
        public string RateClass { get; set; }
        public string RateClassKey { get; set; }
        public string RateTypeDescription { get; set; }
        public string RateTypeFirmStandardRate { get; set; }
        public List<Guid> RateTypeKey { get; set; }
        public ValueUnion EffectiveRateCurrency { get; set; }
        public ValueUnion EffectiveRate { get; set; }
        public string EffectiveRateCurrencyDescription { get; set; }
        public string EDIStartDate { get; set; }
        public string FeeEarnerNumber { get; set; } //used for UI
        public string FeeEarnerRatesStartDate { get; set; }
        public string WorkflowUserAlias { get; set; }
        public string WorkflowUserValue { get; set; }
    }
}
