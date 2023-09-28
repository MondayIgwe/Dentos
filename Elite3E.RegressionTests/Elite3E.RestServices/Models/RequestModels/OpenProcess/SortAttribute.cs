using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels.OpenProcess
{
    public class SortAttribute
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("sortDirection")]
        public long SortDirection { get; set; }
    }
}
