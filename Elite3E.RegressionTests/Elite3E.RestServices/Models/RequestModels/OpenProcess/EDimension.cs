using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels.OpenProcess
{
    public class EDimension
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("showSubTotalsWithSingleRows")]
        public bool ShowSubTotalsWithSingleRows { get; set; }

        [JsonProperty("showGroupWithRowsAllZeros")]
        public bool ShowGroupWithRowsAllZeros { get; set; }

        [JsonProperty("showRowsWithAllZeros")]
        public bool ShowRowsWithAllZeros { get; set; }

        [JsonProperty("indent")]
        public bool Indent { get; set; }

        [JsonProperty("showTotals")]
        public bool ShowTotals { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("sortAttributes")]
        public List<SortAttribute> SortAttributes { get; set; }
    }
}
