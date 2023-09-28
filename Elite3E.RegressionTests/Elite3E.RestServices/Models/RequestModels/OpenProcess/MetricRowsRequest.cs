using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels.OpenProcess
{
    public class MetricRowsRequest
    {
        [JsonProperty("useCache")]
        public bool UseCache { get; set; }

        [JsonProperty("startRow")]
        public long StartRow { get; set; }

        [JsonProperty("rowCount")]
        public long RowCount { get; set; }

        [JsonProperty("startColumn")]
        public long StartColumn { get; set; }

        [JsonProperty("columnCount")]
        public long ColumnCount { get; set; }

        [JsonProperty("includeHeader")]
        public bool IncludeHeader { get; set; }

        [JsonProperty("headerColumnCount")]
        public long HeaderColumnCount { get; set; }
    }
}
