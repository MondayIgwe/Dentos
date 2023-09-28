namespace Elite3E.Infrastructure.Entity.FeeEarnerMaintenance
{
    public class FeeEarnerMaintenanceEntity
    {
        public string Entity { get; set; }
        
        public string DisplayName { get; set; }

        public string Status { get; set; }

        public string Signature { get; set; }

        public string LocalLanguageName { get; set; }
        public string Office { get; set; }
        public string Title { get; set; }
        public string RateClass { get; set; }
        public string RateType { get; set; }
        public string Currency { get; set; }
        public string DefaultRate { get; set; }

        public string Language { get; set; }
        public string CoverLetterNarrative { get; set; }

    }
}
