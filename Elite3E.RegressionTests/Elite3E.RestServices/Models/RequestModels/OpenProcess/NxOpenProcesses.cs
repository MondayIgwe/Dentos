using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Elite3E.RestServices.Models.RequestModels.OpenProcess
{
    public class NxOpenProcesses
    {
        [JsonProperty("where")]
        public Where Where { get; set; }

        [JsonProperty("archetype")]
        public string Archetype { get; set; }

        [JsonProperty("archetypeType")]
        public long ArchetypeType { get; set; }

        [JsonProperty("joins")]
        public List<object> Joins { get; set; }
    }
}
