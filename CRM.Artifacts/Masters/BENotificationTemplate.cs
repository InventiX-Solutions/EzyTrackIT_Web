using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Masters
{
    public class BENotificationTemplate
    {
        public int Template_ID { get; set; }
        public int TemplateType_ID { get; set; }
        public string TemplateType { get; set; }
        public int EmailFlag { get; set; }
        public int SMSFlag { get; set; }
        public int WhatsappFlag { get; set; }
        public string MailSubject { get; set; }
        public string MailContent { get; set; }
        public string MailCC { get; set; }
        public string SMSSubject { get; set; }
        public string SMSContent { get; set; }
        public string WhatsappContent { get; set; }
        public string TemplateName { get; set; }
        public string Category_ID { get; set; }
        public string Category_ID_Text { get; set; }
        //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
        public string MailRecipientType { get; set; }
        public string SMSRecipientType { get; set; }
        public string WhatsappRecipientType { get; set; }

        public int Company_ID { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string LogoPath { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDT { get; set; }
        public byte Active { get; set; }


        public Int32 PageIndex { get; set; }
        public Int32 PageSize { get; set; }

        public string SortBy { get; set; }
        public string SortOrder { get; set; }

        public string Remarks { get; set; }
        public DataSet dsResult { get; set; }

        public int SortOrderPosition { get; set; }
        public string SortOrderDirection { get; set; }

    }
}
