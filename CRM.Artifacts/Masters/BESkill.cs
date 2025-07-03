using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Masters
{
    public class BESkill
    {
        public string SkillCode { get; set; }
        public string SkillName { get; set; }
        public int SkillID { get; set; }
        public int CompanyID { get; set; }
        public int EngineerID { get; set; }
        public int createdby { get; set; }
        public int modifiedby { get; set; }
    }
}
