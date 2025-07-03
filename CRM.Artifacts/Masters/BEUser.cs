using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Masters
{
   public class BEUser
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        public string LoginName { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public int usertype { get; set; }

        public int UserID { get; set; }
        public int createdby { get; set; }
        public int modifiedby { get; set; }
    }
}
