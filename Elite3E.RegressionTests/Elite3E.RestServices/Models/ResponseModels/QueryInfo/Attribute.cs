using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.ResponseModels.QueryInfo
{
    public class Attribute
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("dataType", NullValueHandling = NullValueHandling.Ignore)]
        public long? DataType { get; set; }

        [JsonProperty("captionId", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? CaptionId { get; set; }

        [JsonProperty("sortDirection", NullValueHandling = NullValueHandling.Ignore)]
        public long? SortDirection { get; set; }
    }
}
