using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.ResponseModels.OpenProcess
{
    public class PresentationResponseModel
    {
        [JsonProperty("parameters")]
        public Parameters Parameters { get; set; }

    }
    public class Parameters
    {
        [JsonProperty("dataSet")]
        public DataSet DataSet { get; set; }
    }
    public class DataSet
    {
        [JsonProperty("objects")]
        public Objects Objects { get; set; }
    }
    public class Objects
    {
        [JsonProperty("NxOpenProcessPO")]
        public NxOpenProcessPo NxOpenProcessPo { get; set; }
    }
    public class NxOpenProcessPo
    {
        [JsonProperty("rows")]
        public object Rows { get; set; }
    }
}
