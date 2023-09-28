using System.Collections.Generic;
using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.ResponseModels.Profile
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Add
    {
    }

    public class Delete
    {
    }

    public class Remove
    {
    }

    public class Audit
    {
        public int accessType { get; set; }
    }

    public class Attachments
    {
        public int accessType { get; set; }
    }

    public class ACL
    {
        public int accessType { get; set; }
    }

    public class Actions
    {
        public Add Add { get; set; }
        public Delete Delete { get; set; }
        public Remove Remove { get; set; }
        public Audit Audit { get; set; }
        public Attachments Attachments { get; set; }
        public ACL ACL { get; set; }
    }

    public class RoleID
    {
        public string AliasValue { get; set; }
        public int DataType { get; set; }
        public string Value { get; set; }
    }

    public class RolePrecedence
    {
        public int dataType { get; set; }
        public string value { get; set; }
    }

    public class Attributes
    {
        public RoleID RoleID { get; set; }
        public RolePrecedence RolePrecedence { get; set; }
    }



 
    public partial class Row
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("index")]
        public long Index { get; set; }

     
    }
    
    public class NxUserRoleChild
    {
        public Actions actions { get; set; }
        public int actualRowCount { get; set; }
        public string id { get; set; }
        public string objectId { get; set; }
        public int rowCount { get; set; }
        public Dictionary<string, Row> Rows { get; set; }
    }

    public class Objects
    {
        public NxUserRoleChild NxUser_RoleChild { get; set; }
    }

    public class Data
    {
        public Objects objects { get; set; }
    }

    public class BaseUserRoot
    {
        public Data data { get; set; }
    }


} 