using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.ResponseModels.QuickSearch
{
    public class QuickSearchResponseModel
    {
        [JsonProperty("rowCount")]
        public long RowCount { get; set; }

        [JsonProperty("rows")]
        public List<Row> Rows { get; set; }
    }
}
