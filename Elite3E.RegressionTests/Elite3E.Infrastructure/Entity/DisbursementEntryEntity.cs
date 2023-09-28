namespace Elite3E.Infrastructure.Entity
{
    public class DisbursementEntryEntity
    {
        public string MatterNumber { get; set; }
        public string DisbursementType { get; set; }
        public string WorkCurrency { get; set; }
        public string RefCurrency { get; set; }
        public string WorkAmount { get; set; }
        public string Narrative { get; set; }
        public string WorkDate { get; set; }
        public string TaxCode { get; set; }
    }
}
