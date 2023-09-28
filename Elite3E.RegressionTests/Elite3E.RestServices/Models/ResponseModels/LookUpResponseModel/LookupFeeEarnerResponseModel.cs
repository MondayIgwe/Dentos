using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.ResponseModels.LookUpResponseModel
{
    public partial class LookupFeeEarnerResponseModel
    {
        [JsonProperty("rowCount")]
        public long RowCount { get; set; }

        [JsonProperty("rows")]
        public List<Row> Rows { get; set; }
    }

    public partial class Row
    {
        [JsonProperty("rowKey")]
        public Guid RowKey { get; set; }

        [JsonProperty("processItemId")]
        public Guid ProcessItemId { get; set; }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }
    }

    //public partial class Attributes
    //{
    //    [JsonProperty("Number")]
    //    [JsonConverter(typeof(ParseStringConverter))]
    //    public long Number { get; set; }

    //    [JsonProperty("DisplayName")]
    //    public string DisplayName { get; set; }

    //    [JsonProperty("AltNumber")]
    //    public object AltNumber { get; set; }

    //    [JsonProperty("TkprStatus")]
    //    public string TkprStatus { get; set; }
    //}
}
