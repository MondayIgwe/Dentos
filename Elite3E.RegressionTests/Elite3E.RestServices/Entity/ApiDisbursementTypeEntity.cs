using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiDisbursementTypeEntity
    {
        public string DisbursementTypeId { get; set; }
        public ValueUnion Code { get; set; }
        public ValueUnion Description { get; set; }
        public ValueUnion TransactionTypeValue { get; set; }
        public string TransactionTypeAlias { get; set; }
        public ValueUnion IsHardDisbursementOrSoftDisbursementValue { get; set; }
        public string IsHardDisbursementOrSoftDisbursementOption { get; set; }
        public string GroupDescription { get; set; }
        public string IsBarristerFlag { get; set; } = "false";
    }
}
