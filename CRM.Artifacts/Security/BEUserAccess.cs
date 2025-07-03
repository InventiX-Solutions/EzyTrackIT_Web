using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Security
{
    [Serializable]
    public class BEUserAccess : CRMAbstract
    {
        public int UserAccessID { get; set; }
        public int UserID { get; set; }
        public int ModuleID { get; set; }
        public int ScreenID { get; set; }
        public int Add { get; set; }
        public int Edit { get; set; }
        public int View { get; set; }
        public int Delete { get; set; }
    }
}
