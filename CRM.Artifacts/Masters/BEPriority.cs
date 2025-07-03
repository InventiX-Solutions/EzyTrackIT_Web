using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Masters
{
   public class BEPriority
    {
        public string prCode { get; set; }
        public string prName { get; set; }
        public int prID { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
