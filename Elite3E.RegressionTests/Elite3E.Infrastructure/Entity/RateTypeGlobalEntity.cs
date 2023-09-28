using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Entity
{
    public class RateTypeGlobalEntity
    {
        public string RateType { get; set; }
        public string BasedOnDate { get; set; }
        public string EffectiveDate { get; set; }
        public string Reasontype { get; set; }
        public string DefaultRate { get; set; }
        public string DefaultCurrency { get; set; }
        public string RoundingMethod { get; set; }
        public string ChangeAmount { get; set; }
    }
}
