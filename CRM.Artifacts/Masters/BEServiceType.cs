using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Masters
{
    public class BEServiceType
    {
        public string ServiceTypeCode { get; set; }
        public string ServiceTypeName { get; set; }
        public int ServiceTypeID { get; set; }
        public int createdby { get; set; }
        public int modifiedby { get; set; }
    }
}
