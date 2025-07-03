using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Masters
{
   public class BECompany
    {
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int PostalCode { get; set; }
        public string PhoneNo { get; set; }
        public string FaxNo { get; set; }
        public string Website { get; set; }
        public string EmailID { get; set; }
        public string TINNO { get; set; }
        public string PANNO { get; set; }
        public string SeriveTax { get; set; }
        public string Remarks { get; set; }
        public string companylogo { get; set; }
        public string companylogo2 { get; set; }
        public int CompanyID { get; set; }
        public int createdby { get; set; }
        public int modifiedby { get; set; }
        public string GMT { get; set; }

        public string SMTPPort { get; set; }
        public string SMTPHost { get; set; }
        public string SMTPUserName { get; set; }
        public string SMTPPassword { get; set; }
        public string MailCC { get; set; }
        public string MailBCC { get; set; }
        public string MailSubject { get; set; }
        public string MailContent { get; set; }

        public int EmailFlag { get; set; }
        public int Mobile_Menu_Attendance { get; set; }
        public int Mobile_Menu_AttendanceHistry { get; set; }
        public int Mobile_Menu_NewJob { get; set; }
        public int Mobile_Menu_GetMyJobList { get; set; }
        public int Mobile_Menu_CompletedJob { get; set; }
        public int Mobile_Menu_MoreDetails { get; set; }

        public int Mobile_Report_Send_to_Mail { get; set; }
        public int Mobile_Report_download { get; set; }
       

    }
}
