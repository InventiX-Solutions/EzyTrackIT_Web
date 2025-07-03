using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Masters
{
    public class BECustomer
    {
        public int customer_ID { get; set; }
        
        public int createdby { get; set; }
        public int modifiedby { get; set; }
        public int Active { get; set; }

        public string customer_Code { get; set; }
        public string customer_Name { get; set; }
        public string EmailId { get; set; }
    }
}
