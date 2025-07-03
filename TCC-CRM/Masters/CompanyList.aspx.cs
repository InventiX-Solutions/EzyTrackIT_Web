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
    public partial class CompanyList : System.Web.UI.Page
    {
        public BLLCompany bllCompany = new BLLCompany();
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionMgr.DBName))
                {
                    GetCompanylist();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        private void GetCompanylist()
        {
            DataTable dt = new DataTable();
            dt = bllCompany.GetCompanyList(SessionMgr.DBName);

            dt.Columns[1].ColumnName = "Code";
            dt.Columns[2].ColumnName = "Name";
            dt.Columns[3].ColumnName = "AddressLine1";
            dt.Columns[4].ColumnName = "AddressLine2";
           
            dt.AcceptChanges();
            if (dt.Rows.Count > 0)
            {
                gvCompanylist.DataSource = dt;
                gvCompanylist.DataBind();
                btnnew.Visible = false;

            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                gvCompanylist.DataSource = dt;
                gvCompanylist.DataBind();
                int TotalColumns = gvCompanylist.Rows[0].Cells.Count;
                gvCompanylist.Rows[0].Cells.Clear();
                gvCompanylist.Rows[0].Cells.Add(new TableCell());
                gvCompanylist.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                gvCompanylist.Rows[0].Cells[0].Text = "No Record Found";


                btnnew.Visible = true;
            }
           
        }
        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false; 
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;
            e.Row.Cells[17].Visible = false;
            e.Row.Cells[18].Visible = false;
            e.Row.Cells[19].Visible = false;
            e.Row.Cells[20].Visible = false;
            //e.Row.Cells[21].Visible = false;
            //e.Row.Cells[22].Visible = false;

        }

        protected void Display(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvCompanylist.Rows[rowIndex];

            int CompanyID = Convert.ToInt32((row.FindControl("lblcompany_id") as Label).Text);
            //string lblid = lblcustomer_id.Text;
            Response.Redirect("~/Masters/Company.aspx?CompanyID=" + CompanyID, false);

        }
        protected void Delete(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                GridViewRow row = gvCompanylist.Rows[rowIndex];
                int CompanyID = Convert.ToInt32((row.FindControl("lblcompany_id") as Label).Text);

                int deleteCompany = bllCompany.DeleteCompany(CompanyID, SessionMgr.UserID, SessionMgr.DBName);
                if (deleteCompany > 0)
                {
                    AlertMsg("Record Deleted");
                }
                GetCompanylist();
            }
            catch (Exception ex)
            {
                AlertMsg(ex.Message);
            }
        }
        protected void btn_AddnewCompany(object sender, EventArgs e)
        {
            int CompanyID = 0;
            Response.Redirect("~/Masters/Company.aspx?CompanyID=" + CompanyID, false);
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