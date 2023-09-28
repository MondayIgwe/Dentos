using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels
{
    public class ValueClass
    {
        [JsonProperty("EffStart")]
        public EffStart EffStart { get; set; }

        [JsonProperty("NxStartDate")]
        public NxStartDate NxStartDate { get; set; }

        [JsonProperty("IsActive")]
        public IsActive IsActive { get; set; }

        [JsonProperty("CurrDate")]
        public CurrDate CurrDate { get; set; }

        [JsonProperty("subclassId")]
        public string SubclassId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("IsDefault")]
        public IsDefault IsDefault { get; set; }

        [JsonProperty("Description")]
        public Description Description { get; set; }

        [JsonProperty("TimekeeperLkUp1.Number")]
        public TimekeeperLookUp TimekeeperLookUpNumber { get; set; }

        [JsonProperty("EBillValList")]
        public BillingRulesValidationList EBillValList { get; set; }

      
        [JsonProperty("Matter")]
        public Matter Matter { get; set; }

        [JsonProperty("Amount")]
        public Amount Amount { get; set; }

        [JsonProperty("TrustIntendedUse")]
        public TrustIntendedUse TrustIntendedUse { get; set; }

        [JsonProperty("Role.BaseUserName")]
        public Role RoleBaseUserName { get; set; }

        [JsonProperty("User.BaseUserName")]
        public Role UserBaseName { get; set; }
    }

}
