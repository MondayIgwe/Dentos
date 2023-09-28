using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels
{
    public class QueryInfoResquestModel
    {
        [JsonProperty("archetypeId")]
        public string ArchetypeId { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }
    }
}
