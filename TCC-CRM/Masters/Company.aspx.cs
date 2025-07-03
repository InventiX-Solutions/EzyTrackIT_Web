using CRM.Artifacts;
using CRM.Artifacts.Masters;
using CRM.Bussiness;
using CRM.Bussiness.Masters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackIT.ClassModules;

namespace TCC_CRM.Masters
{
    public partial class Company : System.Web.UI.Page
    {
        public BECompany m_BECompany;
        public BLLCompany bllCompany = new BLLCompany();
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtremarks.Attributes.Add("maxlength", txtremarks.MaxLength.ToString());

                if (!string.IsNullOrEmpty(SessionMgr.DBName))
                {
                    //hdnCompanyID.Value = Request.QueryString["CompanyID"].ToString();
                    hdnCompanyID.Value = SessionMgr.CompanyID.ToString();
                    GetCompanylist(Convert.ToInt32((hdnCompanyID.Value)));

                    if (SessionMgr.UserName.ToUpper() == "PERITUSADMIN" || (SessionMgr.UserName == "admin" && SessionMgr.ClientCode.ToUpper() == "PGS_TRACKIT"))
                    {
                        generallinksettings.Visible = true;
                      // MobileDetailslinksettings.Visible = true;
                       
                    }
                    else
                    {
                        generallinksettings.Visible = false;
                       // MobileDetailslinksettings.Visible = false;
                      
                    }

                }

                    


                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        private void GetCompanylist(int CompanyID)
        {
            if (m_BECompany == null) m_BECompany = new BECompany();

            m_BECompany = bllCompany.GetCompany(CompanyID, SessionMgr.DBName);
            if (m_BECompany != null)
            {
                txtccode.Text = m_BECompany.CompanyCode;
                txtcname.Text = m_BECompany.CompanyName;
                txtadd1.Text = m_BECompany.AddressLine1;
                txtadd2.Text = m_BECompany.AddressLine2;
                txtcity.Text = m_BECompany.City;
                txtstate.Text = m_BECompany.State;
                txtcountry.Text = m_BECompany.Country;
                txtpostalcode.Text = Convert.ToInt32(m_BECompany.PostalCode).ToString();
                FileUpload1.Attributes.Add("data-default-file", m_BECompany.companylogo);
                lbllogo.Text = m_BECompany.companylogo;
                FileUpload2.Attributes.Add("data-default-file", m_BECompany.companylogo2);
                lbllogo2.Text = m_BECompany.companylogo2;
                txtphoneno.Text = m_BECompany.PhoneNo;
                txtfaxno.Text = m_BECompany.FaxNo;
                txtwebsite.Text = m_BECompany.Website;
                txtmailid.Text = m_BECompany.EmailID;
                txttino.Text = m_BECompany.TINNO;
                txtGMT.Text = m_BECompany.GMT;
                //txtpanno.Text = m_BECompany.PANNO;
                // txtservicetax.Text = m_BECompany.SeriveTax;
                txtremarks.Text = m_BECompany.Remarks;

                txtSMTPPort.Text = m_BECompany.SMTPPort;
                txtSMTPHost.Text = m_BECompany.SMTPHost;
                txtSMTPUserName.Text = m_BECompany.SMTPUserName;
                txtSMTPPassword.Text = m_BECompany.SMTPPassword;
                txtMailCC.Text = m_BECompany.MailCC;
                txtMailBCC.Text = m_BECompany.MailBCC;
                txtMailSubject.Text = m_BECompany.MailSubject;
                txtMailContent.Html = m_BECompany.MailContent;



                if (m_BECompany.Mobile_Menu_Attendance==1)
                {
                    chkMobileMenuAttendance.Checked = true;
                    lblchkMobile_Menu_Attendance.Text = "Yes";
                }
                else
                {
                    chkMobileMenuAttendance.Checked = false;
                    lblchkMobile_Menu_Attendance.Text = "No";
                }


                if (m_BECompany.Mobile_Menu_AttendanceHistry==1)
                {
                    chkMobile_Menu_AttendanceHistry.Checked = true;
                    lblMobile_Menu_AttendanceHistry.Text = "Yes";
                }
                else
                {
                    chkMobile_Menu_AttendanceHistry.Checked = false;
                    lblMobile_Menu_AttendanceHistry.Text = "No";
                }



                if (m_BECompany.Mobile_Menu_NewJob==1)
                {
                    chkMobile_Menu_NewJob.Checked = true;
                    lblMobile_Menu_NewJob.Text = "Yes";
                }
                else
                {
                    chkMobile_Menu_NewJob.Checked = false;
                    lblMobile_Menu_NewJob.Text = "No";
                }




                if (m_BECompany.Mobile_Menu_GetMyJobList==1)
                {
                    chkMobile_Menu_GetMyJobList.Checked = true;
                    lblMobile_Menu_GetMyJobList.Text = "Yes";
                }
                else
                {
                    chkMobile_Menu_GetMyJobList.Checked = false;
                    lblMobile_Menu_GetMyJobList.Text = "No";
                }



                if (m_BECompany.Mobile_Menu_CompletedJob==1)
                {
                    chkMobile_Menu_CompletedJob.Checked = true;
                    lblMobile_Menu_CompletedJob.Text = "Yes";
                }
                else
                {
                    chkMobile_Menu_CompletedJob.Checked = false;
                    lblMobile_Menu_CompletedJob.Text = "No";

                }

                if (m_BECompany.Mobile_Menu_MoreDetails==1)
                {
                    chkMobile_Menu_MoreDetails.Checked = true;
                    lblMobile_Menu_MoreDetails.Text = "Yes";
                }
                else
                {
                    chkMobile_Menu_MoreDetails.Checked = false;
                    lblMobile_Menu_MoreDetails.Text = "No";

                }


                if (m_BECompany.Mobile_Report_Send_to_Mail==1)
                {
                    chkMobile_Report_Send_to_Mail.Checked = true;
                    lblMobile_Report_Send_to_Mail.Text = "Yes";
                }
                else
                {
                    chkMobile_Report_Send_to_Mail.Checked = false;
                    lblMobile_Report_Send_to_Mail.Text = "No";

                }


                if (m_BECompany.Mobile_Report_download==1)
                {
                    chkMobile_Report_download.Checked = true;
                    lblMobile_Report_download.Text = "Yes";
                }
                else
                {
                    chkMobile_Report_download.Checked = false;
                    lblMobile_Report_download.Text = "No";

                }


                if (m_BECompany.EmailFlag == 1)
                {
                    chkEmail.Checked = true;
                    lblchkEmail.Text = "Yes";
                }
                else
                {
                    chkEmail.Checked = false;
                    lblchkEmail.Text = "No";
                }

            }
        }

        protected void OnBtnSaveClick(object sender, EventArgs e)
        {
            try
            {
                //hdnCompanyID.Value = Request.QueryString["CompanyID"].ToString();
                int intExist = 0;
                int id = 0;

                //string lblid = lblcustomerid.Text;
                string lblid = hdnCompanyID.Value;

                if (!string.IsNullOrEmpty(lblid))
                {
                    id = Convert.ToInt32(lblid);
                }
                //if (id == 0)
                //{
                intExist = bllCompany.GetDuplicateExists(SessionMgr.CompanyID, "company", "CompanyCode", txtccode.Text.Trim().ToLower(), "CompanyID", id, SessionMgr.DBName);
                if (intExist > 0)
                {
                    lblval.Text = "Code Already Exists";
                    lblval.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                    return;
                }
                //}//Commented by Velu on 19-02-2020, Edit mode also must check the code duplicate

                string Image = string.Empty;
                string Image2 = string.Empty;
                if (FileUpload1.HasFile)
                {
                    string str = FileUpload1.FileName;
                    str = SessionMgr.ClientCode + "_" + str;
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/UploadDocuments/CompanyLogo/" + str));
                    Image = "../UploadDocuments/CompanyLogo/" + str.ToString();
                }
                else { Image = lbllogo.Text; }

                if (FileUpload2.HasFile)
                {
                    string str = FileUpload2.FileName;
                    str = SessionMgr.ClientCode + "_" + str;
                    FileUpload2.PostedFile.SaveAs(Server.MapPath("~/UploadDocuments/CompanyLogo/" + str));
                    Image2 = "../UploadDocuments/CompanyLogo/" + str.ToString();
                }
                else { Image2 = lbllogo2.Text; }

                BECompany br = new BECompany();
                br.CompanyCode = txtccode.Text.Trim();
                br.CompanyName = txtcname.Text.Trim();
                br.AddressLine1 = txtadd1.Text.Trim();
                br.AddressLine2 = txtadd2.Text.Trim();
                br.companylogo = Image;
                br.companylogo2 = Image2;
                br.City = txtcity.Text.Trim();
                br.State = txtstate.Text.Trim();
                br.Country = txtcountry.Text.Trim();
                br.PostalCode = Convert.ToInt32(txtpostalcode.Text.Trim());
                br.PhoneNo = txtphoneno.Text.Trim();
                br.FaxNo = txtfaxno.Text.Trim();
                br.Website = txtwebsite.Text.Trim();
                br.EmailID = txtmailid.Text.Trim();
                br.TINNO = txttino.Text.Trim();
                br.GMT = txtGMT.Text.Trim();
                // br.PANNO = txtpanno.Text.Trim();
                // br.SeriveTax = txtservicetax.Text.Trim();
                br.Remarks = txtremarks.Text.Trim();

                br.SMTPPort = txtSMTPPort.Text.Trim();
                br.SMTPHost = txtSMTPHost.Text.Trim();
                br.SMTPUserName = txtSMTPUserName.Text.Trim();
                br.SMTPPassword = txtSMTPPassword.Text.Trim();
                br.MailCC = txtMailCC.Text.Trim();
                br.MailBCC = txtMailBCC.Text.Trim();
                br.MailSubject = txtMailSubject.Text.Trim();
                br.MailContent = txtMailContent.Html.Trim();

                if (chkMobileMenuAttendance.Checked)
                {
                    br.Mobile_Menu_Attendance = 1;
                }
                else
                {
                    br.Mobile_Menu_Attendance = 0;
                }
                if (chkEmail.Checked)
                {
                    br.EmailFlag = 1;
                }
                else
                {
                    br.EmailFlag = 0;
                }


                if (chkMobile_Menu_AttendanceHistry.Checked)
                {
                    br.Mobile_Menu_AttendanceHistry = 1;
                }
                else
                {
                    br.Mobile_Menu_AttendanceHistry = 0;
                }

                if (chkMobile_Menu_NewJob.Checked)
                {
                    br.Mobile_Menu_NewJob = 1;
                }
                else
                {
                    br.Mobile_Menu_NewJob = 0;
                }


                 if (chkMobile_Menu_GetMyJobList.Checked)
                {
                    br.Mobile_Menu_GetMyJobList = 1;
                }
                else
                {
                    br.Mobile_Menu_GetMyJobList = 0;
                }


                 if (chkMobile_Menu_CompletedJob.Checked)
                {
                    br.Mobile_Menu_CompletedJob = 1;
                }
                else
                {
                    br.Mobile_Menu_CompletedJob = 0;
                }


                   if (chkMobile_Menu_MoreDetails.Checked)
                {
                    br.Mobile_Menu_MoreDetails = 1;
                }
                else
                {
                    br.Mobile_Menu_MoreDetails = 0;
                }


                   if (chkMobile_Report_Send_to_Mail.Checked)
                {
                    br.Mobile_Report_Send_to_Mail = 1;
                }
                else
                {
                    br.Mobile_Report_Send_to_Mail = 0;
                }


               
                   if (chkMobile_Report_download.Checked)
                {
                    br.Mobile_Report_download = 1;
                }
                else
                {
                    br.Mobile_Report_download = 0;
                }
               
                br.CompanyID = id;
                br.createdby = SessionMgr.UserID;
                br.modifiedby = SessionMgr.UserID;
                int insertorupdate = bllCompany.InsertOrUpdateRecord(br, SessionMgr.DBName);
                if (insertorupdate > 0)
                {
                    AlertMsg("Saved Successfully");
                    //Response.Redirect("~/Masters/Company.aspx", false);
                    GetCompanylist(Convert.ToInt32((hdnCompanyID.Value)));
                }
            }
            catch (Exception ex)
            {
                BeCommon.FormName = "CompanyList";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BLLCommon.UpdateLog(BeCommon, SessionMgr.DBName);
            }
        }

        #region Alerts
        private void AlertMsg(string Msg)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + Msg + "');", true);
        }
        #endregion

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Masters/CompanyList.aspx");
        }
    }
}