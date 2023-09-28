using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels.OpenProcess
{
    public class Where
    {
        [JsonProperty("operator")]
        public string Operator { get; set; }

        [JsonProperty("groups")]
        public List<object> Groups { get; set; }

        [JsonProperty("predicates")]
        public List<object> Predicates { get; set; }
    }
}
