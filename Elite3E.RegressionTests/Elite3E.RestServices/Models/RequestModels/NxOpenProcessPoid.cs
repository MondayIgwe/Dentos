using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels
{
    public class NxOpenProcessPoid
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
