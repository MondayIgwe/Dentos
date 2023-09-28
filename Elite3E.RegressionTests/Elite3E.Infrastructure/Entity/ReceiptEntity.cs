namespace Elite3E.Infrastructure.Entity
{
    public class ReceiptEntity
    {
        public string ReceiptType { get; set; }

        public string DocumentNumber { get; set; }

        public string Narrative { get; set; }

        public string ReceiptDate { get; set; }

        public string ReceiptAmount { get; set; }
        public string OperatingUnit { get; set; }
        public string Payer { get; set; }
        public string ChequeDate { get; set; }
        public string DepositNumber { get; set; }
    }
}

