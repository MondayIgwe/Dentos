
namespace Elite3E.Infrastructure.Entity
{
    public class VolumeDiscountEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Office { get; set; }
        public string AdjustmentType { get; set; }
        public string ChargeType { get; set; }
        public string IncreaseChargeType { get; set; }
        public string EffectiveDate { get; set; }
        public string CalculationMethod { get; set; }
        public string Currency { get; set; }
        public string TierAmount { get; set; }
        public string DiscountPercent { get; set; }
    }
}
