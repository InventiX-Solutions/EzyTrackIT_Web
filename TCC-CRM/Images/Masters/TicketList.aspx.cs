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
    public partial class TicketList : System.Web.UI.Page
    {
        public BLLTicket bllTicket = new BLLTicket();
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();
        protected void Page_Load(object sender, EventArgs e)
        {
            getTicketlist();
        }
        private void getTicketlist()
        {
            DataTable dt = new DataTable();
            dt = bllTicket.GetTicketList();
            dt.Columns[1].ColumnName = "TicketNo";
            dt.Columns[2].ColumnName = "Date";
            dt.Columns[3].ColumnName = "Customer Name";
            dt.AcceptChanges();
            if (dt.Rows.Count > 0)
            {
                gvTicketlist.DataSource = dt;
                gvTicketlist.DataBind();
                btnnew.Visible = false;

            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                gvTicketlist.DataSource = dt;
                gvTicketlist.DataBind();
                int TotalColumns = gvTicketlist.Rows[0].Cells.Count;
                gvTicketlist.Rows[0].Cells.Clear();
                gvTicketlist.Rows[0].Cells.Add(new TableCell());
                gvTicketlist.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                gvTicketlist.Rows[0].Cells[0].Text = "No Record Found";


                btnnew.Visible = true;
            }
        }
              protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
         {
             e.Row.Cells[5].Visible = false;
             e.Row.Cells[9].Visible = false;
             e.Row.Cells[10].Visible = false;
             e.Row.Cells[11].Visible = false;
             e.Row.Cells[12].Visible = false;
             e.Row.Cells[13].Visible = false;
             e.Row.Cells[14].Visible = false;
             e.Row.Cells[15].Visible = false;
             e.Row.Cells[16].Visible = false;
         }

         protected void btn_AddnewTicket(object sender, EventArgs e)
         {
             int TicketID = 0;
             Response.Redirect("~/Masters/Tickets.aspx?TicketID=" + TicketID, false);
         }
         protected void Display(object sender, EventArgs e)
         {
             int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
             GridViewRow row = gvTicketlist.Rows[rowIndex];

             int TicketID = Convert.ToInt32((row.FindControl("lblTicketID") as Label).Text);
             string TicketNo = (row.FindControl("lbltno") as Label).Text;
             string Date = (row.FindControl("lbldate") as Label).Text;
             string CustomerName = (row.FindControl("lblcname") as Label).Text;
           //  string lblmodelname

             //string lblid = lblcustomer_id.Text;
             Response.Redirect("~/Masters/Tickets.aspx?TicketID=" + TicketID, false);

         }
         protected void Delete(object sender, EventArgs e)
         {
             try
             {
                 int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                 GridViewRow row = gvTicketlist.Rows[rowIndex];
                 int TicketID = Convert.ToInt32((row.FindControl("lblTicketID") as Label).Text);

                 int deleteTicket = bllTicket.DeleteTicket(TicketID, SessionMgr.UserID);
                 if (deleteTicket > 0)
                 {
                     AlertMsg("Record Deleted");
                 }
                 getTicketlist();
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