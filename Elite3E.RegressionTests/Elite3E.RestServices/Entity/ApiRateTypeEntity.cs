using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public  class ApiRateTypeEntity
    {
        public string RateTypeId { get; set; }
        public string RateTypeCode { get; set; }
        public string RateTypeDescription { get; set; }
        public ValueUnion RateTypeCurrency { get; set; }
        public string RateTypeCurrencyDisplayName { get; set; }
        public ValueUnion EffectiveDate { get; set; }
        public ValueUnion DefaultRateAmount { get; set; }
        public string IsStandardRateCheckbox { get; set; }
        public ValueUnion IsStandardRateCheckboxValue { get; set; }
        public string IsTimeKeeperCheckbox { get; set; }
        public ValueUnion IsTimeKeeperCheckboxValue { get; set; }
        public string IsDisbursementCheckbox { get; set; }
        public ValueUnion IsDisbursementCheckboxValue { get; set; }
        public string IsFirmDefaultCheckbox { get; set; }
        public ValueUnion IsFirmDefaultCheckboxValue { get; set; }
        public string IsValidForTimekeeperCheckboxes { get; set; }
        public ValueUnion IsValidForTimekeeperCheckboxesValues { get; set; }
        public string IsValidForMatterCheckboxes { get; set; }
        public ValueUnion IsValidMatterCheckboxesValues { get; set; }
    }
}
