using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels.OpenProcess
{
    public class Action
    {
        [JsonProperty("accessType")]
        public long AccessType { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
