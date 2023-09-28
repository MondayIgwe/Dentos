using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels.OpenProcess
{
    public class Replace
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("index")]
        public long Index { get; set; }

        [JsonProperty("rowState")]
        public long RowState { get; set; }

        [JsonProperty("subclassId")]
        public string SubclassId { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
