using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.ResponseModels.Session
{
    public class Session
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
