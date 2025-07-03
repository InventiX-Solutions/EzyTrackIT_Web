using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Masters
{
     [Serializable]
   public class BETicketPartDetails
    {
      
       public int TicketID { get; set; }
     
       public int CreatedBy { get; set; }

       public string OrderNo { get; set; }
       public string old_ref1 { get; set; }
       public string new_ref1 { get; set; }
       public string old_ref2 { get; set; }
      
       public string new_ref2 { get; set; }
       public string remarks { get; set; }
   
      
      
    }
}
