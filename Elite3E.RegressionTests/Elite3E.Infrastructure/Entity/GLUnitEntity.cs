using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Entity
{
    public class GLUnitEntity
    {
        public string GLValue { get; set; }
        public string Description { get; set; }
        public string ValueType { get; set; }
        public string Unit { get; set; }
        public bool IsUseLocalAccount { get; set; }
        public string GlLocalChart { get; set; }
        public string GlNatural { get; set; }
        public string DefaultLocalAccount { get; set; }
        public string LocalAccount { get; set; }
        public string LocalAccountDescription { get; set; }
        public string LocalAccountLocalDescription { get; set; }

    }
}
