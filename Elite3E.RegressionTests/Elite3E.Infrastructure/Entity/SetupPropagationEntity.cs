using System.Collections.Generic;

namespace Elite3E.Infrastructure.Entity
{
    public class SetupPropagationEntity
    {
        public string Process { get; set; }
        public string Instance { get; set; }
        public string ControlSource { get; set; }
        public string Role { get; set; }
        public bool? ExcludeList { get; set; }
        public bool? IncludeList { get; set; }
        public Operation Action { get; set; }
        public List<string> InstanceList { get; set; }

    }

    public enum Operation
    {
        Create, 
        Update,
        Delete
    }
}


