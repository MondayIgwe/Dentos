using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels.OpenProcess
{
    public class ParameterSet
    {
        [JsonProperty("objects")]
        public Objects Objects { get; set; }
    }
}
