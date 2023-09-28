using Elite3E.RestServices.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.RestServices.Entity
{
    public class ApiSectionEntity
    {
        public string SectionId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string GLSection { get; set; }
        public ValueUnion GlSectionValue { get; set; }

    }
}
