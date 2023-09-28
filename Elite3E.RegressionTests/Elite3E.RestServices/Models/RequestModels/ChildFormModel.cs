using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels
{
    public class ChildFormModel
    {
        [JsonProperty("changes")]
        public List<Changes> Changes { get; set; }

        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("contextId")]
        public string ContextId { get; set; }

        [JsonProperty("startRow")]
        public int StartRow { get; set; }

        [JsonProperty("filterText")]
        public string FilterText { get; set; }

        [JsonProperty("cacheBlockSize")]
        public int CacheBlockSize { get; set; }

        [JsonProperty("resort")]
        public bool Resort { get; set; }

      

    }
}
