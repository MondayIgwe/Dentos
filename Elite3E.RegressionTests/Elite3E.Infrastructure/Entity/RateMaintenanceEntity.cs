using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Entity
{
    public class RateMaintenanceEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string StartDate { get; set; }
        public string ReasonType { get; set; }
        public string RateType { get; set; }
        public string RateTypeValue { get; set; }
        public string Formula { get; set; }


    }
}
