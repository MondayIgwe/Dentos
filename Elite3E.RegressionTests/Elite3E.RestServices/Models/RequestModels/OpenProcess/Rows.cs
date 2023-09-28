using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels.OpenProcess
{
    public class Rows
    {
        //Replace value before sending the request
        [JsonProperty("replace")]
        public Replace ReplaceValue { get; set; }
    }
}
