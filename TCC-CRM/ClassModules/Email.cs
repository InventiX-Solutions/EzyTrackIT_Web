using CRM.Bussiness;
using CRM.Bussiness.Masters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.UI.WebControls;

namespace TrackIT.ClassModules
{
    public class Email
    {
        public Common cm = new Common();

        //public static int SendEmail(string ccMail, DataTable dtRecipients, DataTable dtNotificationDetails, string bccMail, AlternateView HtmlDesign, EmailValues mEmailValues, string TempID, string category, string Mode)
        //{
        //    BLLAllNotificationAlert mBLLAllNotificationAlert = new BLLAllNotificationAlert();
        //    //   int mEmailCount = mBLLAllNotificationAlert.GetMonthNotificationCountByType("E", SessionMgr.CompanyID, SessionMgr.ClientCode);
        //    string fileName = string.Empty;

        //    try
        //    {
        //        string SMTPUserName = SessionMgr.SMTPUserName;
        //        string SMTPPassword = SessionMgr.SMTPPassword;
        //        string SMTPPort = SessionMgr.SMTPPort;
        //        string SMTPHost = SessionMgr.SMTPHost;

        //        DataRow drTemplate = dtNotificationDetails.Rows[0];
        //        //EmailDetailsSub dEmailDetailsSub = new EmailDetailsSub();
        //        //dEmailDetailsSub = details(TempID, category);

        //        if (!String.IsNullOrEmpty(drTemplate["MailSubject"].ToString()) && !String.IsNullOrEmpty(drTemplate["MailContent"].ToString()))
        //        {
        //            List<string> ccEmail = ccMail.Split(',').ToList();
        //            List<string> bccEMail = bccMail.Split(',').ToList();

        //            MailMessage objMessage = new MailMessage();
        //            SmtpClient smtp = new SmtpClient();
        //            objMessage.From = new MailAddress(SMTPUserName);
        //            if (ccMail.Trim() != string.Empty)
        //            {
        //                foreach (string i in ccEmail)
        //                {
        //                    objMessage.CC.Add(new MailAddress(i));
        //                }
        //            }
        //            if (bccMail.Trim() != string.Empty)
        //            {
        //                foreach (string i in bccEMail)
        //                {
        //                    objMessage.Bcc.Add(new MailAddress(i));
        //                }
        //            }
        //            if (dtRecipients != null)
        //            {
        //                foreach (DataRow email in dtRecipients.Rows)
        //                {
        //                    objMessage.To.Add(new MailAddress(email["emailid"].ToString()));
        //                }
        //            }
        //            objMessage.Subject = replacements(mEmailValues, drTemplate["MailSubject"].ToString().Trim());

        //            if (HtmlDesign != null)
        //            {
        //                objMessage.IsBodyHtml = true; //to make message body as html  
        //                objMessage.AlternateViews.Add(HtmlDesign);
        //            }
        //            else
        //            {
        //                objMessage.IsBodyHtml = true;
        //                objMessage.Body = replacements(mEmailValues, drTemplate["MailContent"].ToString().Trim());
        //            }

        //            if (SMTPPort.Trim() == string.Empty)
        //                smtp.Port = 587;
        //            else
        //                smtp.Port = int.Parse(SMTPPort);
        //            if (SMTPHost.Trim() == string.Empty)
        //                smtp.Host = "smtp.gmail.com"; //for gmail host  
        //            else
        //                smtp.Host = SMTPHost.Trim(); //for gmail host  
        //                                             //commend and add below code khan - start
        //            if (smtp.Host.ToUpper().Contains("GMAIL"))
        //            {
        //                smtp.EnableSsl = true;
        //            }//commend and add below code khan - end

        //            smtp.UseDefaultCredentials = true;

        //            smtp.Credentials = new NetworkCredential(SMTPUserName, SMTPPassword);
        //            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //            smtp.Send(objMessage);

        //            //   mBLLAllNotificationAlert.Notification_Update(toMail + "," + ccMail + "," + bccMail, TempID, "E", "S", SessionMgr.CompanyID, SessionMgr.UserID, ((SessionMgr.LoginMode == "Employee") ? "E" : "U"), "Success", SessionMgr.ClientCode);
        //        }
        //        else
        //        {
        //            Common.LogInfo(DateTime.Now, "Email Subject or Content is empty - " + SessionMgr.DBName);
        //            //  mBLLAllNotificationAlert.Notification_Update(toMail + "," + ccMail + "," + bccMail, TempID, "E", "P", SessionMgr.CompanyID, SessionMgr.UserID, "No smtp Details", SessionMgr.ClientCode);
        //            return -1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogInfo(DateTime.Now, ex.Message.ToString() + " - " + SessionMgr.DBName);
        //        //    mBLLAllNotificationAlert.Notification_Update(toMail + "," + ccMail + "," + bccMail, TempID, "E", "F", SessionMgr.CompanyID, SessionMgr.UserID, ((SessionMgr.LoginMode == "Employee") ? "E" : "U"), ex.Message.ToString(), SessionMgr.ClientCode);
        //        return -1;
        //    }
        //    return 1;
        //}

        //public static int SendEmail(string ccMail, DataTable dtRecipients, DataTable dtNotificationDetails, string bccMail, AlternateView HtmlDesign, EmailValues mEmailValues, string TempID, string category, string Mode)
        //{
        //    BLLAllNotificationAlert mBLLAllNotificationAlert = new BLLAllNotificationAlert();
        //    string fileName = string.Empty;

        //    try
        //    {
        //        string SMTPUserName = SessionMgr.SMTPUserName;
        //        string SMTPPassword = SessionMgr.SMTPPassword;
        //        string SMTPPort = SessionMgr.SMTPPort;
        //        string SMTPHost = SessionMgr.SMTPHost;

        //        DataRow drTemplate = dtNotificationDetails.Rows[0];

        //        if (!String.IsNullOrEmpty(drTemplate["MailSubject"].ToString()) && !String.IsNullOrEmpty(drTemplate["MailContent"].ToString()))
        //        {
        //            List<string> ccEmail = ccMail.Split(',').ToList();
        //            List<string> bccEMail = bccMail.Split(',').ToList();

        //            MailMessage objMessage = new MailMessage
        //            {
        //                From = new MailAddress(SMTPUserName)
        //            };

        //            if (ccMail.Trim() != string.Empty)
        //            {
        //                foreach (string i in ccEmail)
        //                {
        //                    objMessage.CC.Add(new MailAddress(i));
        //                }
        //            }
        //            if (bccMail.Trim() != string.Empty)
        //            {
        //                foreach (string i in bccEMail)
        //                {
        //                    objMessage.Bcc.Add(new MailAddress(i));
        //                }
        //            }
        //            if (dtRecipients != null)
        //            {
        //                foreach (DataRow email in dtRecipients.Rows)
        //                {
        //                    objMessage.To.Add(new MailAddress(email["emailid"].ToString()));
        //                }
        //            }
        //            objMessage.Subject = replacements(mEmailValues, drTemplate["MailSubject"].ToString().Trim());

        //            if (HtmlDesign != null)
        //            {
        //                objMessage.IsBodyHtml = true;
        //                objMessage.AlternateViews.Add(HtmlDesign);
        //            }
        //            else
        //            {
        //                objMessage.IsBodyHtml = true;
        //                objMessage.Body = replacements(mEmailValues, drTemplate["MailContent"].ToString().Trim());
        //            }

        //            SmtpClient smtp = new SmtpClient
        //            {
        //                Port = !string.IsNullOrEmpty(SMTPPort) ? int.Parse(SMTPPort) : 587,
        //                Host = !string.IsNullOrEmpty(SMTPHost) ? SMTPHost.Trim() : "smtp.gmail.com",
        //                EnableSsl = true, // SSL is required by most email providers
        //                UseDefaultCredentials = false,
        //                Credentials = new NetworkCredential(SMTPUserName, SMTPPassword),
        //                DeliveryMethod = SmtpDeliveryMethod.Network
        //            };

        //            smtp.Send(objMessage);
        //        }
        //        else
        //        {
        //            Common.LogInfo(DateTime.Now, "Email Subject or Content is empty - " + SessionMgr.DBName);
        //            return -1;
        //        }
        //    }
        //    catch (SmtpException smtpEx)
        //    {
        //        Common.LogInfo(DateTime.Now, $"SMTP Error: {smtpEx.Message} - " + SessionMgr.DBName);
        //        return -1;
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogInfo(DateTime.Now, $"General Error: {ex.Message} - " + SessionMgr.DBName);
        //        return -1;
        //    }
        //    return 1;
        //}

        public static int SendEmail(string ccMail, DataTable dtRecipients, DataTable dtNotificationDetails, string bccMail, AlternateView HtmlDesign, EmailValues mEmailValues, string TempID, string category, string Mode)
        {
            try
            {
                string SMTPUserName = SessionMgr.SMTPUserName;
                string SMTPPassword = SessionMgr.SMTPPassword;
                string SMTPPort = SessionMgr.SMTPPort;
                string SMTPHost = SessionMgr.SMTPHost;

                DataRow drTemplate = dtNotificationDetails.Rows[0];

                if (!String.IsNullOrEmpty(drTemplate["MailSubject"].ToString()) && !String.IsNullOrEmpty(drTemplate["MailContent"].ToString()))
                {
                    MailMessage objMessage = new MailMessage
                    {
                        From = new MailAddress(SMTPUserName),
                        Subject = replacements(mEmailValues, drTemplate["MailSubject"].ToString().Trim()),
                        IsBodyHtml = true
                    };

                    // Add CC and BCC emails
                    if (!string.IsNullOrEmpty(ccMail))
                    {
                        foreach (var cc in ccMail.Split(','))
                            objMessage.CC.Add(cc.Trim());
                    }
                    if (!string.IsNullOrEmpty(bccMail))
                    {
                        foreach (var bcc in bccMail.Split(','))
                            objMessage.Bcc.Add(bcc.Trim());
                    }
                    // Add recipient emails
                    if (dtRecipients != null)
                    {
                        foreach (DataRow email in dtRecipients.Rows)
                            objMessage.To.Add(new MailAddress(email["emailid"].ToString()));
                    }

                    // Add Body content
                    if (HtmlDesign != null)
                        objMessage.AlternateViews.Add(HtmlDesign);
                    else
                        objMessage.Body = replacements(mEmailValues, drTemplate["MailContent"].ToString().Trim());

                    // Configure SMTP client
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = !string.IsNullOrEmpty(SMTPHost) ? SMTPHost : "smtp.gmail.com",
                        Port = !string.IsNullOrEmpty(SMTPPort) ? int.Parse(SMTPPort) : 587,
                        EnableSsl = true,
                        UseDefaultCredentials = false,  // Important for explicit authentication
                        Credentials = new NetworkCredential(SMTPUserName, SMTPPassword),
                        DeliveryMethod = SmtpDeliveryMethod.Network
                    };

                    smtp.Send(objMessage);
                }
                else
                {
                    Common.LogInfo(DateTime.Now, "Email Subject or Content is empty - " + SessionMgr.DBName);
                    return -1;
                }
            }
            catch (SmtpException smtpEx)
            {
              
                Common.LogInfo(DateTime.Now, "SMTP Error: " + smtpEx.Message + " - " + SessionMgr.DBName);
              //  Common.LogInfo(DateTime.Now, $"SMTP Error: {smtpEx.Message} - " + SessionMgr.DBName);
                return -1;
            }
            catch (Exception ex)
            {
               // Common.LogInfo(DateTime.Now, $"General Error: {ex.Message} - " + SessionMgr.DBName);
                Common.LogInfo(DateTime.Now, "Error: " + ex.Message + " - " + SessionMgr.DBName);
                return -1;
            }
            return 1;
        }


        public static string replacements(EmailValues mEmailValues, string bodys)
        {
            string body = bodys;

            //string body = "Job Number: {JobNo}";


            body = body.Replace("{JobNo}", mEmailValues.JobNo); // Replacing JobNo
            body = body.Replace("{Customer}", mEmailValues.Customer); // Replacing Customer
            body = body.Replace("{JobDate}", mEmailValues.JobDate); // Replacing Job Date
            body = body.Replace("{JobType}", mEmailValues.JobType); // Replacing Job Type
            body = body.Replace("{Problem}", mEmailValues.Problem); // Replacing Problem
            body = body.Replace("{Product}", mEmailValues.Product); // Replacing Product
            body = body.Replace("{Brand}", mEmailValues.Brand); // Replacing Brand
            body = body.Replace("{Model}", mEmailValues.Model); // Replacing Model
            body = body.Replace("{Engineer}", mEmailValues.Engineer); // Replacing Engineer
            body = body.Replace("{CurrentStatus}", mEmailValues.CurrentStatus); // Replacing CurrentStatus
            body = body.Replace("{ServiceType}", mEmailValues.ServiceType); // Replacing ServiceType
            body = body.Replace("{PartNo}", mEmailValues.PartNo); // Replacing PartNo

            body = body.Replace("{Branch}", mEmailValues.Branch); // Replacing Customer Branch
            body = body.Replace("{CallDetails}", mEmailValues.CallDetails); // Replacing Call Details
            body = body.Replace("{Remarks}", mEmailValues.Remarks); // Replacing Remarks

            return body;
        }

        public static void SendEmailNotification(int ticketid, string jobType)  //Job Type -> N => Email has to notified to the Supervisors, E => Email has to notified to the Supervisors as well Engineers
        {
            BLLCommon bllCommon = new BLLCommon();
            BLLTicket bllTicket = new BLLTicket();
            DataTable dtRecipients = new DataTable();

            int notificationID = 0;

            if (jobType == "N")
            {
                dtRecipients = bllCommon.GetSupervisorEmailIDs(SessionMgr.DBName);
                notificationID = 1;
            }
            else if (jobType == "E")
            {
                dtRecipients = bllCommon.GetJobEngineesEmailIDs(ticketid, SessionMgr.DBName);
                notificationID = 2;
            }
            else if (jobType == "C")
            {
                dtRecipients = bllCommon.GetSupervisorEmailIDs(SessionMgr.DBName);
                notificationID = 4;
            }

            if (dtRecipients != null)
            {
                if (dtRecipients.Rows.Count > 0)
                {
                    Common.LogInfo(DateTime.Now, "Email - dtRecipients done - " + SessionMgr.DBName);

                    DataTable dtResult = bllTicket.GetticketemailDetails(ticketid, SessionMgr.DBName);
                    DataTable dtNotificationDetails = bllCommon.GetNotificationTemplateDetails(notificationID, SessionMgr.DBName); //Get RecipientType from notificationtemplate

                    if (dtResult != null && dtNotificationDetails != null)
                    {
                        Common.LogInfo(DateTime.Now, "Email - ticket details & notification details loop - " + SessionMgr.DBName);
                        if (dtResult.Rows.Count > 0 && dtNotificationDetails.Rows.Count > 0)
                        {
                            Common.LogInfo(DateTime.Now, "Email - ticket details & notification details inner loop - " + SessionMgr.DBName);
                            DataRow dr = dtResult.Rows[0];

                            Email.EmailValues mEmailValues = new Email.EmailValues();
                            mEmailValues.JobNo = Common.NullToString(dr["JobNo"]);
                            mEmailValues.Customer = Common.NullToString(dr["Customer"]);
                            mEmailValues.JobDate = Common.NullToString(dr["JobDate"]);
                            mEmailValues.JobType = Common.NullToString(dr["JobType"]);
                            mEmailValues.Problem = Common.NullToString(dr["Problem"]);
                            mEmailValues.Product = Common.NullToString(dr["Product"]);
                            mEmailValues.Brand = Common.NullToString(dr["Brand"]);
                            mEmailValues.Engineer = Common.NullToString(dr["Engineername"]) + " - " + Common.NullToString(dr["Engineer"]);
                            mEmailValues.CurrentStatus = Common.NullToString(dr["CurrentStatus"]);
                            mEmailValues.ServiceType = Common.NullToString(dr["ServiceType"]);
                            mEmailValues.PartNo = Common.NullToString(dr["PartNo"]);
                            mEmailValues.Model = Common.NullToString(dr["Model"]);
                            mEmailValues.Branch = Common.NullToString(dr["customer_branch_Name"]);
                            mEmailValues.CallDetails = Common.NullToString(dr["Call_Detail_Nature"]);
                            mEmailValues.Remarks = Common.NullToString(dr["Remarks"]);

                            //SendEMailNotification("1", mEmailValues, string.Empty, "support@peritusglobal.com", "support@peritusglobal.com", string.Empty, string.Empty, "Jobcreation", 0, ticketid);

                            DataRow drNotify = dtNotificationDetails.Rows[0];

                            SendEmail(drNotify["MailCC"].ToString(), dtRecipients, dtNotificationDetails, string.Empty, null, mEmailValues, notificationID.ToString(), string.Empty, string.Empty);
                        }
                        else
                            Common.LogInfo(DateTime.Now, "Email - ticket details & notification either one has no value exit loop - " + SessionMgr.DBName);
                    }
                }
                Common.LogInfo(DateTime.Now, "Email - dtRecipients details has no value exit loop - " + SessionMgr.DBName);
            }
        }

        public static List<KeyValuePair<string, string>> NotificationEmailKeywords()
        {
            List<KeyValuePair<string, string>> kvpList = new List<KeyValuePair<string, string>>()
          {
              new KeyValuePair<string, string>("1,2,3,4", "{JobNo}"),
              new KeyValuePair<string, string>("1,2,3,4", "{Customer}"),
              new KeyValuePair<string, string>("1,2,3,4", "{Branch}"),
              new KeyValuePair<string, string>("1,2,3,4", "{JobDate}"),
              new KeyValuePair<string, string>("1,2,3,4", "{JobType}"),
              //new KeyValuePair<string, string>("1,2,3,4", "{Problem}"),
              //new KeyValuePair<string, string>("1,2,3,4", "{Product}"),
              //new KeyValuePair<string, string>("1,2,3,4", "{Brand}"),
              // new KeyValuePair<string, string>("1,2,3,4", "{Model}"),
               new KeyValuePair<string, string>("1,2,3,4", "{Engineer}"),
               new KeyValuePair<string, string>("1,2,3,4", "{CurrentStatus}"),
                 new KeyValuePair<string, string>("1,2,3,4", "{ServiceType}"),
                  new KeyValuePair<string, string>("1,2,3,4", "{PartNo}"),
                  new KeyValuePair<string, string>("1,2,3,4", "{CallDetails}"),
               new KeyValuePair<string, string>("1,2,3,4", "{Remarks}")

          };
            return kvpList;
        }

        public class EmailValues
        {
            public string JobNo { get; set; }
            public string Customer { get; set; }
            public string Branch { get; set; }
            public string JobDate { get; set; }
            public string JobType { get; set; }
            public string Problem { get; set; }
            public string Product { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public string Engineer { get; set; }
            public string CurrentStatus { get; set; }
            public string ServiceType { get; set; }
            public string PartNo { get; set; }
            public string CallDetails { get; set; }
            public string Remarks { get; set; }
            // Constructor 
            public EmailValues()
            {
                JobNo = string.Empty;
                Customer = string.Empty;
                JobDate = string.Empty;
                JobType = string.Empty;
                Problem = string.Empty;
                Product = string.Empty;
                Brand = string.Empty;
                Model = string.Empty;
                Engineer = string.Empty;
                CurrentStatus = string.Empty;
                ServiceType = string.Empty;
                PartNo = string.Empty;
            }
        }

        public class EmailDetailsSub
        {
            public string Email_Subject { get; set; }
            public string Email_Content { get; set; }
            public string MailCC { get; set; }

        }

    }
}