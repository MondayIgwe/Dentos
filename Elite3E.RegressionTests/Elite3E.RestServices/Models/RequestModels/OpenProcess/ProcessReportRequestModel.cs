using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels.OpenProcess
{
    public class ProcessReportRequestModel
    {
        [JsonProperty("presentation")]
        public Presentation Presentation { get; set; }

        [JsonProperty("topRows")]
        public long TopRows { get; set; }

        [JsonProperty("parameterSet")]
        public ParameterSet ParameterSet { get; set; }

        [JsonProperty("filters")]
        public Filters Filters { get; set; }

        [JsonProperty("currentDay")]
        public string CurrentDay { get; set; }

        [JsonProperty("currentMonth")]
        public string CurrentMonth { get; set; }

        [JsonProperty("currentYear")]
        public string CurrentYear { get; set; }
    }
}
