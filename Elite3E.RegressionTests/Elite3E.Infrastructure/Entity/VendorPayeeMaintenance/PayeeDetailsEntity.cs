namespace Elite3E.Infrastructure.Entity.VendorPayeeMaintenance
{
   public class PayeeDetailsEntity
    {
        public string PaymentTerms { get; set; }
        public string Office { get; set; }
        public string Vendor { get; set; }
        public string Site { get; set; }
        public string PayeeType { get; set; }
        public string Unit  { get; set; }
        public string Currency { get; set; }
        public bool Is1099 { get; set; }

        public string PayeeStatus { get; set; }

    }
}
