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