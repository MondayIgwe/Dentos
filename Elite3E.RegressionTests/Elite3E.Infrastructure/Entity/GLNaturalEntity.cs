using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Entity
{
    public class GLNaturalEntity
    {
        public string GLNatural { get; set; }
        public string Description { get; set; }
        public string ValueType { get; set; }
        public string AccountCategory { get; set; }
        public bool IsControlAccount { get; set; }
        public bool IsAggregate { get; set; }
        public bool IsAutoAdd { get; set; }
        public bool IsGLSecurity { get; set; }        
    }
}
