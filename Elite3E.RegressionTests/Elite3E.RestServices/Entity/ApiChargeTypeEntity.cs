
using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiChargeTypeEntity
    {
        public string ChargeTypeId { get; set; }
        public ValueUnion ChargeCode { get; set; } 
        public ValueUnion Description { get; set; }
        public string CategoryInput { get; set; }
        public ValueUnion CategoryValue { get; set; }
        public ValueUnion TransactionTypeValue { get; set; }
        public string TransactionTypeAlias { get; set; }
        public ValueUnion Active { get; set; }
        public ValueUnion ChargeTypeGroupCode { get; set; }
        public string ChargeTypeGroupDescription { get; set; }

    }
}
