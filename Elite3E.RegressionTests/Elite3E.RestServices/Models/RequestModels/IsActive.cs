using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels
{
    public class IsActive
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("direction")]
        public int Direction { get; set; }
    }
}
