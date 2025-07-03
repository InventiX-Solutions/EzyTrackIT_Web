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
using ClosedXML.Excel;
using System.IO;

namespace TCC_CRM.Masters
{
    public partial class CustomerBranchList : System.Web.UI.Page
    {

        public BLLCustomerBranch bllCustomer = new BLLCustomerBranch();
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionMgr.DBName))
                {
                    getcustomerlist();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
           
        }

        private void getcustomerlist()
        {
            DataTable dt = new DataTable();
            dt = bllCustomer.GetCustomerList(SessionMgr.DBName);
         
            dt.Columns[1].ColumnName = "Code";
            dt.Columns[2].ColumnName = "Name";
            dt.Columns[3].ColumnName = "CustomerName";
            dt.Columns[4].ColumnName = "AddressLine1";
            dt.Columns[5].ColumnName = "AddressLine2";
            dt.Columns[6].ColumnName = "ContactPerson";
            dt.Columns[7].ColumnName = "PhoneNo";
            dt.AcceptChanges();
            if (dt.Rows.Count > 0)
            {
                gvCustomerlist.DataSource = dt;
                gvCustomerlist.DataBind();
                btnnew.Visible = false;

            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                gvCustomerlist.DataSource = dt;
                gvCustomerlist.DataBind();
                int TotalColumns = gvCustomerlist.Rows[0].Cells.Count;
                gvCustomerlist.Rows[0].Cells.Clear();
                gvCustomerlist.Rows[0].Cells.Add(new TableCell());
                gvCustomerlist.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                gvCustomerlist.Rows[0].Cells[0].Text = "No Record Found";


                btnnew.Visible = true;
            }
           
        }
       protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[9].Visible = false;
            //e.Row.Cells[13].Visible = false;
            //e.Row.Cells[14].Visible = false;

        }

       protected void btn_AddnewCustomer(object sender, EventArgs e)
       {
           int CustomerID = 0;
           Response.Redirect("~/Masters/CustomerBranch.aspx?customer_branch_id=" + CustomerID, false);
       }
        protected void Display(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvCustomerlist.Rows[rowIndex];

            int CustomerID = Convert.ToInt32((row.FindControl("lblcustomer_id") as Label).Text);
            //string lblid = lblcustomer_id.Text;
            Response.Redirect("~/Masters/CustomerBranch.aspx?customer_branch_id=" + CustomerID, false);

        }
        protected void Delete(object sender, EventArgs e)
        {
            try
            
            {
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                GridViewRow row = gvCustomerlist.Rows[rowIndex];
                int CustomerID = Convert.ToInt32((row.FindControl("lblcustomer_id") as Label).Text);
               
                int deletecustomer = bllCustomer.DeleteCustomer(CustomerID, SessionMgr.UserID, SessionMgr.DBName);
                if (deletecustomer > 0)
                {
                    AlertMsg("Record Deleted");
                }
                getcustomerlist();
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

        protected void templatebtn_ServerClick(object sender, EventArgs e)
        {
           

            string columns = "CustomerBranchCode,CustomerBranchName,CustomerName,AddressLine_1,AddressLine_2,ContactPerson,PhoneNo,ServiceLocation,Remarks";

            string[] cols = columns.Split(',');
            DataTable dt = new DataTable();
            foreach (string col in cols)
            {
                dt.Columns.Add(col, typeof(String));
            }

            string Filename = "Customer_Branch_Import_Template";
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add(dt, "Sheet1");
            ws.Tables.FirstOrDefault().ShowAutoFilter = false;
            ws.Columns().AdjustToContents();
            ws.Tables.FirstOrDefault().ShowRowStripes = false;
            //wb.SaveAs("E:\\Error_Download.xlsx");
            string datestring = DateTime.Now.ToString("dd-MM-yyyy");
            string myName = Server.UrlEncode(Filename + ".xlsx");
            MemoryStream stream = GetStream(wb);// The method is defined below
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment; filename=" + myName);
            Response.ContentType = "application/vnd.ms-excel";
            Response.BinaryWrite(stream.ToArray());
            Response.End();
        }

        public MemoryStream GetStream(XLWorkbook excelWorkbook)
        {
            MemoryStream fs = new MemoryStream();
            excelWorkbook.SaveAs(fs);
            fs.Position = 0;
            return fs;
        }

        #region Alerts
        private void AlertMsg(string Msg)
        {

            { ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('"+Msg+"');", true); }
        }
        #endregion

        protected void btnimport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~//Masters/ImportCustomerBranch.aspx",false);
        }
    }
}