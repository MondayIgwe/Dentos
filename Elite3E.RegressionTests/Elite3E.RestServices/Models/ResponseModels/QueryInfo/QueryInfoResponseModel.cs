using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.ResponseModels.QueryInfo
{
    public class QueryInfoResponseModel
    {
        [JsonProperty("archetypeId")]
        public string ArchetypeId { get; set; }

        [JsonProperty("archetypeType")]
        public long ArchetypeType { get; set; }

        [JsonProperty("queryAttributes")]
        public List<Attribute> QueryAttributes { get; set; }

        [JsonProperty("queryResultAttributes")]
        public List<Attribute> QueryResultAttributes { get; set; }

        [JsonProperty("sortAttributes")]
        public List<Attribute> SortAttributes { get; set; }

        [JsonProperty("topRows")]
        public long TopRows { get; set; }

        [JsonProperty("aliasAttributeId")]
        public string AliasAttributeId { get; set; }

        [JsonProperty("keyAttributeId")]
        public string KeyAttributeId { get; set; }

        [JsonProperty("qfDesc")]
        public string QfDesc { get; set; }

        [JsonProperty("filterDesc")]
        public string FilterDesc { get; set; }

        [JsonProperty("archDesc")]
        public string ArchDesc { get; set; }
    }
}
