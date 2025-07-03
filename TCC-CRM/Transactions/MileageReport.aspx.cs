using ClosedXML.Excel;
using CRM.Bussiness;
using CRM.Bussiness.Masters;
using DevExpress.Web;
using System;
using System.Data;
using System.IO;
using TrackIT.ClassModules;

namespace TCC_CRM.Transactions
{
    public partial class MileageReport : System.Web.UI.Page
    {
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BLLTicket bllTicket = new BLLTicket();
        public BLLEngineer bllengineer = new BLLEngineer();

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
            DataTable dds = m_BLLCommon.GetEngineerDetails(SessionMgr.DBName);
            ASPxListBox cuslistss = ((ASPxListBox)ASPxDropDownEdit_Product.FindControl("ListBoxLocation"));
            cuslistss.DataSource = dds;
            cuslistss.TextField = "Name";
            cuslistss.ValueField = "Value";
            cuslistss.DataBind();

            DataTable dtLoadvehicleList = bllengineer.Getvehiclelist(SessionMgr.DBName);
            ASPxListBox ListBoxVehicle = ((ASPxListBox)ASPxDropDownEdit_Vehicle.FindControl("ListBoxVehicle"));
            ListBoxVehicle.DataSource = dtLoadvehicleList;
            ListBoxVehicle.TextField = "Vehicle"; // Use the concatenated field
            ListBoxVehicle.ValueField = "vehicleID"; // Use vehicleID as the value
            ListBoxVehicle.DataBind();
        }

        private void bindgridview()
        {
            string StartTime = Convert.ToDateTime(ASPxDateEdit1.Value).ToString("yyyy-MM-dd");
            string EndTime = Convert.ToDateTime(ASPxDateEdit2.Value).ToString("yyyy-MM-dd");
            //string StartTime = Convert.ToDateTime(ASPxDateEdit1.Value).ToString("dd/MM/yyyy");
            //string EndTime = Convert.ToDateTime(ASPxDateEdit2.Value).ToString("dd/MM/yyyy");
            string strVehicleID = string.Empty;
            string strEngineerID = string.Empty;

            ASPxListBox ListBoxVehicle = ((ASPxListBox)ASPxDropDownEdit_Vehicle.FindControl("ListBoxVehicle"));
            if (ListBoxVehicle.SelectedItems.Count > 0)
            {
                for (int i = 0; i < ListBoxVehicle.SelectedItems.Count; i++)
                {
                    int strselecteditems = Convert.ToInt32(ListBoxVehicle.SelectedItems[i].Value.ToString());
                    if (!string.IsNullOrEmpty(strVehicleID))
                    {
                        strVehicleID = strVehicleID + "','" + strselecteditems;
                    }
                    else
                    {
                        strVehicleID = strselecteditems.ToString();
                    }
                }
            }
            else
            {
                strVehicleID = string.Empty;
            }

            ASPxListBox Locationlist = ((ASPxListBox)ASPxDropDownEdit_Product.FindControl("ListBoxLocation"));
            if (Locationlist.SelectedItems.Count > 0)
            {
                for (int i = 0; i < Locationlist.SelectedItems.Count; i++)
                {
                    int strselecteditems = Convert.ToInt32(Locationlist.SelectedItems[i].Value.ToString());
                    if (!string.IsNullOrEmpty(strEngineerID))
                    {
                        strEngineerID = strEngineerID + "','" + strselecteditems;
                    }
                    else
                    {
                        strEngineerID = strselecteditems.ToString();
                    }
                }
            }
            else
            {
                strEngineerID = string.Empty;
            }

            DataTable dt = m_BLLCommon.GetVehicleMileageList(StartTime, EndTime, strEngineerID, strVehicleID, SessionMgr.DBName);
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
            string Filename = "VehicleMileage";
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
    }
}