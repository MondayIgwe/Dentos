﻿using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels
{
    public class CurrDate
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("direction")]
        public long Direction { get; set; }

        [JsonProperty("order")]
        public long Order { get; set; }
    }
}
