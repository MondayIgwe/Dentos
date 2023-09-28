namespace Elite3E.Infrastructure.Entity.FeeEarnerMaintenance
{
    public class FeeEarnerEntity
    {
        public string Id { get; set; }
        public string TransactionType { get; set; }
        public string WorkDate { get; set; }
        public string FeeEarnerNumber { get; set; }
        public string Name { get; set; }
        public bool IsResponsible { get; set; } = true;
        public bool IsOwner { get; set; } = true;
        public string User { get; set; } 
    }
}
