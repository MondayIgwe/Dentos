using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels
{
    public  class SubmitModel
    {
        [JsonProperty("pageId")]
        public string PageId { get; set; }

        [JsonProperty("isPrint")]
        public bool IsPrint { get; set; }
    }
}
