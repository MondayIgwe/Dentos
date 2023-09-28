using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels.OpenProcess
{
    public class Attributes
    {
        [JsonProperty("NxOpenProcessPOID")]
        public User NxOpenProcessPoid { get; set; }

        [JsonProperty("NxBaseUser")]
        public User NxBaseUser { get; set; }

        [JsonProperty("Process")]
        public User Process { get; set; }

        [JsonProperty("NxBaseUserRel.BaseUserName")]
        public User NxBaseUserRelBaseUserName { get; set; }

        [JsonProperty("NxFWKAppObjectRel.AppObjectCode")]
        public User NxFwkAppObjectRelAppObjectCode { get; set; }
    }
}
