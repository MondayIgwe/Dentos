
using Elite3E.RestServices.Models.RequestModels;
using Elite3E.RestServices.Models.ResponseModels.Common;
using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.ResponseModels.QuickSearch
{
    public class Attributes
    {
        [JsonProperty("FormattedName")]
        public string FormattedName { get; set; }

        [JsonProperty("PersonType")]
        public string PersonType { get; set; }

        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("InvNumber")]
        public string InvoiceNumber { get; set; }

        [JsonProperty("BalAmt")]
        public string BalanceAmount { get; set; }

        [JsonProperty("IsCostType")]
        public long IsCostType { get; set; }

        [JsonProperty("SortString")]
        public object SortString { get; set; }

        [JsonProperty("IsStandard")]
        public long IsStandard { get; set; }

        [JsonProperty("IsFirmDefault")]
        public long IsFirmDefault { get; set; }

        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }
        [JsonProperty("PayeeNum")]
        public string PayeeNum { get; set; }

        [JsonProperty("Number")]
        public string Number { get; set; }

        [JsonProperty("LeadMatterRel.Number")]
        public string LeadMatterRel { get; set; }

        [JsonProperty("LeadMatter1.Number")]
        public string LeadMatterNumber { get; set; }

        [JsonProperty("PayeeIndex")]
        public string PayeeIndex { get; set; }

        [JsonProperty("BaseUserName")]
        public string BaseUserName { get; set; }

        [JsonProperty("OfficeRel.Description")]
        public object OfficeRelDescription { get; set; }

        [JsonProperty("DefaultCurrency")]
        public object DefaultCurrency { get; set; }

        [JsonProperty("IsActive")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long IsActive { get; set; }

        [JsonProperty("ArchetypeCode")]
        public string ArchetypeCode { get; set; }

        [JsonProperty("GLValue")]
        public string GLValue { get; set; }

        [JsonProperty("CategoryCode")]
        public string CategoryCode { get; set; }

        [JsonProperty("PayorIndex")]
        public string PayerIndex { get; set; }
    }
}
