using CRM.Artifacts;
using CRM.Artifacts.Masters;
using CRM.Bussiness;
using CRM.Bussiness.Masters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackIT.ClassModules;

namespace TCC_CRM.Masters
{
    public partial class UserRoles : System.Web.UI.Page
    {
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();
        public BLLUserMaster bllUser = new BLLUserMaster();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionMgr.DBName))
                {
                    hdntypeid.Value = Request.QueryString["TypeID"].ToString();
                    LoadDefaultVaues();
                    GetRoleDetails(Convert.ToInt32((hdntypeid.Value)));
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }

            }
        }

        private void LoadDefaultVaues()
        {
 
        }
        private void GetRoleDetails(int TypeID)
        {
            DataTable dt = bllUser.GetUserRoles(TypeID, SessionMgr.DBName);
            txtusertype.Text = dt.Rows[0]["usertype"].ToString();
            gvRoleAccess.DataSource = dt;
            gvRoleAccess.DataBind();
        }
        protected void OngvRoleAccessRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    CheckBox objchkViewSelectAll = (CheckBox)e.Row.FindControl("htView");
                    objchkViewSelectAll.Text = "View";
                    objchkViewSelectAll.Attributes.Add("onclick", "return fnViewSelectAll('" + objchkViewSelectAll.ClientID + "')");
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox objchkView = (CheckBox)e.Row.FindControl("chkView");
                        objchkView.Attributes.Add("onclick", "return UnSelectCheckboxes('" + objchkView.ClientID + "')");
                }
            }
            catch (Exception ex)
            {
                BeCommon.FormName = "UserRoles";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BLLCommon.UpdateLog(BeCommon, SessionMgr.DBName);
            }
        }
    }
}