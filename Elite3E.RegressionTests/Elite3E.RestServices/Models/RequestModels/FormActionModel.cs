using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels
{
    public class FormActionModel
    {
        [JsonProperty("contextId")]
        public Guid ContextId { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
