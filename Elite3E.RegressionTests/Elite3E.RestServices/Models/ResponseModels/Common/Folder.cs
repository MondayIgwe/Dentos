using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Elite3E.RestServices.Models.ResponseModels.Common
{
    public partial class Folder
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("checkSum")]
        public long CheckSum { get; set; }

        [JsonProperty("unit")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Unit { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("priority")]
        public long Priority { get; set; }

        [JsonProperty("unitList")]
        public UnitList UnitList { get; set; }
    }

    public partial class UnitList
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }
    }
}
