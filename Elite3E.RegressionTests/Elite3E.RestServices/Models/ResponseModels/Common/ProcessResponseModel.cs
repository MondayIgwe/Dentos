using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.ResponseModels.Common
{
    public class ProcessResponseModel
    {
        [JsonProperty("changes")]
        public List<Change> Changes { get; set; }

        [JsonProperty("contextId")]
        public string ContextId { get; set; }

        [JsonProperty("dataStateChanges")]
        public List<DataStateChange> DataStateChanges { get; set; }
    }

    public partial class Change
    {
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Value { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("op")]
        public string Op { get; set; }
    }
    
    public partial class DataStateChange
    {
        [JsonProperty("value")]
        public DataStateChangeValue Value { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("op")]
        public string Op { get; set; }
    }

    public partial struct DataStateChangeValue
    {
        public FluffyValue FluffyValue;
        public long? Integer;
        public string String;

        public static implicit operator DataStateChangeValue(FluffyValue FluffyValue) => new() { FluffyValue = FluffyValue };
        public static implicit operator DataStateChangeValue(long Integer) => new() { Integer = Integer };
        public static implicit operator DataStateChangeValue(string String) => new() { String = String };
    }

}

