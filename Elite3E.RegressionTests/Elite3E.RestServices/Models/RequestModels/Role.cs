using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.RestServices.Models.RequestModels
{
    public class Role
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("direction")]
        public long Direction { get; set; }

        [JsonProperty("order")]
        public long Order { get; set; }

    }

    public class BaseUser : Role
    {
    }

}
