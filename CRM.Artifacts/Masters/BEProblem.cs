using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Masters
{
    public class BEProblem
    {
        public string ProblemCode { get; set; }
        public string ProblemName { get; set; }
        public int ProblemID { get; set; }
        public int createdby { get; set; }
        public int modifiedby { get; set; }
    }
}
