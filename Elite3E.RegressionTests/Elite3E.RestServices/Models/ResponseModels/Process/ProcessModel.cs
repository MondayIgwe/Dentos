using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.ResponseModels.Process
{
    public class ProcessModel
    {
        [JsonProperty("processItemId")]
        public Guid ProcessItemId { get; set; }

        [JsonProperty("contextId")]
        public string ContextId { get; set; }
    }
}
