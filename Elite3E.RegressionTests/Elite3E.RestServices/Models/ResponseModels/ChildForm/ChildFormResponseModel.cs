using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.ResponseModels.ChildForm
{
    public  class ChildFormResponseModel
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("objects")]
        public Objects Objects { get; set; }
    }

    public partial class Objects
    {
        [JsonProperty("MattDate")]
        public MattDate MattDate { get; set; }
    }

    public partial class MattDate
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("rows")]
        public dynamic Rows { get; set; }
    }
}

