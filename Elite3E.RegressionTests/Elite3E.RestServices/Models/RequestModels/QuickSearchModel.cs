using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels
{
    public class QuickSearchModel
    {
        [JsonProperty("archetypeId")]
        public string ArchetypeId { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("toprows")]
        public long Toprows { get; set; }

        [JsonProperty("processItemId")]
        public string ProcessItemId { get; set; }

        [JsonProperty("addIDAttribute")]
        public bool AddIdAttribute { get; set; }

        [JsonProperty("actionPath")]
        public string ActionPath { get; set; }

        [JsonProperty("select")]
        public Select Select { get; set; }

        [JsonProperty("queryResultAttributes")]
        public List<QueryResultAttributes> QueryResultAttributes { get; set; }
    }

        public partial class Select
        {
            [JsonProperty("where")]
            public Where Where { get; set; }

            [JsonProperty("archetype")]
            public string Archetype { get; set; }

            [JsonProperty("archetypeType")]
            public long ArchetypeType { get; set; }

            [JsonProperty("joins")]
            public List<Joins> Joins { get; set; }
        }

        public class Where
        {
            [JsonProperty("operator")]
            public string Operator { get; set; }

            [JsonProperty("predicates")]
            public List<Predicate> Predicates { get; set; }

            [JsonProperty("groups")]
            public List<object> Groups { get; set; }
        }

        public class Predicate
        {
            [JsonProperty("attribute")]
            public string Attribute { get; set; }

            [JsonProperty("operator")]
            public string Operator { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }
        }

        public class Joins
        {
            [JsonProperty("from")]
            public string From { get; set; }

            [JsonProperty("to")]
            public string To { get; set; }
        }

        public class QueryResultAttributes
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("captionId")]
            public string CaptionId { get; set; }

            [JsonProperty("caption")]
            public string Caption { get; set; }

            [JsonProperty("dataType")]
            public int DataType { get; set; }
        }
}
