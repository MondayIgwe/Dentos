
using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiChargeTypeGroupEntity
    {
        public string ChargeTypeGroupId { get; set; }
        public ValueUnion ChargeTypeGroupCode { get; set; } 
        public ValueUnion ChargeTypeGroupDescription { get; set; }
        public ValueUnion ChargeTypeGroupIsExcludeOrIncludeListValue { get; set; }
        public string ChargeTypeGroupExcludeOrIncludeListOption { get; set; }

        public ValueUnion ChargeTypeDetailId { get; set; }

    }
}
