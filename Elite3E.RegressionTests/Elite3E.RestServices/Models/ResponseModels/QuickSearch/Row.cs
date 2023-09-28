using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.ResponseModels.QuickSearch
{
    public class Row
    {
        [JsonProperty("rowKey")]
        public string RowKey { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("processItemId")]
        public string ProcessItemId { get; set; }
    }
}
