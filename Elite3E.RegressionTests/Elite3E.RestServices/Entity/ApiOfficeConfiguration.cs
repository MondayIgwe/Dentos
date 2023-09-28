using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiOfficeConfiguration
    {
        public string OfficeId { get; set; }
        public ValueUnion OfficeKey { get; set; }
        public string Office { get; set; }
        public ValueUnion DisbursementTypeKey { get; set; }
        public string DisbursementTypeValue { get; set; }
        public string PayeeKey { get; set; }
        public string TImeKeeper { get; set; }
        public ValueUnion PayeeValue { get; set; }
        public ValueUnion TimeKeeperValue { get; set; }
    }
}
