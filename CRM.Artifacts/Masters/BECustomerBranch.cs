using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Masters
{
    public class BECustomerBranch
    {
        public string CustomerBranchCode { get; set; }
        public string CustomerBranchName { get; set; }
        public int CustomerID { get; set; }
        
        
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public string ContactPerson { get; set; }
        public string PhoneNo { get; set; }
        public string ServiceLocation { get; set; }
        public string Remarks { get; set; }

        public int CustomerBranchID { get; set; }
        public int createdby { get; set; }
        public int modifiedby { get; set; }
    }
}
