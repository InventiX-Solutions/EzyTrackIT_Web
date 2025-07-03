using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Masters
{
    public class BEModel
    {
        public string ModelCode { get; set; }
        public string ModelName { get; set; }
        public int ModelID { get; set; }
        public int Brand_ID { get; set; }
        public int createdby { get; set; }
        public int modifiedby { get; set; }
        public int productid { get; set; }
    }
}
