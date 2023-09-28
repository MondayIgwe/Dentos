using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiReceiptTypeEntity
    {
        public ValueUnion Code { get; set; }
        public ValueUnion Description { get; set; }
        public string Id { get; set; }
        public string BankAccountDisplayName { get; set; }
        public ValueUnion BankAccountValue { get; set; }
        public string ToleranceAmount { get; set; }
        public string TolerancePercentage { get; set; }
        public string CurrencyTypeDescription { get; set; }
        public ValueUnion CurrencyTypeValue { get; set; }
        public string OperatingUnit { get;set; }
        public ValueUnion OperatingUnitValue { get;set; }
        

    }
}
