using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Masters
{
     [Serializable]
    public class BETicketStatus
    {
      
       public int TicketID { get; set; }
       public int status_id { get; set; }
       public string Currentstatus { get; set; } 
       public string NewStatus { get; set; }
       public string remarks { get; set; }
       public int Assignedto { get; set; }
       public string Assignedtoname { get; set; }
       public string ClaimAmount { get; set; }
       public string starttime { get; set; }
       public string endtime { get; set; }
       public Decimal tothrs { get; set; }
       public int ModifiedBy { get; set; }

      
    
      
    }
}
