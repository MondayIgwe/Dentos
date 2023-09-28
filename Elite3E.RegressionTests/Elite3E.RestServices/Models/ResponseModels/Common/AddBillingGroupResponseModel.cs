using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Elite3E.RestServices.Models.ResponseModels.Common
{
    public class AddBillingGroupResponseModel
    {

        [JsonProperty("dataStateChanges")]
        public List<DataStateChange> DataStateChanges { get; set; }
    }

    //public partial class Change
    //{
    //    [JsonProperty("value")]
    //    public ChangeValue Value { get; set; }

    //    [JsonProperty("path")]
    //    public string Path { get; set; }

    //    [JsonProperty("op")]
    //    public string Op { get; set; }
    //}

    public partial class PurpleValue
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("index")]
        public long Index { get; set; }

        [JsonProperty("rowState")]
        public long RowState { get; set; }

        [JsonProperty("subclassId")]
        public string SubclassId { get; set; }
    }

    public partial class Attributes
    {
        [JsonProperty("BillingGroup")]
        public BillingGroup BillingGroup { get; set; }

        [JsonProperty("IsLead")]
        public IsLead IsLead { get; set; }
    }

    public partial class BillingGroup
    {
        [JsonProperty("accessType")]
        public long AccessType { get; set; }

        [JsonProperty("aliasValue")]
        public string AliasValue { get; set; }

        [JsonProperty("dataType")]
        public long DataType { get; set; }
    }

    public partial class IsLead
    {
        [JsonProperty("dataType")]
        public long DataType { get; set; }

        [JsonProperty("value")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Value { get; set; }
    }

    //public partial class DataStateChange
    //{
    //    [JsonProperty("value")]
    //    public DataStateChangeValue Value { get; set; }

    //    [JsonProperty("path")]
    //    public string Path { get; set; }

    //    [JsonProperty("op")]
    //    public string Op { get; set; }
    //}

    public partial class FluffyValue
    {
        [JsonProperty("childStates")]
        public ChildStates ChildStates { get; set; }
    }

    public partial class ChildStates
    {
    }

    public partial struct ChangeValue
    {
        public long? Integer;
        public PurpleValue PurpleValue;

        public static implicit operator ChangeValue(long Integer) => new() { Integer = Integer };
        public static implicit operator ChangeValue(PurpleValue PurpleValue) => new() { PurpleValue = PurpleValue };
    }

    //public partial struct DataStateChangeValue
    //{
    //    public FluffyValue FluffyValue;
    //    public long? Integer;
    //    public string String;

    //    public static implicit operator DataStateChangeValue(FluffyValue FluffyValue) => new DataStateChangeValue { FluffyValue = FluffyValue };
    //    public static implicit operator DataStateChangeValue(long Integer) => new DataStateChangeValue { Integer = Integer };
    //    public static implicit operator DataStateChangeValue(string String) => new DataStateChangeValue { String = String };
    //}

    public partial class Welcome2
    {
        public static Welcome2 FromJson(string json) => JsonConvert.DeserializeObject<Welcome2>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Welcome2 self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new()
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ChangeValueConverter.Singleton,
                DataStateChangeValueConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ChangeValueConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ChangeValue) || t == typeof(ChangeValue?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    var integerValue = serializer.Deserialize<long>(reader);
                    return new ChangeValue { Integer = integerValue };
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<PurpleValue>(reader);
                    return new ChangeValue { PurpleValue = objectValue };
            }
            throw new Exception("Cannot unmarshal type ChangeValue");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (ChangeValue)untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value);
                return;
            }
            if (value.PurpleValue != null)
            {
                serializer.Serialize(writer, value.PurpleValue);
                return;
            }
            throw new Exception("Cannot marshal type ChangeValue");
        }

        public static readonly ChangeValueConverter Singleton = new();
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new();
    }

    internal class DataStateChangeValueConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(DataStateChangeValue) || t == typeof(DataStateChangeValue?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    var integerValue = serializer.Deserialize<long>(reader);
                    return new DataStateChangeValue { Integer = integerValue };
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new DataStateChangeValue { String = stringValue };
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<FluffyValue>(reader);
                    return new DataStateChangeValue { FluffyValue = objectValue };
            }
            throw new Exception("Cannot unmarshal type DataStateChangeValue");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (DataStateChangeValue)untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value);
                return;
            }
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.FluffyValue != null)
            {
                serializer.Serialize(writer, value.FluffyValue);
                return;
            }
            throw new Exception("Cannot marshal type DataStateChangeValue");
        }

        public static readonly DataStateChangeValueConverter Singleton = new();
    }







}
