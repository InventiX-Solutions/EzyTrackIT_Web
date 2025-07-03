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
    public partial class CustomerList : System.Web.UI.Page
    {
        public BLLCustomer bllLocation = new BLLCustomer();
        // public BLLCustomer bllCustomer = new BLLCustomer();
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionMgr.ClientCode))
                {
                    lblval.Text = string.Empty;
                    lblval.Visible = false;

                    GetCustomerList();
                    //DerializeCustomerDataTable();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }

        }
        //private void DerializeCustomerDataTable()
        //{
        //    try
        //    {

        //        DataTable dtLoadList = bllCustomer.GetCustomerList(SessionMgr.DBName);
        //        ddlCustomer.DataSource = dtLoadList;
        //        ddlCustomer.DataTextField = "customer_Name";
        //        ddlCustomer.DataValueField = "customer_ID";
        //        ddlCustomer.DataBind();
        //        ddlCustomer.Items.Insert(0, "Please Select");
        //    }
        //    catch (Exception ex)
        //    {
        //        AlertMsg(ex.Message);
        //    }

        //}
        private void GetCustomerList()
        {
            DataTable dt = new DataTable();
            dt = bllLocation.GetCustomerList(SessionMgr.DBName);

            dt.Columns[1].ColumnName = "Code";
            dt.Columns[2].ColumnName = "Name";
            dt.Columns[3].ColumnName = "Email";
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

            e.Row.Cells[5].Visible = false;
            //e.Row.Cells[5].Visible = false;
        }

        //protected void btn_AddLocation(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/Masters/Location.aspx", false);
        //}
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int intExist = 0;
                int id = 0;
                string lblid = lblCustomerid.Text;
                // int Customerid = 0;
                //string selprd = hdnprd.Value.ToString();

                //if (string.IsNullOrEmpty(selprd) || selprd == "Please Select")
                //{
                //    lblval.Text = "Please Select a Customer";
                //    lblval.Visible = true;
                //    lblval.ForeColor = System.Drawing.Color.Red;
                //    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                //    return;
                //}
                //else
                //{
                //    Customerid = Convert.ToInt32(selprd);
                //}
                if (!string.IsNullOrEmpty(lblid))
                {
                    id = Convert.ToInt32(lblid);
                }
                //if (ddlCustomer.SelectedValue.ToString() == "0")
                //{
                //    lblval.Text = "Please Select a Customer";
                //    lblval.Visible = true;
                //    lblval.ForeColor = System.Drawing.Color.Red;
                //    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                //    return;
                //}
                //if (id == 0)
                //{
                intExist = bllLocation.GetDuplicateExists(SessionMgr.CompanyID, "customers", "customer_Code", txtbcode.Text.Trim().ToLower(), "customer_ID", id, SessionMgr.DBName);
                if (intExist > 0)
                {
                    lblval.Text = "Code Already Exists";
                    lblval.Visible = true;
                    lblval.ForeColor = System.Drawing.Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                    //  ddlCustomer.SelectedValue = Customerid.ToString();
                    return;

                }
                //}//Commented by Velu on 19-02-2020, Edit mode also must check the code duplicate

                BECustomer br = new BECustomer();
                br.customer_Code = txtbcode.Text.Trim();
                br.customer_Name = txtbname.Text.Trim();
                br.EmailId = txtbemailid.Text.Trim();
                //    br.CustomerID = Customerid;
                br.customer_ID = id;
                br.createdby = SessionMgr.UserID;
                br.modifiedby = SessionMgr.UserID;
                int insertorupdate = bllLocation.InsertOrUpdateRecord(br, SessionMgr.DBName);
                if (insertorupdate > 0)
                {
                    AlertMsg("Saved Successfully");

                }
                Response.Redirect("~/Masters/CustomerList.aspx?Saved=" + insertorupdate, false);
            }
            catch (Exception ex)
            {

                BeCommon.FormName = "CustomerList";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BLLCommon.UpdateLog(BeCommon, SessionMgr.DBName);
            }

        }
        protected void Display(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvCustomerlist.Rows[rowIndex];

            lblval.Text = string.Empty;

            lblCustomerid.Text = (row.FindControl("lblCustomer_id") as Label).Text;
            txtbcode.Text = (row.FindControl("lblcode") as Label).Text; ;
            txtbname.Text = (row.FindControl("lblname") as Label).Text;
            txtbemailid.Text = (row.FindControl("lblemail") as Label).Text;
            //ddlCustomer.ClearSelection();
            //ddlCustomer.Items.FindByText((row.FindControl("lblcustname") as Label).Text).Selected = true;
            //hdnprd.Value = ddlCustomer.SelectedValue.ToString();
            //ddlCustomer.SelectedItem.Text = (row.FindControl("lblprdname") as Label).Text;

            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "updateselectedval();", true);
        }
        protected void Delete(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                GridViewRow row = gvCustomerlist.Rows[rowIndex];
                lblCustomerid.Text = (row.FindControl("lblCustomer_id") as Label).Text;
                int LocationID = Convert.ToInt32(lblCustomerid.Text);
                int deleteLocation = bllLocation.DeleteLocation(LocationID, SessionMgr.UserID, SessionMgr.DBName);
                if (deleteLocation > 0)
                {
                    AlertMsg("Record Deleted");
                    lblCustomerid.Text = string.Empty;
                }
                GetCustomerList();
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




        public MemoryStream GetStream(XLWorkbook excelWorkbook)
        {
            MemoryStream fs = new MemoryStream();
            excelWorkbook.SaveAs(fs);
            fs.Position = 0;
            return fs;
        }


        protected void btn_import_Click(object sender, EventArgs e)
        {
            Response.Redirect("~//Masters/ImportCustomer.aspx", false);
        }

        protected void btnexcelImportemp_Click(object sender, EventArgs e)
        {
            string columns = "CustomerCode,CustomerName";

            string[] cols = columns.Split(',');
            DataTable dt = new DataTable();
            foreach (string col in cols)
            {
                dt.Columns.Add(col, typeof(String));
            }

            string Filename = "Customer_Import_Template";
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
    }
}