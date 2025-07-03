using CRM.Artifacts;
using CRM.Artifacts.Security;
using CRM.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackIT.ClassModules;

namespace TCC_CRM
{
    public partial class Login : System.Web.UI.Page
    {
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OnBtnLoginClick(Object sender, EventArgs e)
        {
            try
            {
                int userId = SessionMgr.UserID;
                string Photo = SessionMgr.Photo;
                SessionMgr.ClientCode = txtclientcode.Value.Trim();
                if (SessionMgr.UserID == 0)
                {
                    if (ValidateUser())
                    {
                       // Response.Redirect("~/Transactions/Dashboard.aspx", false);
                        Response.Redirect("~/Transactions/TicketList.aspx", false);
                    }
                }
                else
                {
                    Response.Redirect("Logout.aspx", false);
                   // Response.Redirect("Session.aspx", false);
                }
            }
            catch (Exception ex)
            {
               
                BeCommon.FormName = "LoginPage";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BLLCommon.UpdateLog(BeCommon,SessionMgr.ClientCode);
            }

        }

        private bool ValidateUser()
        {
            bool myResult = true;
            //if (string.IsNullOrEmpty(cmbCompany.SelectedValue))
            //{
            //    txtnewerroe.Visible = true;
            //    txtnewerroe.InnerHtml = "Invalid: Please Select Company";
            //    return myResult;
            //}

            if (txtclientcode.Value.Trim() == string.Empty)
            {
                txtnewerroe.Visible = true;
                txtnewerroe.InnerHtml = "Please Enter ClientCode";
                myResult = false;
                return myResult;
            }
            if (txtusername.Value.Trim() == string.Empty)
            {
                txtnewerroe.Visible = true;
                txtnewerroe.InnerHtml = "Please Enter UserName";
                myResult = false;
                return myResult;
            }
            if (txtpass.Value.Trim() == string.Empty)
            {
                txtnewerroe.Visible = true;
                txtnewerroe.InnerHtml = "Please Enter Password";
                myResult = false;
                return myResult;
            }

            if ((txtclientcode.Value.Trim() != string.Empty) && (txtusername.Value.Trim() != string.Empty) && (txtpass.Value.Trim() != string.Empty))
            {
                myResult = true;
                int iResult = this.IsValidLogin();
                if (iResult == 0)
                {
                    txtnewerroe.Visible = true;
                    txtnewerroe.InnerHtml = "Invalid : Login Fail";
                    myResult = false;
                    return myResult;
                }
                else if (iResult == 2)
                {
                    txtnewerroe.Visible = true;
                    txtnewerroe.InnerHtml = "Invalid : Non-Active User";
                    myResult = false;
                    return myResult;
                }
                else if (iResult == 3)
                {
                    txtnewerroe.Visible = true;
                    txtnewerroe.InnerHtml = "Invalid : ClientCode is not Registered";
                    myResult = false;
                    return myResult;
                }
                else if (iResult == -1)
                {
                    txtnewerroe.Visible = true;
                    txtnewerroe.InnerHtml = "Invalid : Organization Blocked by Admin.";
                    myResult = false;
                    return myResult;
                }
            }
            return myResult;
        }
     
        private int IsValidLogin()
        {
            int myResult = 0;

            BLLUser myBLLUser = new BLLUser();
            BEUsers myBEUsers = new BEUsers();
            myBEUsers.CompanyID = 1;
            myBEUsers.UserName = txtusername.Value;
            myBEUsers.UserPassword = txtpass.Value;
            myBEUsers.RegisteredClient = myBLLUser.ValidateRegisteredClient(SessionMgr.ClientCode);
            if (myBEUsers.RegisteredClient != "failed")
            {
                SessionMgr.DBName = myBEUsers.RegisteredClient.ToString();
                myBEUsers = myBLLUser.GetLoginInfo(myBEUsers, SessionMgr.DBName);

                if (myBEUsers != null)
                {
                    SessionMgr.CompanyID = Convert.ToInt32(myBEUsers.CompanyID);
                    SessionMgr.UserID = Convert.ToInt32(myBEUsers.UserID);
                    SessionMgr.UserName = myBEUsers.UserName.ToString();
                    SessionMgr.CompanyName = myBEUsers.CompanyName.ToString();
                    SessionMgr.Photo = myBEUsers.Photo.ToString();
                    SessionMgr.CompanyLogo1 = myBEUsers.companylogo.ToString();
                    SessionMgr.CompanyLogo2 = myBEUsers.companylogo2.ToString();
                    SessionMgr.CompanyAddress = myBEUsers.Address.ToString();
                    SessionMgr.SMTPUserName = myBEUsers.SMTPUserName.ToString();
                    SessionMgr.SMTPPassword = myBEUsers.SMTPPassword.ToString();
                    SessionMgr.SMTPHost = myBEUsers.SMTPHost.ToString();
                    SessionMgr.SMTPPort = myBEUsers.SMTPPort.ToString();

                    myResult = 1;
                }
                else
                {
                    SessionMgr.UserID = 0;
                    SessionMgr.UserName = "";
                    myResult = 0;
                }
            }
            else
            {
                SessionMgr.UserID = 0;
                SessionMgr.UserName = "";
                myResult = 3;
            }
            //if (txtUserName.Value.ToLower() == "admin" && txtPassword.Value == "admin")
            //{
            //    myResult = 1;
            //}
            //else
            //{
            //    myResult = 0;
            //}
            return myResult;
        
        }
    }
}