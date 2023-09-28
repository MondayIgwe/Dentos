using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Entity
{
    public class MyBillableMattersEntity
    {
        public string FeeEarner { get; set; }
        public bool? WIPExcludeZeroCheckbox { get; set; } = null;
        public bool? ARExcludeZeroCheckbox { get; set; } = null;
        public bool? ClientAccountExcludeZeroCheckbox { get; set; } = null;
    }
}
