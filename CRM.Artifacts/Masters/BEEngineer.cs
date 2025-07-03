using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Masters
{
   public class BEEngineer
    {
       public string EngineerCode { get; set; }
       public string EngineerName { get; set; }
       public int SkillID { get; set; }
      
       public int EngineerID { get; set; }
       public string MobileNo { get; set; }
       public string EmailID { get; set; }
       public int createdby { get; set; }
       public int modifiedby { get; set; }
       public String password { get; set; }
       public String EngineerTypeText { get; set; }
      
       public int vehicleID { get; set; }
       public string EngineerType { get; set; }
    }
}
