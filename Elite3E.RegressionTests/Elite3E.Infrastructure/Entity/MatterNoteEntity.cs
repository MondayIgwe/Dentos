using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elite3E.Infrastructure.Entity
{
    public class MatterNoteEntity
    {
        public string DateEntered { get; set; }
        public string EntryUser { get; set; }
        public string NoteType { get; set; }
        public string Note { get; set; }
        public string ActionOwner { get; set; }
        public string ActionDate { get; set; }
    }
}
