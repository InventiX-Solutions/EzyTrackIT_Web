using CRM.Artifacts.Masters;
using CRM.Bussiness;
using CRM.Bussiness.Masters;
using System;
using System.Data;
using System.Net.Mail;
using TrackIT.ClassModules;

namespace TCC_CRM.ClassModules
{
    public static class HangfireHelper
    {
        public static BLLCommon m_BLLCommon = new BLLCommon();

        public static void SendMorningAttendanceStatus()
        {
            Common.LogInfo(DateTime.Now, "SendMorningAttendanceStatus Method Initiation");

            DataTable result = GetAttendanceData();

            string htmlString = string.Empty;

            if (result.Rows.Count > 0)
            {
                DataView dataView = new DataView(result);
                ///string[] selectedColumns = new[] { "engineer_code", "engineer_name", "PunchDate", "EMPStatus", "TimeSpent", "Intime", "INLocation", "outtime", "OutLocation" };
                string[] selectedColumns = new[] { "engineer_name", "Intime", "outtime", "TimeSpent", "EMPStatus" };

                DataTable resultDataTable = dataView.ToTable(false, selectedColumns);
                //DataView dvs = resultDataTable.DefaultView;

                htmlString += GetDataSet(resultDataTable, "M");

                string recipient = Common.EmailTo;

                SendEmail(htmlString, Common.EmailTo, Common.EmailCC, Common.EmailBCC, DateTime.Now.ToString("dd/MM/yyyy"), string.Empty, String.Empty);

                Common.LogInfo(DateTime.Now, "SendMorningAttendanceStatus Method Execution completed");
            }
        }

        public static void SendAttendanceStatus()
        {
            Common.LogInfo(DateTime.Now, "SendAttendanceStatus Method Initiation");

            DataTable result = GetAttendanceData();

            string htmlString = string.Empty;

            if (result.Rows.Count > 0)
            {
                DataView dataView = new DataView(result);
                ///string[] selectedColumns = new[] { "engineer_code", "engineer_name", "PunchDate", "EMPStatus", "TimeSpent", "Intime", "INLocation", "outtime", "OutLocation" };
                string[] selectedColumns = new[] { "engineer_name", "Intime", "outtime", "TimeSpent", "EMPStatus" };

                DataTable resultDataTable = dataView.ToTable(false, selectedColumns);
                //DataView dvs = resultDataTable.DefaultView;

                htmlString += GetDataSet(resultDataTable, "E");

                string recipient = Common.EmailTo;

                SendEmail(htmlString, Common.EmailTo, Common.EmailCC, Common.EmailBCC, DateTime.Now.ToShortDateString(), string.Empty, string.Empty);

                Common.LogInfo(DateTime.Now, "SendAttendanceStatus Method Execution completed");
            }
        }

        private static DataTable GetAttendanceData()
        {
            Common.LogInfo(DateTime.Now, "GetAttendanceData Method Initiation");
            DataTable result = new DataTable();
            //string startDate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            //string endDate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");

            ////string startDate = Convert.ToDateTime("13-09-2021").ToString("yyyy-MM-dd");
            ////string endDate = Convert.ToDateTime("13-09-2021").ToString("yyyy-MM-dd");

            //result = m_BLLCommon.GetAttendanceList(startDate, endDate, string.Empty, "1erp_trackit");

            //Common.LogInfo(DateTime.Now, "GetAttendanceData Method Execution completed" + result.Rows.Count);

            return result;
        }

        private static string GetDataSet(DataTable dtResult, string sMailDuration)
        {
            string messageBody = string.Empty;

            try
            {
                if (dtResult.Rows.Count == 0)
                    return messageBody;

                var resultAbsent = dtResult.Select("EMPStatus = 'Not Checked'");

                string htmlTotal = "Total Employee : " + dtResult.Rows.Count.ToString();
                string htmlAbsent = "Absent : " + dtResult.Select("EMPStatus = 'Not Checked'").Length;
                string htmlInvalid = string.Empty;
                string htmlValid = string.Empty;

                if (sMailDuration == "M")
                {
                    htmlValid = "Total Present : " + dtResult.Select("EMPStatus = 'Checked IN'").Length;
                }
                else if (sMailDuration == "E")
                {
                    htmlInvalid = "Single Punch : " + dtResult.Select("EMPStatus = 'Checked IN'").Length;
                    htmlValid = "Total Present : " + dtResult.Select("EMPStatus = 'Checked OUT'").Length;
                }

                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:left; font-size:14px;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style =\"background-color:#6FA1D2; color:#ffffff;  \">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style =\"color:#555555;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";
                //string htmlFooterTdStart = "<td style=\" border-color:#5c87b2; border-style:solid;  border-width:thin; padding: 5px;font-weight: bold;\">";
                //string htmlFooterTdEnd = "</td>";

                messageBody += "<br><span style=\"font-size:16px;\">" + htmlTotal + "</span></br>";
                messageBody += "<br><span style=\"font-size:16px;\">" + htmlValid + "</span></br>";

                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;

                dtResult.Columns["engineer_name"].ColumnName = "Employee Name";
                dtResult.Columns["Intime"].ColumnName = "In Time";
                dtResult.Columns["outtime"].ColumnName = "Out Time";
                dtResult.Columns["TimeSpent"].ColumnName = "Hours Worked";

                for (int i = 0; i < dtResult.Columns.Count - 1; i++)
                {
                    messageBody += htmlTdStart;
                    messageBody += dtResult.Columns[i].ColumnName;
                    messageBody += htmlTdEnd;
                }
                messageBody += htmlHeaderRowEnd;

                if (sMailDuration == "E")
                {
                    DataRow[] dataRow = dtResult.Select("EMPStatus = 'Checked OUT'");

                    for (int j = 0; j < dataRow.Length; j++)
                    {
                        messageBody += htmlTrStart;
                        for (int i = 0; i < dtResult.Columns.Count - 1; i++)
                        {
                            messageBody += htmlTdStart;
                            messageBody += dataRow[j][i].ToString();
                            messageBody += htmlTdEnd;
                        }
                        messageBody += htmlTrEnd;
                    }
                    messageBody = messageBody + htmlTableEnd;

                    messageBody += "<br><span style=\"font-size:16px;\">" + htmlInvalid + "</span></br>";
                    messageBody += htmlTableStart;
                    messageBody += htmlHeaderRowStart;

                    for (int i = 0; i < dtResult.Columns.Count - 1; i++)
                    {
                        messageBody += htmlTdStart;
                        messageBody += dtResult.Columns[i].ColumnName;
                        messageBody += htmlTdEnd;
                    }
                    messageBody += htmlHeaderRowEnd;
                }

                DataRow[] inValidDataRow = dtResult.Select("EMPStatus = 'Checked IN'");

                for (int j = 0; j < inValidDataRow.Length; j++)
                {
                    messageBody += htmlTrStart;
                    for (int i = 0; i < dtResult.Columns.Count - 1; i++)
                    {
                        messageBody += htmlTdStart;
                        messageBody += inValidDataRow[j][i].ToString();
                        messageBody += htmlTdEnd;
                    }
                    messageBody += htmlTrEnd;
                }
                messageBody = messageBody + htmlTableEnd;

                messageBody += "<br><span style=\"font-size:16px;\">" + htmlAbsent + "</span></br>";
                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;

                for (int i = 0; i < dtResult.Columns.Count - 1; i++)
                {
                    messageBody += htmlTdStart;
                    messageBody += dtResult.Columns[i].ColumnName;
                    messageBody += htmlTdEnd;
                }
                messageBody += htmlHeaderRowEnd;

                DataRow[] absentDataRow = dtResult.Select("EMPStatus = 'Not Checked'");

                for (int j = 0; j < absentDataRow.Length; j++)
                {
                    messageBody += htmlTrStart;
                    for (int i = 0; i < dtResult.Columns.Count - 1; i++)
                    {
                        messageBody += htmlTdStart;
                        messageBody += absentDataRow[j][i].ToString();
                        messageBody += htmlTdEnd;
                    }
                    messageBody += htmlTrEnd;
                }
                messageBody = messageBody + htmlTableEnd;

                return messageBody;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static DataTable GetOutstandingJobListData()
        {
            Common.LogInfo(DateTime.Now, "GetOutstandingJobListData Method Initiation");
            DataTable result = new DataTable();
            string startDate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string endDate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");

            //string startDate = Convert.ToDateTime("13-09-2021").ToString("yyyy-MM-dd");
            //string endDate = Convert.ToDateTime("13-09-2021").ToString("yyyy-MM-dd");

            result = m_BLLCommon.GetOutstandingJobListData(1, "stss_trackit");      //"stss_trackit"

            Common.LogInfo(DateTime.Now, "GetOutstandingJobListData Method Execution completed" + result.Rows.Count);

            return result;
        }

        public static void SendSecurtexJobReminderEmail()
        {
            Common.LogInfo(DateTime.Now, "SendSecurtexJobReminderEmail Method Initiation");

            DataTable dtNotificationDetails = m_BLLCommon.GetNotificationTemplateDetails(3, "stss_trackit");

            if (dtNotificationDetails != null)
            {
                if (dtNotificationDetails.Rows.Count > 0)
                {
                    DataRow drTemplate = dtNotificationDetails.Rows[0];

                    DataTable result = GetOutstandingJobListData();

                    string htmlString = string.Empty;

                    if (result.Rows.Count > 0)
                    {
                        DataView dataView = new DataView(result);
                        string[] selectedColumns = new[] { "TicketNo", "tktdate", "customer_Name", "customer_branch_Name", "CurrentStatus", "engineer_name", "NameOfCaller", "Call_Detail_Nature" };

                        DataTable resultDataTable = dataView.ToTable(false, selectedColumns);

                        htmlString += GetJobDataSet(resultDataTable);

                        string recipient = drTemplate["MailCC"].ToString();

                        SendEmail(htmlString, recipient, string.Empty, "velu.d@inventixglobal.com", DateTime.Now.ToShortDateString(), drTemplate["MailSubject"].ToString(), drTemplate["MailContent"].ToString());

                        Common.LogInfo(DateTime.Now, "SendSecurtexJobReminderEmail Method Execution completed");
                    }
                }
            }
        }

        private static string GetJobDataSet(DataTable dtResult)
        {
            string messageBody = string.Empty;

            try
            {
                if (dtResult.Rows.Count == 0)
                    return messageBody;

                string htmlTotal = "Open Jobs " + dtResult.Rows.Count.ToString();
                string htmlInvalid = string.Empty;
                string htmlValid = string.Empty;

                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:left; font-size:14px;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style =\"background-color:#6FA1D2; color:#ffffff;  \">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style =\"color:#555555;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";

                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;

                dtResult.Columns["TicketNo"].ColumnName = "Job No";
                dtResult.Columns["tktdate"].ColumnName = "Job Date";
                dtResult.Columns["customer_Name"].ColumnName = "Customer";
                dtResult.Columns["customer_branch_Name"].ColumnName = "Customer Branch";
                dtResult.Columns["CurrentStatus"].ColumnName = "Status";
                dtResult.Columns["engineer_name"].ColumnName = "Engineer";
                dtResult.Columns["NameOfCaller"].ColumnName = "Caller Name";
                dtResult.Columns["Call_Detail_Nature"].ColumnName = "Call Details";

                for (int i = 0; i < dtResult.Columns.Count; i++)
                {
                    messageBody += htmlTdStart;
                    messageBody += dtResult.Columns[i].ColumnName;
                    messageBody += htmlTdEnd;
                }
                messageBody += htmlHeaderRowEnd;

                for (int j = 0; j < dtResult.Rows.Count - 1; j++)
                {
                    messageBody += htmlTrStart;
                    for (int i = 0; i < dtResult.Columns.Count; i++)
                    {
                        messageBody += htmlTdStart;
                        messageBody += dtResult.Rows[j][i].ToString();
                        messageBody += htmlTdEnd;
                    }
                    messageBody += htmlTrEnd;
                }
                messageBody = messageBody + htmlTableEnd;

               // messageBody += "<br><span style=\"font-size:16px;\">" + htmlInvalid + "</span></br>";
               // messageBody += htmlTableStart;
               // messageBody += htmlHeaderRowStart;

               // for (int i = 0; i < dtResult.Columns.Count - 1; i++)
               // {
               //     messageBody += htmlTdStart;
               //     messageBody += dtResult.Columns[i].ColumnName;
               //     messageBody += htmlTdEnd;
               // }
               //// messageBody += htmlHeaderRowEnd;

               // //messageBody = messageBody;

                return messageBody;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static void SendEmail(string htmlString, string recipientTo, string recipientCC, string recipientBCC, string attendanceDate, string subject, string content)
        {
            Common.LogInfo(DateTime.Now, "SendEmail Method Initialized");
            BECompany m_BECompany = new BECompany();
            BLLCompany bllCompany = new BLLCompany();

            //m_BECompany = bllCompany.GetCompany(1, "pgs_trackit");
            m_BECompany = bllCompany.GetCompany(1, "stss_trackit");

            if (m_BECompany != null)
            {
                Common.HangfireSMTPUserName = m_BECompany.SMTPUserName;
                Common.HangfireSMTPPassword = m_BECompany.SMTPPassword;
                Common.HangfireSMTPHost = m_BECompany.SMTPHost;  //"smtp.gmail.com", // smtp server address here…
                Common.HangfireSMTPPort = Convert.ToInt32(m_BECompany.SMTPPort);
            }

            string FromEmail = Common.HangfireSMTPUserName; //"EzyVisit.Alerts@gmail.com";
            string Password = Common.HangfireSMTPPassword;  //"PGSEzyVisit@123";
            string result = string.Empty;
            DateTime dtReportFetchDate = DateTime.Now;

            try
            {
                result = "Message Sent Successfully..!!";
                MailMessage message = new MailMessage();
                message.From = new MailAddress(FromEmail);
                message.IsBodyHtml = true;
                message.Body = htmlString;

                //message.Subject = "Attendance Status - " + attendanceDate;
                message.Subject = subject;

                string[] MultiRecipientTo = recipientTo.Split(',');
                foreach (string MultiEmail in MultiRecipientTo)
                {
                    if (!string.IsNullOrEmpty(MultiEmail))
                        message.To.Add(new MailAddress(MultiEmail));
                }

                string[] MultiRecipientCC = recipientCC.Split(',');
                foreach (string MultiEmail in MultiRecipientCC)
                {
                    if (!string.IsNullOrEmpty(MultiEmail))
                        message.CC.Add(new MailAddress(MultiEmail));
                }

                string[] MultiRecipientBCC = recipientBCC.Split(',');
                foreach (string MultiEmail in MultiRecipientBCC)
                {
                    if (!string.IsNullOrEmpty(MultiEmail))
                        message.Bcc.Add(new MailAddress(MultiEmail));
                }

                SmtpClient smtp = new SmtpClient
                {
                    Host = Common.HangfireSMTPHost,  //"smtp.gmail.com", // smtp server address here…
                    Port = Common.HangfireSMTPPort, //587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(message.From.Address, Password),
                };
                smtp.Send(message);
            }
            catch (Exception e)
            {
                Common.LogInfo(DateTime.Now, "SendEmail Method Error - " + e.Message);
            }
        }
    }
}