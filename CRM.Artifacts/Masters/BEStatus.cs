using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Masters
{
    public class BEStatus
    {
        public string StatusCode { get; set; }
        public string StatusName { get; set; }
        public int StatusID { get; set; }
        public string SequenceNo { get; set; }
        public string StatusImage { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
