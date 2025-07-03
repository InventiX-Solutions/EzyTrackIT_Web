using CRM.Artifacts;
using CRM.Artifacts.Masters;
using CRM.Bussiness;
using CRM.Bussiness.Masters;
using DevExpress.Web;
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
using System.Globalization;
using ClosedXML.Excel;

namespace TCC_CRM.Transactions
{
    public partial class BranchWiseReport : System.Web.UI.Page

    {
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BLLTicket bllTicket = new BLLTicket();

        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    DateTime dt = DateTime.Now;
                    ASPxDateEdit1.Value = dt;
                    ASPxDateEdit2.Value = dt;

                    LoadDropdown();
                }
                bindgridview();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void LoadDropdown()
        {

            try
            {

                DataTable dtLoadList = bllTicket.GetCustomerlist(SessionMgr.DBName);
                cmbcustomer.DataSource = dtLoadList;
                cmbcustomer.DataTextField = "customer_Name";
                cmbcustomer.DataValueField = "customer_ID";
                cmbcustomer.DataBind();
                cmbcustomer.Items.Insert(0, "Please Select");
            }
            catch (Exception ex)
            {

            }

        }

        protected void Image_Init(object sender, EventArgs e)
        {
            ASPxImage image = sender as ASPxImage;
            GridViewDataItemTemplateContainer container = image.NamingContainer as GridViewDataItemTemplateContainer;
            string base64str = ASPxGridView1.GetRowValues(container.VisibleIndex, "Intime_img").ToString();
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64str)))
            {
                byte[] byteImage = ms.ToArray();
                image.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }
        }

        private void bindgridview()
        {
            string StartTime = Convert.ToDateTime(ASPxDateEdit1.Value).ToString("yyyy-MM-dd");
            string EndTime = Convert.ToDateTime(ASPxDateEdit2.Value).ToString("yyyy-MM-dd");
            //string StartTime = Convert.ToDateTime(ASPxDateEdit1.Value).ToString("dd/MM/yyyy");
            //string EndTime = Convert.ToDateTime(ASPxDateEdit2.Value).ToString("dd/MM/yyyy");

            //if (String.IsNullOrEmpty(cmbcustomer.SelectedValue) || cmbcustomer.SelectedValue == "0")
            //{
            //    return;
            //}

            //if (String.IsNullOrEmpty(cmbbranch.SelectedValue) || cmbbranch.SelectedValue == "0")
            //{
            //    return;
            //}

            DataTable dt = m_BLLCommon.GetCustomerBranchList(StartTime, EndTime, cmbcustomer.SelectedValue, cmbbranch.SelectedValue, SessionMgr.DBName);
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.KeyFieldName = "RowId";
            ASPxGridView1.DataBind();
        }

        protected void btn_go_click(object sender, EventArgs e)
        {
            bindgridview();
        }

        protected void excelexport_Click(object sender, EventArgs e)
        {
            DataTable resultdt = GetDataView(ASPxGridView1);
            string Filename = "TimeAnalysis";
            var wb = new XLWorkbook();
            wb.Worksheets.Add(resultdt, "Sheet1");
            //wb.SaveAs("E:\\Error_Download.xlsx");
            string datestring = DateTime.Now.ToString("dd-MM-yyyy");
            string myName = Server.UrlEncode(Filename + "_" + datestring + ".xlsx");
            MemoryStream stream = GetStream(wb);// The method is defined below
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
            "attachment; filename=" + myName);
            Response.ContentType = "application/vnd.ms-excel";
            Response.BinaryWrite(stream.ToArray());
            Response.End();
        }

        private DataTable GetDataView(ASPxGridView grid)
        {
            DataTable dt = new DataTable();
            foreach (GridViewColumn col in grid.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = grid.GetRowValues(i, col.ColumnName);
            }
            return dt;
        }

        public MemoryStream GetStream(XLWorkbook excelWorkbook)
        {
            MemoryStream fs = new MemoryStream();
            excelWorkbook.SaveAs(fs);
            fs.Position = 0;
            return fs;
        }

        protected void cmbcustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CustomerID = Convert.ToInt32(cmbcustomer.SelectedValue);

            DataTable dtLoadList = bllTicket.GetCustomerdetails(CustomerID, SessionMgr.DBName);

            cmbbranch.DataSource = dtLoadList;
            cmbbranch.DataTextField = "Name";
            cmbbranch.DataValueField = "Value";
            cmbbranch.DataBind();
            cmbbranch.Items.Insert(0, new ListItem("Please Select", "0"));

        }

        protected void cmbbranch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }




}