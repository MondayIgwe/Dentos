using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels
{
    public class AddBatchModel
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("itemIDs")]
        public List<Guid> ItemIDs { get; set; }
    }
}
