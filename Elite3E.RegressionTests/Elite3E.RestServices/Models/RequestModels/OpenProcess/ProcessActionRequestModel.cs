using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels.OpenProcess
{
    public class ProcessActionRequestModel
    {
        [JsonProperty("actionData")]
        public string ActionData { get; set; }

        [JsonProperty("filters")]
        public Filters Filters { get; set; }

        [JsonProperty("isReload")]
        public bool IsReload { get; set; }

        [JsonProperty("parameterSet")]
        public ParameterSet ParameterSet { get; set; }

        [JsonProperty("presentation")]
        public Presentation Presentation { get; set; }

        [JsonProperty("startDate")]
        public DateTimeOffset StartDate { get; set; }

        [JsonProperty("metricRowsRequest")]
        public MetricRowsRequest MetricRowsRequest { get; set; }

        [JsonProperty("topRows")]
        public long TopRows { get; set; }
    }
}
