using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels.OpenProcess
{
    public class User
    {
        [JsonProperty("aliasValue")]
        public string AliasValue { get; set; }

        [JsonProperty("dataType")]
        public long DataType { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("accessType")]
        public long? AccessType { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
