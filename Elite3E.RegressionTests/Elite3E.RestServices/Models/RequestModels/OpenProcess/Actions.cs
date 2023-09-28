using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels.OpenProcess
{
    public class Actions
    {
        [JsonProperty("Add")]
        public Action AddAction { get; set; }

        [JsonProperty("Delete")]
        public Action DeleteAction { get; set; }
    }
}
