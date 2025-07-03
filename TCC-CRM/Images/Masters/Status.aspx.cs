using CRM.Artifacts;
using CRM.Artifacts.Masters;
using CRM.Bussiness;
using CRM.Bussiness.Masters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackIT.ClassModules;


namespace TCC_CRM.Masters 
{
    public partial class Status : System.Web.UI.Page
    {
        public BEStatus m_BEStatus;
        public BLLStatus bllStatus = new BLLStatus();
        public BLLCommon m_BllCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();

        protected void Page_Load(object sender, EventArgs e)
        {  
            if (!IsPostBack)
            {
                lblHeader.Text = "New Status";

                if (!string.IsNullOrEmpty(SessionMgr.DBName))
                {
                    hdnStatusID.Value = Request.QueryString["StatusID"].ToString();
                    if (Convert.ToInt32((hdnStatusID.Value)) > 0)
                        getStatuslist(Convert.ToInt32((hdnStatusID.Value)));
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }

        }
        private void getStatuslist(int StatusID)
        {
            if (m_BEStatus == null) m_BEStatus = new BEStatus();

            m_BEStatus = bllStatus.GetStatus(SessionMgr.CompanyID, StatusID, SessionMgr.DBName);
            if (m_BEStatus != null)
            {
                lblHeader.Text = "Edit Status";

                txtscode.Text = m_BEStatus.StatusCode;
                txtsname.Text = m_BEStatus.StatusName;
                txtseqno.Text = m_BEStatus.SequenceNo;
                statusimage.Attributes.Add("data-default-file", m_BEStatus.StatusImage);
                lblstatusimageFile.Text = m_BEStatus.StatusImage;
               
            }
        }
         protected void OnBtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Masters/StatusList.aspx?Saved=", false);
        }
        protected void OnbtnSaveClick(object sender, EventArgs e)
        {
            try
            {
                hdnStatusID.Value = Request.QueryString["StatusID"].ToString();
                int intExist = 0;
                int id = 0;

               
                string lblid = hdnStatusID.Value;

                if (!string.IsNullOrEmpty(lblid))
                {
                    id = Convert.ToInt32(lblid);
                }
                string statusImage = string.Empty;
                if (statusimage.HasFile)
                {
                    string str = statusimage.FileName;
                    str = SessionMgr.DBName + "_" + str;
                    if (!Directory.Exists(Server.MapPath("~/UploadDocuments/StatusImage/" + SessionMgr.DBName)))
                    {
                        // create Folder  
                        Directory.CreateDirectory(Server.MapPath("~/UploadDocuments/StatusImage/" + SessionMgr.DBName));
                    }
                    statusimage.PostedFile.SaveAs(Server.MapPath("~/UploadDocuments/StatusImage/" + SessionMgr.DBName + "/" + str));
                    statusImage = "../UploadDocuments/StatusImage/" + SessionMgr.DBName + "/" + str.ToString();
                }
                else { statusImage = lblstatusimageFile.Text; }

                //if (id == 0)
                //{
                    intExist = bllStatus.GetDuplicateExists(SessionMgr.CompanyID, "Status", "StatusCode", txtscode.Text.Trim().ToLower(), "StatusID", id, SessionMgr.DBName);
                    if (intExist > 0)
                    { 
                        lblvalue.Text = "Code Already Exists";
                   
                        lblvalue.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                        return;

                    }
                //}

                    //Commented by Velu on 19-02-2020, Edit mode also must check the code duplicate

                BEStatus br = new BEStatus();
                br.StatusCode = txtscode.Text.Trim();
                br.StatusName = txtsname.Text.Trim();
                br.SequenceNo = txtseqno.Text.Trim();
                br.StatusImage = statusImage;
                br.StatusID = id;
                br.CreatedBy = SessionMgr.UserID;
                br.ModifiedBy = SessionMgr.UserID;
                int insertorupdate = bllStatus.InsertOrUpdateRecord(br, SessionMgr.DBName);
                if (insertorupdate > 0)
                {
                    //AlertMsg("Saved Successfully");
                    Response.Redirect("~/Masters/StatusList.aspx", false);

                }

            }
            catch (Exception ex)
            {
                BeCommon.FormName = "StatusList";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BllCommon.UpdateLog(BeCommon, SessionMgr.DBName);
            }

        }

        #region Alerts
        private void AlertMsg1(string Msg)
        {

            { ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + Msg + "');", true); }
        }
        #endregion
    }
}