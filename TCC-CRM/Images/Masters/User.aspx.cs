using CRM.Artifacts;
using CRM.Artifacts.Masters;
using CRM.Bussiness;
using CRM.Bussiness.Masters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using TrackIT.ClassModules;
using System.IO;


namespace TCC_CRM.Masters
{
    public partial class User : System.Web.UI.Page
    {
       

        public BEUser m_BEUser;
        public BLLUserMaster bllUser = new BLLUserMaster();
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblHeader.Text = "New User";
                if (!string.IsNullOrEmpty(SessionMgr.DBName))
                {
                    hdnUserID.Value = Request.QueryString["UserID"].ToString();
                    LoadDefaultVaues();
                    if (Convert.ToInt32((hdnUserID.Value)) > 0)
                        GetUserlist(Convert.ToInt32((hdnUserID.Value)));
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        private void LoadDefaultVaues()
        {
            try
            {

                DataTable dtLoadDropdownvalues = new DataTable();
                dtLoadDropdownvalues = m_BLLCommon.GetDropDownList(SessionMgr.CompanyID, "usertype", "typeid", "usertype", string.Empty, SessionMgr.DBName);

                cmbusertype.DataSource = dtLoadDropdownvalues;
                cmbusertype.DataTextField = "Name";
                cmbusertype.DataValueField = "Value";
                cmbusertype.DataBind();
                cmbusertype.Items.Insert(0, "Please Select");
                //cmbusertype.SelectedIndex = 1;
                cmbusertype.SelectedValue = "1";
            }
            catch (Exception ex)
            {
                AlertMsg(ex.Message);
            }

        }

        private void GetUserlist(int UserID)
        {
            if (m_BEUser == null) m_BEUser = new BEUser();

            m_BEUser = bllUser.GetUser(UserID, SessionMgr.DBName);
            if (m_BEUser != null)
            {
                lblHeader.Text = "Edit User";

                txtusercode.Text = m_BEUser.UserCode;
                txtUserName.Text = m_BEUser.UserName;
                txtPasswords.Text = m_BEUser.Password;
                txtLoginName.Text = m_BEUser.LoginName;
                txtmobile.Text =m_BEUser.MobileNo;
                FileUpload1.Attributes.Add("data-default-file",m_BEUser.Photo);
                lblUserFile.Text = m_BEUser.Photo;
                if(m_BEUser.usertype > 0)
                {
                    cmbusertype.SelectedValue = m_BEUser.usertype.ToString();
                }
                txtmailid.Text = m_BEUser.EmailID;
            }
        }

        protected void OnBtnSaveClick(object sender, EventArgs e)
        {
            try
            {
                hdnUserID.Value = Request.QueryString["UserID"].ToString();
                int intExist = 0;
                int id = 0;
                               
                string lblid = hdnUserID.Value;
                //Iamge
                string Image = string.Empty;
                if (FileUpload1.HasFile)
                {
                    string str = FileUpload1.FileName;
                    str = SessionMgr.ClientCode + "_" + str;
                    if (!Directory.Exists(Server.MapPath("~/UploadDocuments/User/UserPhoto/"+SessionMgr.DBName)))
                    {
                        // create Folder  
                        Directory.CreateDirectory(Server.MapPath("~/UploadDocuments/User/UserPhoto/" + SessionMgr.DBName));
                    }
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/UploadDocuments/User/UserPhoto/" + SessionMgr.DBName+"/" + str));
                    Image = "../UploadDocuments/User/UserPhoto/" + SessionMgr.DBName + "/" +str.ToString();
                }
                else { Image = lblUserFile.Text; }

                //iamge

                if (!string.IsNullOrEmpty(lblid))
                {
                    id = Convert.ToInt32(lblid);
                }
                //if (id == 0)
                //{
                //user code duplication checking
                    intExist = bllUser.GetDuplicateExists(SessionMgr.CompanyID, "usersetup", "UserCode", txtusercode.Text.Trim().ToLower(), "UserID", id, SessionMgr.DBName);
                    if (intExist > 0)
                    {
                        lblval.Text = "Code Already Exists";
                        lblval.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                        return;

                    }

                    ////user name duplication checking
                    //intExist = bllUser.GetDuplicateExists(SessionMgr.CompanyID, "usersetup", "UserName", txtUserName.Text.Trim().ToLower(), "UserID", id, SessionMgr.DBName);
                    //if (intExist > 0)
                    //{
                    //    lblval.Text = "Login Name Already Exists";
                    //    lblval.Visible = true;
                    //    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                    //    return;

                    //}

                //}
                BEUser br = new BEUser();
                br.UserCode = txtusercode.Text.Trim();
                br.UserName = txtUserName.Text.Trim();
                br.Password = txtPasswords.Text.Trim();
                br.usertype = Convert.ToInt32(cmbusertype.SelectedValue);
                br.UserID = id;
                br.Photo = Image;
                br.LoginName = txtLoginName.Text.Trim();
                br.MobileNo = txtmobile.Text.Trim();
                br.EmailID = txtmailid.Text.Trim();
                br.createdby = SessionMgr.UserID;
                br.modifiedby = SessionMgr.UserID;
                int insertorupdate = bllUser.InsertOrUpdateRecord(br, SessionMgr.DBName);
                if (insertorupdate > 0)
                {
                    //AlertMsg("Saved Successfully");
                    Response.Redirect("~/Masters/UserList.aspx", false);

                }

            }
            catch (Exception ex)
            {

                BeCommon.FormName = "UserList";
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
            Response.Redirect("~/Masters/UserList.aspx");
        }
    }
}