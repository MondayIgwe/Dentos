using System.Xml;
using System.Xml.Serialization;

namespace Elite3E.SoapServices
{
    public static class XmlExtensions
    {
        public static string Serialize<T>(T value)
        {
            if (value == null) return string.Empty;

            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var stringWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
                {
                    xmlSerializer.Serialize(xmlWriter, value);
                    return stringWriter.ToString();
                }
            }
        }

        public static T Deserialise<T>(string xmlString )
        {
            T returnObject = default(T);

            if (string.IsNullOrEmpty(xmlString)) return default(T);
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (StringReader reader = new StringReader(xmlString))
            {
                returnObject = (T) xmlSerializer.Deserialize(reader);
            }

            return returnObject;
        }
        
    }
}
