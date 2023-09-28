using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels.OpenProcess
{
    public class OpenProcessRequestModel
    {
        [JsonProperty("pageId")]
        public string PageId { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("isPrint")]
        public string IsPrint { get; set; }
    }
    public class Data
    {
        [JsonProperty("objects")]
        public Objects Objects { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }
    }

    public class Objects
    {
        [JsonProperty("NxOpenProcessPO")]
        public NxOpenProcessPo NxOpenProcessPo { get; set; }
    }
    public class NxOpenProcessPo
    {
        [JsonProperty("actions")]
        public Actions Actions { get; set; }

        [JsonProperty("actualRowCount")]
        public long ActualRowCount { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("objectId")]
        public string ObjectId { get; set; }

        [JsonProperty("rowCount")]
        public long RowCount { get; set; }

        [JsonProperty("rows")]
        public Rows Rows { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

}
