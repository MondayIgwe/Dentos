using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels.OpenProcess
{
    public class Presentation
    {
        [JsonProperty("downDimensions")]
        public List<object> DownDimensions { get; set; }

        [JsonProperty("pageDimensions")]
        public List<EDimension> PageDimensions { get; set; }

        [JsonProperty("reportAvailableDimensions")]
        public List<EDimension> ReportAvailableDimensions { get; set; }

        [JsonProperty("viewMode")]
        public long ViewMode { get; set; }

        [JsonProperty("boundId")]
        public string BoundId { get; set; }

        [JsonProperty("boundType")]
        public long BoundType { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
