using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Security
{
   [Serializable]
    public class BEUsers : CRMAbstract
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string RegisteredClient { get; set; }
        public string UserPassword { get; set; }
        public int RoleID { get; set; }
        public int UserType { get; set; }
        public int SuperUser { get; set; }
        public int Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDT { get; set; }
        public string companylogo { get; set; }
        public string companylogo2 { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string SMTPUserName { get; set; }
        public string SMTPPassword { get; set; }
        public string SMTPHost { get; set; }
        public string SMTPPort { get; set; }
        public List<BEUserAccess> UserAccess { get; set; }
    }
}

