using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiBillingRulesValidationListEntity
    {
        public ValueUnion Description { get; set; }
        public ValueUnion Code { get; set; }        
        public string Id { get; set; }
        public List<RulesList> BillingRulesValidationListRules { get; set; }
    }

    public class RulesList
    {
        public string BillRuleDescrption { get; set; }
        public ValueUnion BillRuleCode { get; set; }
        public ValueUnion IsPendingWarning { get; set; }
        public ValueUnion IsPostWarning { get; set; }
        public ValueUnion IsProformaGen { get; set; }
        public ValueUnion IsProformaEdit { get; set; }
        public ValueUnion IsBillWarning { get; set; }
        public ValueUnion IsPendingError { get; set; }
        public ValueUnion IsPostError { get; set; }
        public ValueUnion IsBillError { get; set; }
        public string RowId { get; set; }
    }

}
