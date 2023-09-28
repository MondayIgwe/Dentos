using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.ResponseModels.LookUpResponseModel
{
    public class Item
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }
    }
}
