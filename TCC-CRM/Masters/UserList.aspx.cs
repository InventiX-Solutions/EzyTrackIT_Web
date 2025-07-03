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
    public partial class UserList : System.Web.UI.Page
    {
        public BLLUserMaster bllUser = new BLLUserMaster();
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionMgr.DBName))
                {
                    GetUserlist();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }

        }
         private void GetUserlist()
        {
            DataTable dt = new DataTable();
            dt = bllUser.GetUserlist(SessionMgr.DBName);

            dt.Columns[1].ColumnName = "Code";
            dt.Columns[2].ColumnName = "Name";
          
            dt.AcceptChanges();
            if (dt.Rows.Count > 0)
            {
                gvuserlist.DataSource = dt;
                gvuserlist.DataBind();
                btnnew.Visible = false;

            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                gvuserlist.DataSource = dt;
                gvuserlist.DataBind();
                int TotalColumns = gvuserlist.Rows[0].Cells.Count;
                gvuserlist.Rows[0].Cells.Clear();
                gvuserlist.Rows[0].Cells.Add(new TableCell());
                gvuserlist.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                gvuserlist.Rows[0].Cells[0].Text = "No Record Found";


                btnnew.Visible = true;
            }
           
        }
       protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[4].Visible = false;

        }

       protected void btn_AddnewCustomer(object sender, EventArgs e)
       {
           int UserID = 0;
           Response.Redirect("~/Masters/User.aspx?UserID=" + UserID, false);
       }
        protected void Display(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvuserlist.Rows[rowIndex];

            int UserID = Convert.ToInt32((row.FindControl("lbluser_id") as Label).Text);
            //string lblid = lblcustomer_id.Text;
            Response.Redirect("~/Masters/User.aspx?UserID=" + UserID, false);

        }
        protected void Delete(object sender, EventArgs e)
        {
            try
            
            {
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                GridViewRow row = gvuserlist.Rows[rowIndex];
                int UserID = Convert.ToInt32((row.FindControl("lbluser_id") as Label).Text);

                int deleteuser = bllUser.DeleteUser(UserID, SessionMgr.UserID, SessionMgr.DBName);
                if (deleteuser > 0)
                {
                    AlertMsg("Record Deleted");
                }
                GetUserlist();
            }
            catch (Exception ex)
            {
                AlertMsg(ex.Message);
            }
        }
        protected void GridView_PreRender(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;

            if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                || (gv.ShowHeaderWhenEmpty == true))
            {
                //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
                gv.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            if (gv.ShowFooter == true && gv.Rows.Count > 0)
            {
                //Force GridView to use <tfoot> instead of <tbody> - 11/03/2013 - MCR.
                gv.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }
        #region Alerts
        private void AlertMsg(string Msg)
        {

           ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('"+Msg+"');", true);
        }
        #endregion
    }
}
   