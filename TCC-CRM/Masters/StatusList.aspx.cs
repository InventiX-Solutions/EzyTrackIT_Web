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
    public partial class StatusList : System.Web.UI.Page
    {
        public BLLStatus bllStatus = new BLLStatus();
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionMgr.DBName))
                {
                    getstatuslist();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
     private void getstatuslist()
        {
            DataTable dt = new DataTable();
            dt = bllStatus.GetStatusList(SessionMgr.DBName);
            dt.Columns[1].ColumnName = "Code";
            dt.Columns[2].ColumnName = "Name";
          dt.AcceptChanges();
            gvStatuslist.DataSource = dt;
            gvStatuslist.DataBind();
         }
     protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
     {
         e.Row.Cells[4].Visible = false;
         e.Row.Cells[1].Visible = false;
         e.Row.Cells[2].Visible = false;

     }
         protected void Display(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvStatuslist.Rows[rowIndex];

            int StatusID = Convert.ToInt32((row.FindControl("lblStatusID") as Label).Text);
            //string lblid = lblStatusID.Text;
            Response.Redirect("~/Masters/Status.aspx?StatusID=" + StatusID, false);

        }
        protected void Delete(object sender, EventArgs e)
        {
            try
            
            {
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                GridViewRow row = gvStatuslist.Rows[rowIndex];
                int StatusID = Convert.ToInt32((row.FindControl("lblStatusID") as Label).Text);
               
                int deleteStatus = bllStatus.DeleteStatus(StatusID, SessionMgr.UserID, SessionMgr.DBName);
                if (deleteStatus > 0)
                {
                    AlertMsg("Record Deleted");
                    getstatuslist();
                }
               // getbrandlist();
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

            { ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + Msg + "');", true); }
        }
        #endregion
    }
}