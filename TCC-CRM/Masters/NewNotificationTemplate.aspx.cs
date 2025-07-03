using ClosedXML.Excel;
//using CRM.Artifacts;
//using EzyVisIT.Business;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using TrackIT.ClassModules;
using System.Diagnostics;
using System.Reflection;
using System.Web.Services;
using System.Xml;
using TrackIT.ClassModules;
using CRM.Bussiness;
using CRM.Artifacts.Masters;
using CRM.Business;

namespace TCC_CRM.Masters
{
    public partial class NewNotificationTemplate : System.Web.UI.Page
    {
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BLLCommon bll = new BLLCommon();
        public Common cm = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SessionMgr.DBName))
            {
                try
                {
                    if (!IsPostBack)
                    {
                        //  cm.ActivityLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name);
                        LoadDefaultVaues();

                        //if (SessionMgr.EmailFlag == 0)
                        //{
                        //    chkEmail.Enabled = false;
                        //    //txtMailSubject.Enabled = false;
                        //    txtMailSubject.Attributes.Add("disabled", "disabled");
                        //    txtEmailContent.Enabled = false;
                        //    divEmailRecipientType.Attributes.Add("disabled", "disabled");           //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
                        //}
                        //else
                        //{
                        chkEmail.Enabled = true;
                        divWhatsappContent.Visible = false;
                        divemailcategoryrecip.Visible = false;
                        divEmailRecipientType.Visible = false;
                        divSMSRecipientType.Visible = false;
                        divWhatsappRecipientType.Visible = false;
                        divSMScontent.Visible = false;

                        //}
                        //if (SessionMgr.SMSFlag == 0)
                        //{
                        //    chkSMS.Enabled = false;
                        //    //txtSMSSubject.Enabled = false;
                        //    //txtSMSContent.Enabled = false;
                        //    txtSMSSubject.Attributes.Add("disabled", "disabled");
                        //    txtSMSContent.Attributes.Add("disabled", "disabled");
                        //    divSMSRecipientType.Attributes.Add("disabled", "disabled");         //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
                        //}
                        //else
                        //{
                        //    chkSMS.Enabled = true;

                        //}
                        //if (SessionMgr.WhatsappFlag == 0)
                        //{
                        //    chkWhatsApp.Enabled = false;
                        //    txtWhatsappContent.Attributes.Add("disabled", "disabled");
                        //    divWhatsappRecipientType.Attributes.Add("disabled", "disabled");            //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
                        //    //txtWhatsappContent.Enabled = false;
                        //}
                        //else
                        //{
                        //    chkWhatsApp.Enabled = true;
                        //}

                        int idvalue = Convert.ToInt32(Request.QueryString["id"].ToString());
                        if (idvalue > 0)
                        {
                            // cmbTemplateType.Attributes.Add("disabled", "disabled");
                            cmbTemplateType.Enabled = false;
                            getNotificationTemplatedetails(idvalue);
                        }

                    }

                    chkWhatsAppChkBX.Visible = false;
                    divWhatsappContent.Visible = false;
                    divWhatsappRecipientType.Visible = false;
                    if (SessionMgr.UserName.ToUpper() == "PeritusAdmin".ToUpper())
                    {
                        chkWhatsAppChkBX.Visible = true;
                        divWhatsappContent.Visible = true;
                        divWhatsappRecipientType.Visible = true;

                    }


                }
                catch (Exception ex)
                {
                    //     cm.ErrorLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.GetType().ToString(), ex.Source.ToString(), SessionMgr.ClientCode);
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void LoadDefaultVaues()
        {
            try
            {
                DataTable dtLoadDropdownvalues = new DataTable();
                dtLoadDropdownvalues = Common.GetNotificationType();
                cmbTemplateType.DataSource = dtLoadDropdownvalues;
                cmbTemplateType.DataTextField = "Name";
                cmbTemplateType.DataValueField = "Value";
                cmbTemplateType.DataBind();
                cmbTemplateType.Items.Insert(0, new ListItem("Please Select", "0"));
                cmbTemplateType.SelectedIndex = 0;
                cmbTemplateType_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                // cm.ErrorLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.GetType().ToString(), ex.Source.ToString(), SessionMgr.ClientCode);
            }
        }

        //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
        private void LoadNotificationRecipient(string notificationType)
        {
            try
            {
                DataTable dtLoadNotificationRecipient = new DataTable();
                dtLoadNotificationRecipient = Common.GetNotificationRecipient();
                if (notificationType == "5")
                {
                    rdoEmailRecipientType.DataSource = dtLoadNotificationRecipient.Select("Type = 3").CopyToDataTable();
                    rdoSMSRecipientType.DataSource = dtLoadNotificationRecipient.Select("Type = 3").CopyToDataTable();
                    rdoWhatsappRecipientType.DataSource = dtLoadNotificationRecipient.Select("Type = 3").CopyToDataTable();
                }
                else if (notificationType == "6")
                {
                    rdoEmailRecipientType.DataSource = dtLoadNotificationRecipient.Select("Type = 2").CopyToDataTable();
                    rdoSMSRecipientType.DataSource = dtLoadNotificationRecipient.Select("Type = 2").CopyToDataTable();
                    rdoWhatsappRecipientType.DataSource = dtLoadNotificationRecipient.Select("Type = 2").CopyToDataTable();
                }
                else
                {
                    rdoEmailRecipientType.DataSource = dtLoadNotificationRecipient.Select("Type = 1").CopyToDataTable();
                    rdoSMSRecipientType.DataSource = dtLoadNotificationRecipient.Select("Type = 1").CopyToDataTable();
                    rdoWhatsappRecipientType.DataSource = dtLoadNotificationRecipient.Select("Type = 1").CopyToDataTable();
                }

                rdoEmailRecipientType.DataTextField = "Name";
                rdoEmailRecipientType.DataValueField = "Value";
                rdoEmailRecipientType.DataBind();

                rdoSMSRecipientType.DataTextField = "Name";
                rdoSMSRecipientType.DataValueField = "Value";
                rdoSMSRecipientType.DataBind();

                rdoWhatsappRecipientType.DataTextField = "Name";
                rdoWhatsappRecipientType.DataValueField = "Value";
                rdoWhatsappRecipientType.DataBind();


            }
            catch (Exception ex)
            {
                // cm.ErrorLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.GetType().ToString(), ex.Source.ToString(), SessionMgr.ClientCode);
            }
        }
        //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration

        private void getNotificationTemplatedetails(int Templateid)
        {
            try
            {
                //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
                string columns = "TemplateType_ID, TemplateType, EmailFlag, SMSFlag, WhatsappFlag, MailSubject, MailContent, SMSSubject, SMSContent, WhatsappContent, TemplateName, Category_ID, Category_ID_Text, MailRecipientType, SMSRecipientType, WhatsappRecipientType,MailCC";
                //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
                DataTable dt = bll.GetMastersDetailswithTable("NotificationTemplate", "Template_ID", Templateid, columns, SessionMgr.CompanyID, SessionMgr.DBName);

                foreach (DataRow row in dt.Rows)
                {
                    cmbTemplateType.Text = row["TemplateType_ID"].ToString();
                    txtMailSubject.Text = row["MailSubject"].ToString();
                    txtEmailContent.Html = row["MailContent"].ToString();
                    txtSMSSubject.Text = row["SMSSubject"].ToString();
                    txtSMSContent.Text = row["SMSContent"].ToString();
                    txtWhatsappContent.Text = row["WhatsappContent"].ToString();
                    txtTemplateName.Text = row["TemplateName"].ToString();
                    //  txtemailcc.Text = row["MailCC"].ToString();
                    txtemailcc.Text = row["MailCC"] != DBNull.Value ? row["MailCC"].ToString() : string.Empty;

                    if (Convert.ToInt32(row["WhatsappFlag"].ToString()) == 1)
                    {
                        chkWhatsApp.Checked = true;
                        rdoWhatsappRecipientType.SelectedValue = row["WhatsappRecipientType"].ToString();           //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
                    }
                    else
                    {
                        chkWhatsApp.Checked = false;
                        txtWhatsappContent.Attributes.Add("disabled", "disabled");
                        divEmailRecipientType.Attributes.Add("disabled", "disabled");            //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
                    }

                    if (Convert.ToInt32(row["EmailFlag"].ToString()) == 1)
                    {
                        chkEmail.Checked = true;
                        rdoEmailRecipientType.SelectedValue = row["MailRecipientType"].ToString();          //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
                    }
                    else
                    {
                        chkEmail.Checked = false;
                        txtMailSubject.Attributes.Add("disabled", "disabled");
                        txtEmailContent.Attributes.Add("disabled", "disabled");
                        divEmailRecipientType.Attributes.Add("disabled", "disabled");           //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
                    }

                    if (Convert.ToInt32(row["SMSFlag"].ToString()) == 1)
                    {
                        chkSMS.Checked = true;
                        rdoSMSRecipientType.SelectedValue = row["SMSRecipientType"].ToString();         //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
                    }
                    else
                    {
                        chkSMS.Checked = false;
                        txtSMSSubject.Attributes.Add("disabled", "disabled");
                        txtSMSContent.Attributes.Add("disabled", "disabled");
                        divSMSRecipientType.Attributes.Add("disabled", "disabled");             //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
                    }

                    ASPxListBox objListbox = chkCategory.FindControl("listbox") as ASPxListBox;
                    string[] strCategoryIDs = new string[0];
                    if (!string.IsNullOrEmpty(row["Category_ID"].ToString()))
                        strCategoryIDs = row["Category_ID"].ToString().Split(',');

                    foreach (ListEditItem item in objListbox.Items)
                    {
                        for (int i = 0; i < strCategoryIDs.Length; i++)
                        {
                            if (item.Value.ToString() == strCategoryIDs[i].ToString())
                                item.Selected = true;
                        }
                    }

                    cmbTemplateType_SelectedIndexChanged(null, null);
                }
            }
            catch (Exception ex)
            {

                //   cm.ErrorLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.GetType().ToString(), ex.Source.ToString(), SessionMgr.ClientCode);
            }
        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {

            try
            {
                //  cm.ActivityLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name);
                int idvalue = Convert.ToInt32(Request.QueryString["id"].ToString());
                BENotificationTemplate u = new BENotificationTemplate();
                u.Template_ID = idvalue;
                u.TemplateType_ID = Convert.ToInt32(cmbTemplateType.SelectedValue.ToString());
                u.TemplateType = cmbTemplateType.SelectedItem.Text.ToString();

                u.TemplateName = txtTemplateName.Text.ToString();

                ASPxListBox objListbox = chkCategory.FindControl("listbox") as ASPxListBox;
                string sCategoryIDs = string.Empty;
                for (int i = 0; i < objListbox.SelectedValues.Count; i++)
                {
                    if (!string.IsNullOrEmpty(sCategoryIDs))
                        sCategoryIDs += ",";

                    sCategoryIDs += objListbox.SelectedValues[i];
                }

                u.Category_ID = sCategoryIDs;
                u.Category_ID_Text = chkCategory.Text;

                if (chkWhatsApp.Checked)
                {
                    u.WhatsappFlag = 1;
                    u.WhatsappContent = txtWhatsappContent.Text.ToString();
                    u.WhatsappRecipientType = rdoWhatsappRecipientType.SelectedValue;   //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
                }
                else
                {
                    u.WhatsappFlag = 0;
                    u.WhatsappContent = string.Empty;
                    u.WhatsappRecipientType = string.Empty; //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
                }

                List<string> ccEmails = new List<string>();

                // Split the emails by comma and loop through each
                string[] emails = txtemailcc.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string email in emails)
                {
                    string trimmedEmail = email.Trim();  // Trim any extra spaces
                    if (!string.IsNullOrEmpty(trimmedEmail))
                    {
                        ccEmails.Add(trimmedEmail);  // Add valid email to the list
                    }
                }

                // Example: Assign the ccEmails list to your object's MailCC field (assuming it's a string or list)
                u.MailCC = string.Join(",", ccEmails);
                u.EmailFlag = 1;
                //if (chkEmail.Checked)
                // {
                u.EmailFlag = 1;
                u.MailSubject = txtMailSubject.Text.ToString();
                u.MailContent = txtEmailContent.Html.ToString();
                //  u.MailRecipientType = rdoEmailRecipientType.SelectedValue;   //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
                //}
                //else
                //{
                //    u.EmailFlag = 0;
                //    u.MailSubject = string.Empty;
                //    u.MailContent = string.Empty;
                //    u.MailRecipientType = string.Empty;   //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
                //}

                if (chkSMS.Checked)
                {
                    u.SMSFlag = 1;
                    u.SMSSubject = txtSMSSubject.Text.ToString();
                    u.SMSContent = txtSMSContent.Text.ToString();
                    u.SMSRecipientType = rdoSMSRecipientType.SelectedValue;   //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
                }
                else
                {
                    u.SMSFlag = 0;
                    u.SMSSubject = string.Empty;
                    u.SMSContent = string.Empty;
                    u.SMSRecipientType = string.Empty;   //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
                }
                u.EmailFlag = 1;
                u.CreatedBy = SessionMgr.UserID;
                u.Company_ID = SessionMgr.CompanyID;

                string sCondition = string.Empty;

                if (Convert.ToInt32(idvalue) > 0)
                    sCondition = " and Template_ID != " + u.Template_ID;

                if (idvalue > 0)
                {
                    string updatedvalues = "TemplateType_ID='" + u.TemplateType_ID + "', TemplateType ='" + u.TemplateType + "', EmailFlag='" + u.EmailFlag
                        + "', SMSFlag='" + u.SMSFlag + "', WhatsappFlag='" + u.WhatsappFlag + "', MailSubject='" + u.MailSubject + "', MailContent='" + u.MailContent
                        + "', SMSSubject='" + u.SMSSubject + "', SMSContent='" + u.SMSContent + "', WhatsappContent='" + u.WhatsappContent
                        + "', TemplateName='" + u.TemplateName + "', Category_ID ='" + u.Category_ID + "', Category_ID_Text='" + u.Category_ID_Text
                        //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
                        + "', MailRecipientType='" + u.MailRecipientType + "', SMSRecipientType='" + u.SMSRecipientType + "', WhatsappRecipientType='" + u.WhatsappRecipientType + "', MailCC='" + u.MailCC
                        //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
                        + "', ModifiedDT=now(),ModifiedBy=" + SessionMgr.UserID + "";
                    string conditions = "Template_ID=" + idvalue + "";

                    int res = bll.UpdateMasters("NotificationTemplate", updatedvalues, conditions, SessionMgr.CompanyID, SessionMgr.DBName);

                    if (res > 0)
                    {
                        Response.Redirect("~/Masters/NotificationTemplateList.aspx?Saved=1", false);
                    }
                    else
                    {
                        err.Text = "Something went wrong, Refresh page and try again";
                    }
                }
                else
                {
                    BLLNotificationTemplate objBusiness = new BLLNotificationTemplate();
                    string sMessage = string.Empty;
                    int insertorupdate = objBusiness.InsertOrUpdateRecord(u, out sMessage, SessionMgr.DBName);
                    if (insertorupdate > 0)
                    {
                        //  SendEmail(insertorupdate.ToString() );
                        Response.Redirect("~/Masters/NotificationTemplateList.aspx?Saved=1", false);
                    }
                    else
                    {
                        string[] msg = sMessage.Split(':');
                        //  cm.ErrorLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name, msg[0], msg[1], msg[2], SessionMgr.ClientCode);
                        err.Text = "Something went wrong, Refresh page and try again";
                    }
                }

            }
            catch (Exception ex)
            {
                err.Text = "Something went wrong, Refresh page and try again";
                //  cm.ErrorLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.GetType().ToString(), ex.Source.ToString(), SessionMgr.ClientCode);
            }
        }

        [WebMethod()]
        public static string VerifyCode(string code)
        {
            Common cm = new Common();
            string result = string.Empty;
            BLLCommon bll = new BLLCommon();
            try
            {
                result = bll.VerifyCodes(SessionMgr.CompanyID, "TemplateType_ID", code, "NotificationTemplate", SessionMgr.ClientCode, string.Empty);
            }
            catch (Exception ex)
            {
                //  cm.ErrorLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.GetType().ToString(), ex.Source.ToString(), SessionMgr.ClientCode);
            }
            return result;
        }

        protected void cmbTemplateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
            if (cmbTemplateType.SelectedValue != "0")
            {
                //   LoadNotificationRecipient(cmbTemplateType.SelectedValue);
                LoadKeywords(cmbTemplateType.SelectedValue);
            }
            //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
        }

        private void LoadKeywords(string id)
        {
            try
            {
                //string emailKey = string.Empty;
                //emailKey = "  <ul class=\"tags\">";
                //foreach (string value in Email.Emailkeywords())
                //{
                //    emailKey += "<span id=\"" + value + "\" class=\"tag\" draggable=\"true\" ondragstart=\"drag(event)\"><b>" + value + "</b></span>";
                //}
                //emailKey += " </ul>";
                //keywords.InnerHtml = emailKey;

                string emailKey = string.Empty;
                emailKey = " <ul class=\"tags\">";

                var kword = Email.NotificationEmailKeywords();
                var kwords = kword.Where(p => p.Key.Contains(id));
                foreach (KeyValuePair<string, string> k in kwords)
                {
                    emailKey += "<span id=\"" + k.Value + "\" class=\"tag\" draggable=\"true\" ondragstart=\"drag(event)\"><b>" + k.Value + "</b></span>";
                }


                emailKey += " </ul>";
                keywords.InnerHtml = emailKey;
            }
            catch (Exception ex)
            {
                //  cm.ErrorLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.GetType().ToString(), ex.Source.ToString(), SessionMgr.ClientCode);
            }
        }

        protected void ASPxComboBoxInstance_Init(object sender, EventArgs e)
        {
            try
            {
                ASPxListBox cmb = (ASPxListBox)sender;

                DataTable dtLoadDropdownvalues = m_BLLCommon.GetDropDownList(SessionMgr.CompanyID, "Category_Name", "Category_ID", "category", " Category_Type = 'V'", SessionMgr.ClientCode);

                cmb.DataSource = dtLoadDropdownvalues;
                cmb.TextField = "Name";
                cmb.ValueField = "Value";
                cmb.DataBindItems();
            }
            catch (Exception ex)
            {
                //   cm.ErrorLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.GetType().ToString(), ex.Source.ToString(), SessionMgr.ClientCode);
            }
        }

        protected void ASPxComboBoxInstance_DataBound(object sender, EventArgs e)
        {
            try
            {
                int idvalue = Convert.ToInt32(Request.QueryString["id"].ToString());
                if (idvalue <= 0)
                {
                    ASPxListBox listBox = (ASPxListBox)sender;

                    foreach (ListEditItem item in listBox.Items)
                    {
                        item.Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                //  cm.ErrorLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.GetType().ToString(), ex.Source.ToString(), SessionMgr.ClientCode);
            }
        }




        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        foreach (XmlNode node in Common.XmlRead((string.IsNullOrEmpty(SessionMgr.Language)) ? "en_GB" : SessionMgr.Language, "NewNotificationTemplateMaster"))
        //        {
        //            spnNewNotificationTemplateHeader.InnerText = Common.LoadLanguageValue(node.SelectSingleNode("spnNewNotificationTemplateHeader").InnerText, "Notification Template");
        //            Savebtn.Text = Common.LoadLanguageValue(node.SelectSingleNode("spnNewNotificationTemplateSave").InnerText, "Save");
        //            spnNewNotificationTemplateBranchBack.InnerText = Common.LoadLanguageValue(node.SelectSingleNode("spnNewNotificationTemplateBranchBack").InnerText, "Back");

        //            spnNewNotificationTemplateType.InnerText = Common.LoadLanguageValue(node.SelectSingleNode("spnNewNotificationTemplateType").InnerText, "Type");
        //            spnNewNotificationTemplateNotification.InnerText = Common.LoadLanguageValue(node.SelectSingleNode("spnNewNotificationTemplateNotification").InnerText, "Notification");
        //            spnNewNotificationTemplateEmailSubject.InnerText = Common.LoadLanguageValue(node.SelectSingleNode("spnNewNotificationTemplateEmailSubject").InnerText, "Email Subject");
        //            spnNewNotificationTemplateEmailContent.InnerText = Common.LoadLanguageValue(node.SelectSingleNode("spnNewNotificationTemplateEmailContent").InnerText, "Email Content");
        //            spnNewNotificationTemplateSMSSubject.InnerText = Common.LoadLanguageValue(node.SelectSingleNode("spnNewNotificationTemplateSMSSubject").InnerText, "SMS Subject");
        //            spnNewNotificationTemplateSMSContent.InnerText = Common.LoadLanguageValue(node.SelectSingleNode("spnNewNotificationTemplateSMSContent").InnerText, "SMS Content");
        //            spnNewNotificationTemplateWhatsappContent.InnerText = Common.LoadLanguageValue(node.SelectSingleNode("spnNewNotificationTemplateWhatsappContent").InnerText, "Whatsapp Content");
        //            spnNewNotificationTemplateKeywords.InnerText = Common.LoadLanguageValue(node.SelectSingleNode("spnNewNotificationTemplateKeywords").InnerText, "Keywords");

        //            //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
        //            spnEmailRecipientType.InnerText = Common.LoadLanguageValue(node.SelectSingleNode("spnEmailRecipientType").InnerText, "Email Recipients");
        //            spnSMSRecipientType.InnerText = Common.LoadLanguageValue(node.SelectSingleNode("spnSMSRecipientType").InnerText, "SMS Recipients");
        //            spnWhatsappRecipientType.InnerText = Common.LoadLanguageValue(node.SelectSingleNode("spnWhatsappRecipientType").InnerText, "Whatsapp Recipients");
        //            //Added by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //     //   cm.ErrorLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.GetType().ToString(), ex.Source.ToString(), SessionMgr.ClientCode);
        //    }
        //}
    }
}