using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiBillingGroupEntity
    {
        public string Id { get; set; }
        public ValueUnion Name { get; set; } 
        public ValueUnion Description { get; set; }
        public ValueUnion Icb { get; set; }
        public string UnitDueFromDescription { get; set; }
        public string UnitDueToDescription { get; set; }
        public string MatterNumber { get; set; }
        public ValueUnion UnitDueToValue { get; set; }
        public ValueUnion UnitDueFromValue { get; set; }
        public List<Guid> MatterKeyValue { get; set; }
    }
}
