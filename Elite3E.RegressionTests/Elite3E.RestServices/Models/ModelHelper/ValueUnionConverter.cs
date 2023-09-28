using Elite3E.RestServices.Models.RequestModels;
using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.ModelHelper
{
    public class ValueUnionConverter : JsonConverter
    {
        public static readonly ValueUnionConverter Singleton = new();

        public override bool CanConvert(Type t) => t == typeof(ValueUnion) || t == typeof(ValueUnion?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Boolean:
                    var boolValue = serializer.Deserialize<bool>(reader);
                    return new ValueUnion { Bool = boolValue };
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<ValueClass>(reader);
                    return new ValueUnion { ValueClass = objectValue };
                case JsonToken.Date:
                case JsonToken.String:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new ValueUnion { String = stringValue };
            }
            throw new Exception("Cannot unmarshal type ValueUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (ValueUnion)untypedValue;
            try
            {
                if (value.Bool != null)
                {
                    serializer.Serialize(writer, value.Bool.Value);
                    return;
                }
                if (value.ValueClass != null)
                {
                    serializer.Serialize(writer, value.ValueClass);
                    return;
                }
                if (value.String != null)
                {
                    serializer.Serialize(writer, value.String);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
                      
        }

    }
}
