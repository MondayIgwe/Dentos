using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Entity
{
    public class OfficeConfigurationEntity
    {
        public string TrustDisbursementType { get; set; }
        public string DaysToDispatch { get; set; }
        public string TimeKeeperLeaver { get; set; }
        public string ClientAccountReceiptType { get; set; }
        public string ClientAccountDefault { get; set; }

        public string Language { get; set; }

        public string MatterAttribute { get; set; }

        public string CoverLetterNarrative { get; set; }

        public string InvoiceNarrative { get; set; }

        public string LegalName { get; set; }
        public string Office { get; set; }
        public string GovtSysTemplate { get; set; }

    }
}
