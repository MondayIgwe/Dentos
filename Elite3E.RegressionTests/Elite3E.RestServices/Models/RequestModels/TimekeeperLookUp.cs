using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels
{
    public class TimekeeperLookUp
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("direction")]
        public long Direction { get; set; }
    }
}
