namespace Elite3E.Infrastructure.Entity
{
    public class ProformaGenerationEntity
    {
        public string Description { get; set; }
        public string IncludeOtherProformas { get; set; }
        public string ProformaStatus { get; set; }
        public string ChangeStatusTo { get; set; }
        public string InvoiceDate { get; set; }
        public string ToTaxArea { get; set; }
        public string Email { get; set; }
        public string Payer { get; set; }
        public string ContactType { get; set; }
        public string ContactName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PresCurrency { get; set; }
        public string PresExchangeRate { get; set; }

        public string InvoiceType { get; set; }

        public string BillingGroup { get; set; }
        public string CoverLetterNarrative { get; set; }

        public string InvoiceNarrative { get; set; }

        public string AlternativeBankDetails { get; set; }
        public string InvoiceDistributionMethod { get; set; }

        public string BillingOffice { get; set; }

        public string FromTaxArea { get; set; }
    }
}
