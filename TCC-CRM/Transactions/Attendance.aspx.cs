using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Bussiness;
using DevExpress.Web;
using TrackIT.ClassModules;
using ClosedXML.Excel;


namespace TCC_CRM.Transactions
{
    public partial class Attendance : System.Web.UI.Page
    {
        public BLLCommon m_BLLCommon = new BLLCommon();


        //if (!IsPostBack)
        //{
        //    if (!string.IsNullOrEmpty(SessionMgr.DBName))
        //    {
        //    }
        //    else
        //    {
        //        Response.Redirect("~/Login.aspx");
        //    }
        //}




        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //loaddefaultvalues();
                    DateTime dt = DateTime.Now;
                    ASPxDateEdit1.Value = dt;
                    ASPxDateEdit2.Value = dt;

                    LoadDropdown();
                }
                bindgridview();
            }
            catch (Exception ex)
            {
                try
                {

                }
                catch (Exception ew)
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        private void LoadDropdown()
        {
            DataTable dds = m_BLLCommon.GetEngineerDetails(SessionMgr.DBName);
            ASPxListBox cuslistss = ((ASPxListBox)ASPxDropDownEdit_Product.FindControl("ListBoxLocation"));
            cuslistss.DataSource = dds;
            cuslistss.TextField = "Name";
            cuslistss.ValueField = "Value";
            cuslistss.DataBind();
        }

        protected void Image_Init(object sender, EventArgs e)
        {
            ASPxImage image = sender as ASPxImage;
            GridViewDataItemTemplateContainer container = image.NamingContainer as GridViewDataItemTemplateContainer;
            string base64str = ASPxGridView1.GetRowValues(container.VisibleIndex, "IN_Img").ToString();
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64str)))
            {
                byte[] byteImage = ms.ToArray();
                image.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }
        }

        protected void Image_Outit(object sender, EventArgs e)
        {
            ASPxImage image = sender as ASPxImage;
            GridViewDataItemTemplateContainer container = image.NamingContainer as GridViewDataItemTemplateContainer;
            string base64str = ASPxGridView1.GetRowValues(container.VisibleIndex, "OUT_Img").ToString();
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64str)))
            {
                byte[] byteImage = ms.ToArray();
                image.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }
        }

        //private void loaddefaultvalues()
        //{
        //    DataTable dt = new DataTable();
        //    dt = bllcommon.GetDropDownValues("EmployeeName", "EmployeeID", "EmployeeMaster ", string.Empty);
        //    ddlemp.DataSource = dt;
        //    ddlemp.DataTextField = "Name";
        //    ddlemp.DataValueField = "Value";
        //    ddlemp.DataBind();
        //    ddlemp.Items.Insert(0, "Please Select");
        //}

        private DataTable getData()
        {
            DataTable dt = new DataTable();
            string StartTime = Convert.ToDateTime(ASPxDateEdit1.Value).ToString("yyyy-MM-dd");
            string FinishTime = Convert.ToDateTime(ASPxDateEdit2.Value).ToString("yyyy-MM-dd");
            //string userid = ddlemp.SelectedValue.ToString();
            string userid = string.Empty;
            ASPxListBox Locationlist = ((ASPxListBox)ASPxDropDownEdit_Product.FindControl("ListBoxLocation"));
            if (Locationlist.SelectedItems.Count > 0)
            {

                for (int i = 0; i < Locationlist.SelectedItems.Count; i++)
                {

                    int strselecteditems = Convert.ToInt32(Locationlist.SelectedItems[i].Value.ToString());
                    if (!string.IsNullOrEmpty(userid))
                    {
                        userid = userid + "," + strselecteditems;
                    }
                    else
                    {
                        userid = strselecteditems.ToString();
                    }
                }
            }
            else
            {
                userid = string.Empty;
            }

            dt = m_BLLCommon.GetAttendanceList(StartTime, FinishTime, userid, SessionMgr.DBName);
            return dt;

        }

        private void bindgridview()
        {



            DataTable dt = getData();
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
            DataTable resultdt = getData();
            resultdt.Columns.Remove("IN_Img");
            resultdt.Columns.Remove("OUT_Img");
            string Filename = "AttendanceList";
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
                {
                    dt.Columns.Add(dataColumn.FieldName);
                }
            }
          //  dt.Columns.Remove("RowId");
            //dt.Columns.Remove("Column1");



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
    }
}
