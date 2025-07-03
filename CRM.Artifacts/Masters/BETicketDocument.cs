using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Masters
{
    public class BETicketDocument
    {
        public int TicketID { get; set; }
        public string TicketNo { get; set; }
        public int tickets_doc_id { get; set; }
        public string DocumentName { get; set; }
        public string remarks { get; set; }
        public string DocumentPath { get; set; }
        public string DocfullName { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDt { get; set; }
    }
}
