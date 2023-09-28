using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Entity
{
    public  class ContactTypeEntity
    {
        public string code { get; set; }
        public string description { get; set; }
        public List<string> Checkboxes { get; set; }

    }
}
