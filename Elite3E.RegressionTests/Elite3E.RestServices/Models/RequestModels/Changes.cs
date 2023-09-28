using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels
{
    public class Changes
    {

        [JsonProperty("op")]
        public string Op { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("value")]
        public ValueUnion Value { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
