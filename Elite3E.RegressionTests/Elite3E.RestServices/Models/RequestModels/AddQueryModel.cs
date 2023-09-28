using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels
{
    public class AddQueryModel
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("selectedRows")]
        public List<Guid> SelectedRows { get; set; }
    }
}
