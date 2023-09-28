using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.ResponseModels.LookUpResponseModel
{
    public class LookUpListItems
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }
}
