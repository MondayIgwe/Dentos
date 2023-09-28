using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Elite3E.RestServices.Models.ModelHelper
{
    public class JsonHelper
    {

        public static readonly JsonSerializerSettings Settings = new()
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ValueUnionConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };

        public static string JsonReaderChecker(string value, string tag, int numberchecks)
        {
            bool on = false;
            int num = 0;
            JsonTextReader reader = new JsonTextReader(new StringReader(value));
            while (reader.Read())
            {
                if (reader.Value == null)
                {
                    continue;
                }
                if (on && numberchecks.Equals(num))
                {
                    return reader.Value.ToString();
                }
                if (reader.Value.ToString().Equals(tag))
                {
                    on = true;
                    num++;
                }
            }
            return null;
        }

        public static string GetTagValue2(String soapResponse, String tag)
        {
            try
            {
                soapResponse = soapResponse.ToLower();
                tag = tag.ToLower();
                String s1 = soapResponse.Substring(soapResponse.IndexOf("\"" + tag + "\""));
                String s2 = s1.Replace("\"" + tag + "\"", "");
                String s4 = s2.Replace(":\"", "");
                String s3 = s4.Substring(0, s4.IndexOf("\""));
                return s3;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return null;
            }

        }
    }
}
