using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Entity
{
    public class CreditDetailsEntity
    {
        public string RiskScore { get; set; }
        public string CreditScoreRating { get; set; }
        public string CreditLimit { get; set; }
        public string Currency { get; set; }
        public string AMLRisk { get; set; }
        public string FinanceRiskScore { get; set; }

    }
}
