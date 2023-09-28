using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels.OpenProcess
{
    public class Filters
    {
        [JsonProperty("NxOpenProcesses")]
        public NxOpenProcesses NxOpenProcesses { get; set; }
    }
}
