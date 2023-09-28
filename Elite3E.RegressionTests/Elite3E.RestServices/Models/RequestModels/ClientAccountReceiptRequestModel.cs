using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels
{
    public class ClientAccountReceiptRequestModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("direction")]
        public int Direction { get; set; }

        [JsonProperty("order")]
        public long Order { get; set; }
    }

    public class Amount : ClientAccountReceiptRequestModel
    {
    }

    public class Matter : ClientAccountReceiptRequestModel
    {
    }

    public class TrustIntendedUse : ClientAccountReceiptRequestModel
    {
    }

}