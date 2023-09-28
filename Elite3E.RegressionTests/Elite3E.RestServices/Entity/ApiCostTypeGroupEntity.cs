using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiCostTypeGroupEntity
    {
        public string CostTypeId { get; set; }
        public ValueUnion Code { get; set; }
        public ValueUnion Description { get; set; }
        public ValueUnion CostTypeGroupIsExcludeOrIncludeListValue { get; set; }
        public string CostTypeGroupExcludeOrIncludeListOption { get;  set; }
        public string CostTypeDetailId { get; set; }
    }
}
