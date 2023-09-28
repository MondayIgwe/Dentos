using Elite3E.RestServices.Models.RequestModels;

namespace Elite3E.RestServices.Entity
{
    public class ApiUnallocatedTypeEntity
    {
        public ValueUnion Code { get; set; }
        public ValueUnion Description { get; set; }
        public string Id { get; set; }
        public string BankAccountDisplayName { get; set; }
        public ValueUnion BankAccountValue { get; set; }
        public ValueUnion ToleranceAmount { get; set; }
        public ValueUnion TolerancePercentage { get; set; }
        public string CurrencyTypeDescription { get; set; }
        public ValueUnion CurrencyTypeValue { get; set; }       

    }
}
