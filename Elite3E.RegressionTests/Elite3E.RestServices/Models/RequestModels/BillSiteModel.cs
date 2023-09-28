using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels
{
    public class BillSiteModel
    {
        [JsonProperty("interfaceId")]
        public string InterfaceId { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
