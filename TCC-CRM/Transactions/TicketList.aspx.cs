using ClosedXML.Excel;
using CRM.Artifacts;
using CRM.Artifacts.Masters;
using CRM.Bussiness;
using CRM.Bussiness.Masters;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackIT.ClassModules;
namespace TCC_CRM.Transactions
{
    public partial class TicketList : System.Web.UI.Page
    {
        public BLLTicket bllTicket = new BLLTicket();
        public BLLCommon m_BLLCommon = new BLLCommon();
        CRM.Data.Masters.DALTicket myDALTicket = new CRM.Data.Masters.DALTicket();
        public BECommom BeCommon = new BECommom();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(SessionMgr.DBName))
            {
                string statusIDStr = Request.QueryString["statusid"];

                if (!IsPostBack)
                {
                    DateTime dtFrom = DateTime.Now.AddDays(-7);
                    DateTime dtTO = DateTime.Now;

                    orderfromdt.Value = dtFrom;
                    ordertodt.Value = dtTO;
                    //string dashboardstatus = Request.QueryString["status"];
                    //int status = Convert.ToInt32(dashboardstatus);
                    //if (status !=0 )
                    //{
                    //   // lblStatusTitle.Text = "Showing Tickets with Status: " + status;
                    //    LoadTicketsByStatus(status);
                    //}
                    //if (!string.IsNullOrEmpty(statusIDStr))
                    //{
                    //    // Hide filter if coming from dashboard
                    //    filterPanel.Visible = false;
                    //}
                    //else
                    //{
                    //    filterPanel.Visible = true;
                    //}
                    //if (!string.IsNullOrEmpty(statusIDStr) && int.TryParse(statusIDStr, out int statusID))
                    //{
                    //    Session["DashboardStatusID"] = statusID;
                    //    LoadTicketsByStatus(statusID);
                    //}
                    //else
                    //{
                    //    Session["DashboardStatusID"] = null;
                    //    getTicketlist(); // fallback if no status ID provided
                    //}
                    //  LoadstatusValues();
                    LoadDefaultValues();
                }
                if (!string.IsNullOrEmpty(statusIDStr))
                {
                    // Hide filter if coming from dashboard
                    filterPanel.Visible = false;
                }
                else
                {
                    filterPanel.Visible = true;
                }
                if (!string.IsNullOrEmpty(statusIDStr) && int.TryParse(statusIDStr, out int statusID))
                {
                    Session["DashboardStatusID"] = statusID;
                    LoadTicketsByStatus(statusID);
                }
                else
                {
                    Session["DashboardStatusID"] = null;
                    getTicketlist(); // fallback if no status ID provided
                }
                // statusIDStr = Request.QueryString["statusid"];
                //if (!string.IsNullOrEmpty(statusIDStr) && int.TryParse(statusIDStr, out int statusID))
                //{
                //    LoadTicketsByStatus(statusID);
                //}
                //else
                //{
                //    getTicketlist(); // fallback if no status ID provided
                //}
                //getTicketlist();
            }

            else
            {
                Response.Redirect("~/Login.aspx");
            }

        }

        private void LoadTicketsByStatus(int status)
        {
            DataTable dt = new DataTable();
            //dt = bllTicket.GetTicketList(OrderFromDT, OrderToDT, stat, SessionMgr.DBName);
          //  dt = bllTicket.GetTicketList(status, SessionMgr.DBName);
            dt = bllTicket.GetstatusTicketList( status, SessionMgr.DBName);
            dt.Columns[1].ColumnName = "TicketNo";
            dt.Columns[2].ColumnName = "Date";
            //dt.Columns[3].ColumnName = "Customer Name";
            dt.AcceptChanges();
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();

        }


        private void LoadDefaultValues()
        {

            LoadstatusValues();

            DerializeproductDataTable();          
            DerializeNOPDataTable();
            DerializeCustomerDataTable();           
           // DerializeSevereDataTable();            
            LoadEngineers();
            DerializecmbJoTysDataTable();
        
        }

        private void LoadstatusValues()
        {
            try
            {

               // DataTable dtLoadList = bllTicket.GetCustomerlist(SessionMgr.DBName);
                DataTable dtLoadList = bllTicket.GetStatuslist(SessionMgr.DBName);
                cmbstatus.DataSource = dtLoadList;
                cmbstatus.DataTextField = "StatusName";
                cmbstatus.DataValueField = "StatusID";
                cmbstatus.DataBind();
                cmbstatus.Items.Insert(0, "Please Select");
            }
            catch (Exception ex)
            {
              //  AlertMsg(ex.Message);
            }

        }


      

            private void DerializeproductDataTable()
        {
            try
            {

                DataTable dtLoadList = bllTicket.GetProductlist(SessionMgr.DBName);
                cmbproduct.DataSource = dtLoadList;
                cmbproduct.DataTextField = "product_name";
                // cmbproduct.DataTextField = "ProductSerialNo";
                cmbproduct.DataValueField = "product_id";
                cmbproduct.DataBind();
                cmbproduct.Items.Insert(0, "Please Select");
            }
            catch (Exception ex)
            {
               // AlertMsg(ex.Message);
            }

        }
        private void DerializeNOPDataTable()
        {
            try
            {

                DataTable dtLoadList = bllTicket.GetProblemlist(SessionMgr.DBName);
                cmbNOP.DataSource = dtLoadList;
                cmbNOP.DataTextField = "problem_name";
                cmbNOP.DataValueField = "problem_id";
                cmbNOP.DataBind();
                cmbNOP.Items.Insert(0, "Please Select");
            }
            catch (Exception ex)
            {
                //AlertMsg(ex.Message);
            }

        }

        private void DerializecmbJoTysDataTable()
        {
            try
            {

                DataTable dtLoadList = bllTicket.GetJobTypeslist(SessionMgr.DBName);
                cmbJoTys.DataSource = dtLoadList;
                cmbJoTys.DataTextField = "JobTypes";
                cmbJoTys.DataValueField = "JobTypeId";
                cmbJoTys.DataBind();
                cmbJoTys.Items.Insert(0, "Please Select");
            }
            catch (Exception ex)
            {
               // AlertMsg(ex.Message);
            }

        }
        private void DerializeCustomerDataTable()
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
              //  AlertMsg(ex.Message);
            }

        }
        private void LoadEngineers()
        {
            DataTable dtLoadList = bllTicket.GetEngineerlist(SessionMgr.DBName);
            ddlengg.DataSource = dtLoadList;
            ddlengg.DataTextField = "engineer_name";
            ddlengg.DataValueField = "engineer_id";
            ddlengg.DataBind();
            ddlengg.Items.Insert(0, "Please Select");
        }

        protected void Image_Init(object sender, EventArgs e)
        {
            ASPxImage image = sender as ASPxImage;
            GridViewDataItemTemplateContainer container = image.NamingContainer as GridViewDataItemTemplateContainer;
            
            string CurStatus = ASPxGridView1.GetRowValues(container.VisibleIndex, "CurrentStatus").ToString();
            string TicketID = ASPxGridView1.GetRowValues(container.VisibleIndex, "TicketID").ToString();
            string CompanyCode = ASPxGridView1.GetRowValues(container.VisibleIndex, "CompanyCode").ToString();
            string engineer_id = ASPxGridView1.GetRowValues(container.VisibleIndex, "engineer_id").ToString();

            image.ImageUrl = "../Images/download.png";
            image.Height = 14;
            image.Width = 14;
            string baseUrl = "https://testservices.ezytrackit.com/api/ReportDownload/GetReport?UserId=" + engineer_id + "&clientcode=" + CompanyCode + "&id=" + TicketID;
            if (CurStatus == "8_Closed" && SessionMgr.ClientCode.ToUpper()=="STSS")
            {


                image.Visible = true;
                image.SetClientSideEventHandler("Click", string.Format("function(s, id,c,emp) {{ document.location.href = '{0}'; }}", baseUrl));
              
            }
            else
            {
                image.Visible = false;

            }


        }


        protected void btn_go_click(object sender, EventArgs e)
        {
            getTicketlist();
        }
        protected void btn_new_click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transactions/Tickets.aspx?TicketID=0&id=1");
        }
        public MemoryStream GetStream(XLWorkbook excelWorkbook)
        {
            MemoryStream fs = new MemoryStream();
            excelWorkbook.SaveAs(fs);
            fs.Position = 0;
            return fs;
        }
        //protected void excelexport_Click(object sender, EventArgs e)
        //{
        //    DataTable resultdt = GetDataView(ASPxGridView1);
        //    DataTable dtstatus = myDALTicket.GetTicketStatusDetails(TicketID, dbname);
        //    string Filename = "TicketList";
        //    var wb = new XLWorkbook();
        //    wb.Worksheets.Add(resultdt, "Sheet1");
        //    //wb.SaveAs("E:\\Error_Download.xlsx");
        //    string datestring = DateTime.Now.ToString("dd-MM-yyyy");
        //    string myName = Server.UrlEncode(Filename + "_" + datestring + ".xlsx");
        //    MemoryStream stream = GetStream(wb);// The method is defined below
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition",
        //    "attachment; filename=" + myName);
        //    Response.ContentType = "application/vnd.ms-excel";
        //    Response.BinaryWrite(stream.ToArray());
        //    Response.End();
        //}

        //protected void excelexport_Click(object sender, EventArgs e)
        //{
        //    DataTable resultdt = GetDataView(ASPxGridView1);
        //    string Filename = "TicketList";
        //    var wb = new XLWorkbook();
        //    var ws = wb.Worksheets.Add("Sheet1");

        //    // Add headers for primary data
        //    for (int i = 0; i < resultdt.Columns.Count; i++)
        //    {
        //        ws.Cell(1, i + 1).Value = resultdt.Columns[i].ColumnName;
        //    }
        //    string ticketIDColumnName = "TicketID"; 
        //    // Iterate through each parent row
        //    int rowIndex = 2; // Starting row index for data
        //    foreach (DataRow parentRow in resultdt.Rows)
        //    {
        //        // Add parent data
        //        for (int columnIndex = 0; columnIndex < resultdt.Columns.Count; columnIndex++)
        //        {
        //            ws.Cell(rowIndex, columnIndex + 1).Value = parentRow[columnIndex];
        //        }

        //        // Retrieve child data based on ticket ID
        //       // int ticketID = (int)parentRow["TicketID"]; // Assuming TicketID is the column name
        //        int ticketID = Convert.ToInt32(parentRow[ticketIDColumnName]);
        //        DataTable childData = myDALTicket.GetTicketStatusDetails(ticketID, "pgs_trackit"); // Replace this with your method to get child data

        //        // Add headers for child data
        //        for (int i = 0; i < childData.Columns.Count; i++)
        //        {
        //            ws.Cell(rowIndex + 1, i + 1).Value = childData.Columns[i].ColumnName;
        //        }

        //        // Add child data
        //        for (int childRowIndex = 0; childRowIndex < childData.Rows.Count; childRowIndex++)
        //        {
        //            for (int columnIndex = 0; columnIndex < childData.Columns.Count; columnIndex++)
        //            {
        //                ws.Cell(rowIndex + 2 + childRowIndex, columnIndex + 1).Value = childData.Rows[childRowIndex][columnIndex];
        //            }
        //        }

        //        // Increment rowIndex for the next parent row
        //        rowIndex += 2 + childData.Rows.Count; // Increment by 2 for the parent row and space, and add the child rows count
        //    }

        //    // Auto-fit columns
        //    ws.Columns().AdjustToContents();

        //    string datestring = DateTime.Now.ToString("dd-MM-yyyy");
        //    string myName = Server.UrlEncode(Filename + "_" + datestring + ".xlsx");

        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        wb.SaveAs(memoryStream);
        //        memoryStream.Position = 0;

        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.AddHeader("content-disposition", "attachment; filename=" + myName);
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        memoryStream.CopyTo(Response.OutputStream);
        //        Response.End();
        //    }
        //}  give all child columns 

        //protected void excelexport_Click(object sender, EventArgs e)
        //{
        //    DataTable resultdt = GetDataView(ASPxGridView1);
        //    string Filename = "TicketList";
        //    var wb = new XLWorkbook();
        //    var ws = wb.Worksheets.Add("Sheet1");

        //    // Add headers for primary data
        //    for (int i = 0; i < resultdt.Columns.Count; i++)
        //    {
        //        ws.Cell(1, i + 1).Value = resultdt.Columns[i].ColumnName;
        //    }

        //    // Iterate through each parent row
        //    int rowIndex = 2; // Starting row index for data
        //    foreach (DataRow parentRow in resultdt.Rows)
        //    {
        //        // Add parent data
        //        for (int columnIndex = 0; columnIndex < resultdt.Columns.Count; columnIndex++)
        //        {
        //            ws.Cell(rowIndex, columnIndex + 1).Value = parentRow[columnIndex];
        //        }

        //        // Retrieve the ticket ID from the parentRow
        //        int ticketID = Convert.ToInt32(parentRow["TicketID"]); // Assuming "TicketID" is the correct column name

        //        // Get child data for the current parent row based on the ticket ID
        //        DataTable childData = myDALTicket.GetTicketStatusDetails(ticketID, "pgs_trackit"); // Replace this with your method to get child data

        //        // Add headers for child data
        //        int childColumnIndex = 0;
        //        foreach (DataColumn column in childData.Columns)
        //        {
        //            string columnName = column.ColumnName;
        //            if (columnName != "status_id" && columnName != "engineer_id" && columnName != "claimamount" && columnName != "remarks")
        //            {
        //                ws.Cell(rowIndex + 1, childColumnIndex + 1).Value = columnName;
        //                childColumnIndex++;
        //            }
        //        }

        //        // Add child data
        //        for (int childRowIndex = 0; childRowIndex < childData.Rows.Count; childRowIndex++)
        //        {
        //            childColumnIndex = 0; // Reset childColumnIndex for each row
        //            foreach (DataColumn column in childData.Columns)
        //            {
        //                string columnName = column.ColumnName;
        //                if (columnName != "status_id" && columnName != "engineer_id" && columnName != "claimamount" && columnName != "remarks")
        //                {
        //                    ws.Cell(rowIndex + 2 + childRowIndex, childColumnIndex + 1).Value = childData.Rows[childRowIndex][column];
        //                    childColumnIndex++;
        //                }
        //            }
        //        }

        //        // Increment rowIndex for the next parent row
        //        rowIndex += 2 + childData.Rows.Count; // Increment by 2 for the parent row and space, and add the child rows count
        //    }

        //    // Auto-fit columns
        //    ws.Columns().AdjustToContents();

        //    string datestring = DateTime.Now.ToString("dd-MM-yyyy");
        //    string myName = Server.UrlEncode(Filename + "_" + datestring + ".xlsx");

        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        wb.SaveAs(memoryStream);
        //        memoryStream.Position = 0;

        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.AddHeader("content-disposition", "attachment; filename=" + myName);
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        memoryStream.CopyTo(Response.OutputStream);
        //        Response.End();
        //    }
        //}


        //below is working
        //protected void excelexport_Click(object sender, EventArgs e)
        //{
        //    string dbname = SessionMgr.DBName;
        //    DataTable resultdt = GetDataView(ASPxGridView1);
        //    string Filename = "TicketList";
        //    var wb = new XLWorkbook();
        //    var ws = wb.Worksheets.Add("Sheet1");

        //    // Add headers for primary data with blue color
        //    for (int i = 0; i < resultdt.Columns.Count; i++)
        //    {
        //        var cell = ws.Cell(1, i + 1);
        //        cell.Value = resultdt.Columns[i].ColumnName;
        //        cell.Style.Fill.BackgroundColor = XLColor.LightBlue; // Set header color
        //    }

        //    // Enable filtering for all columns
        //    ws.RangeUsed().SetAutoFilter(true);

        //    // Iterate through each parent row
        //    int rowIndex = 2; // Starting row index for data
        //    foreach (DataRow parentRow in resultdt.Rows)
        //    {
        //        // Add parent data
        //        for (int columnIndex = 0; columnIndex < resultdt.Columns.Count; columnIndex++)
        //        {
        //            ws.Cell(rowIndex, columnIndex + 1).Value = parentRow[columnIndex];
        //        }

        //        // Retrieve the ticket ID from the parentRow
        //        int ticketID = Convert.ToInt32(parentRow["TicketID"]); // Assuming "TicketID" is the correct column name

        //        // Get child data for the current parent row based on the ticket ID
        //        //  DataTable childData = myDALTicket.GetChildData(ticketID, dbname); // Replace this with your method to get child data
        //        DataTable childData = myDALTicket.GetTicketStatusDetails(ticketID, dbname);
        //        // Add headers for child data
        //        int childColumnIndex = 0;
        //        foreach (DataColumn column in childData.Columns)
        //        {
        //            string columnName = column.ColumnName;
        //            if (columnName != "status_id" && columnName != "engineer_id" && columnName != "claimamount" && columnName != "remarks")
        //            {
        //                //var cell = ws.Cell(rowIndex + 1, childColumnIndex + 1);
        //                var cell = ws.Cell(rowIndex + 1, childColumnIndex + 2);
        //                cell.Value = columnName;
        //                cell.Style.Fill.BackgroundColor = XLColor.LightBlue; // Set header color
        //                childColumnIndex++;
        //            }
        //        }

        //        // Add child data
        //        if (childData.Rows.Count == 0)
        //        {
        //            ws.Cell(rowIndex + 2, 2).Value = "No data to display"; // Assuming the message should start from the second column
        //        }
        //        else
        //        {
        //            for (int childRowIndex = 0; childRowIndex < childData.Rows.Count; childRowIndex++)
        //            {
        //                childColumnIndex = 0; // Reset childColumnIndex for each row
        //                foreach (DataColumn column in childData.Columns)
        //                {
        //                    string columnName = column.ColumnName;
        //                    if (columnName != "status_id" && columnName != "engineer_id" && columnName != "claimamount" && columnName != "remarks")
        //                    {
        //                        //  ws.Cell(rowIndex + 2 + childRowIndex, childColumnIndex + 1).Value = childData.Rows[childRowIndex][column];
        //                        ws.Cell(rowIndex + 2 + childRowIndex, childColumnIndex + 2).Value = childData.Rows[childRowIndex][column];
        //                        childColumnIndex++;
        //                    }
        //                }
        //            }
        //        }
        //        // Increment rowIndex for the next parent row
        //        rowIndex += 2 + childData.Rows.Count; // Increment by 2 for the parent row and space, and add the child rows count
        //    }

        //    // Auto-fit columns
        //    ws.Columns().AdjustToContents();

        //    string datestring = DateTime.Now.ToString("dd-MM-yyyy");
        //    string myName = Server.UrlEncode(Filename + "_" + datestring + ".xlsx");

        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        wb.SaveAs(memoryStream);
        //        memoryStream.Position = 0;

        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.AddHeader("content-disposition", "attachment; filename=" + myName);
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        memoryStream.CopyTo(Response.OutputStream);
        //        Response.End();
        //    }
        //}
       // **************** below code  working  Fine  comment for change request from TCC**********************
        //protected void excelexport_Click(object sender, EventArgs e)
        //{
        //    string dbname = SessionMgr.DBName;
        //    DataTable resultdt = GetDataView(ASPxGridView1);
        //    string Filename = "TicketList";
        //    var wb = new XLWorkbook();
        //    var ws = wb.Worksheets.Add("Sheet1");

        //    if (resultdt != null && resultdt.Rows.Count > 0)
        //    {
        //        // Add headers for primary data with blue color and bold font
        //        for (int i = 0; i < resultdt.Columns.Count; i++)
        //        {
        //            var cell = ws.Cell(1, i + 1);
        //            cell.Value = resultdt.Columns[i].ColumnName;
        //            cell.Style.Fill.BackgroundColor = XLColor.LightBlue; // Set header color
        //            cell.Style.Font.Bold = true; // Set bold font
        //        }

        //        // Enable filtering for all columns
        //        ws.RangeUsed().SetAutoFilter(true);

        //        // Add borders to the table
        //        ws.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        ws.RangeUsed().Style.Border.InsideBorder = XLBorderStyleValues.Thin;

        //        // Iterate through each parent row
        //        int rowIndex = 2; // Starting row index for data
        //        foreach (DataRow parentRow in resultdt.Rows)
        //        {
        //            // Add parent data
        //            for (int columnIndex = 0; columnIndex < resultdt.Columns.Count; columnIndex++)
        //            {
        //                var cell = ws.Cell(rowIndex, columnIndex + 1);
        //                cell.Value = parentRow[columnIndex];

        //                // Add border to parent row
        //                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            }

        //            // Add border to parent row
        //            ws.Range(rowIndex, 1, rowIndex, resultdt.Columns.Count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

        //            // Retrieve the ticket ID from the parentRow
        //            int ticketID = Convert.ToInt32(parentRow["TicketID"]); // Assuming "TicketID" is the correct column name

        //            // Get child data for the current parent row based on the ticket ID
        //            DataTable childData = myDALTicket.GetTicketStatusDetails(ticketID, dbname);

        //            if (childData.Rows.Count == 0)
        //            {
        //                // If child has no data, add "No data to display" message
        //                ws.Cell(rowIndex + 1, 2).Value = "No data to display";
        //                ws.Cell(rowIndex + 1, 2).Style.Font.Italic = true; // Set italic font for the message
        //                ws.Cell(rowIndex + 1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin; // Add border
        //            }
        //            else
        //            {
        //                // Add child headers
        //                int childColumnIndex = 0;
        //                foreach (DataColumn column in childData.Columns)
        //                {
        //                    string columnName = column.ColumnName;
        //                    if (columnName != "status_id" && columnName != "engineer_id" && columnName != "claimamount" && columnName != "remarks")
        //                    {
        //                        var cell = ws.Cell(rowIndex + 1, childColumnIndex + 2); // Start from the second column
        //                        cell.Value = columnName;
        //                        cell.Style.Fill.BackgroundColor = XLColor.LightBlue; // Set header color
        //                        cell.Style.Font.Bold = true; // Set bold font for child headers
        //                        childColumnIndex++;
        //                    }
        //                }

        //                // Add child data
        //                for (int childRowIndex = 0; childRowIndex < childData.Rows.Count; childRowIndex++)
        //                {
        //                    childColumnIndex = 0; // Reset childColumnIndex for each row
        //                    foreach (DataColumn column in childData.Columns)
        //                    {
        //                        string columnName = column.ColumnName;
        //                        if (columnName != "status_id" && columnName != "engineer_id" && columnName != "claimamount" && columnName != "remarks")
        //                        {
        //                            ws.Cell(rowIndex + 2 + childRowIndex, childColumnIndex + 2).Value = childData.Rows[childRowIndex][column];
        //                            childColumnIndex++;
        //                        }
        //                    }
        //                }

        //                // Add border to child rows
        //                ws.Range(rowIndex + 1, 2, rowIndex + childData.Rows.Count, resultdt.Columns.Count + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            }

        //            // Increment rowIndex for the next parent row
        //            rowIndex += 2 + (childData.Rows.Count > 0 ? childData.Rows.Count : 1); // Increment by 2 for the parent row and space, and add the child rows count
        //        }

        //        // Auto-fit columns
        //        ws.Columns().AdjustToContents();

        //        // Save the workbook and send it as a response
        //        string datestring = DateTime.Now.ToString("dd-MM-yyyy");
        //        string myName = Server.UrlEncode(Filename + "_" + datestring + ".xlsx");

        //        using (MemoryStream memoryStream = new MemoryStream())
        //        {
        //            wb.SaveAs(memoryStream);
        //            memoryStream.Position = 0;

        //            Response.Clear();
        //            Response.Buffer = true;
        //            Response.AddHeader("content-disposition", "attachment; filename=" + myName);
        //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //            memoryStream.CopyTo(Response.OutputStream);
        //            Response.End();
        //        }
        //    }
        //    else
        //    {
        //        // If resultdt is null or empty, display a message to the user
        //        // For example:
        //        Response.Write("No data available for export.");
        //    }
        //}
        //protected void excelexport_Click(object sender, EventArgs e)
        //{
        //    string dbname = SessionMgr.DBName;
        //    DataTable resultdt = GetDataView(ASPxGridView1);
        //    string Filename = "TicketList";
        //    var wb = new XLWorkbook();
        //    var ws = wb.Worksheets.Add("Sheet1");

        //    // Add headers for primary data with blue color
        //    for (int i = 0; i < resultdt.Columns.Count; i++)
        //    {
        //        var cell = ws.Cell(1, i + 1);
        //        cell.Value = resultdt.Columns[i].ColumnName;
        //        cell.Style.Fill.BackgroundColor = XLColor.LightBlue; // Set header color
        //    }

        //    // Enable filtering for all columns
        //    ws.RangeUsed().SetAutoFilter(true);

        //    // Iterate through each parent row
        //    int rowIndex = 2; // Starting row index for data
        //    foreach (DataRow parentRow in resultdt.Rows)
        //    {
        //        // Add parent data
        //        for (int columnIndex = 0; columnIndex < resultdt.Columns.Count; columnIndex++)
        //        {
        //            ws.Cell(rowIndex, columnIndex + 1).Value = parentRow[columnIndex];
        //        }

        //        // Retrieve the ticket ID from the parentRow
        //        int ticketID = Convert.ToInt32(parentRow["TicketID"]); // Assuming "TicketID" is the correct column name

        //        // Get child data for the current parent row based on the ticket ID
        //        //  DataTable childData = myDALTicket.GetChildData(ticketID, dbname); // Replace this with your method to get child data
        //        DataTable childData = myDALTicket.GetTicketStatusDetails(ticketID, dbname);
        //        // Add headers for child data
        //        int childColumnIndex = 1; // Start from the second column for child data
        //        foreach (DataColumn column in childData.Columns)
        //        {
        //            string columnName = column.ColumnName;
        //            if (columnName != "status_id" && columnName != "engineer_id" && columnName != "claimamount" && columnName != "remarks")
        //            {
        //                var cell = ws.Cell(rowIndex + 1, childColumnIndex);
        //                cell.Value = columnName;
        //                cell.Style.Fill.BackgroundColor = XLColor.Blue; // Set header color
        //                childColumnIndex++;
        //            }
        //        }

        //        // Add child data
        //        for (int childRowIndex = 1; childRowIndex < childData.Rows.Count; childRowIndex++)
        //        {
        //            childColumnIndex = 1; // Start from the second column for child data
        //            foreach (DataColumn column in childData.Columns)
        //            {
        //                string columnName = column.ColumnName;
        //                if (columnName != "status_id" && columnName != "engineer_id" && columnName != "claimamount" && columnName != "remarks")
        //                {
        //                    ws.Cell(rowIndex + 2 + childRowIndex, childColumnIndex).Value = childData.Rows[childRowIndex][column];
        //                    childColumnIndex++; // Increment column index
        //                }
        //            }
        //        }

        //        // Increment rowIndex for the next parent row
        //        rowIndex += 2 + childData.Rows.Count; // Increment by 2 for the parent row and space, and add the child rows count
        //    }

        //    // Auto-fit columns
        //    ws.Columns().AdjustToContents();

        //    string datestring = DateTime.Now.ToString("dd-MM-yyyy");
        //    string myName = Server.UrlEncode(Filename + "_" + datestring + ".xlsx");

        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        wb.SaveAs(memoryStream);
        //        memoryStream.Position = 0;

        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.AddHeader("content-disposition", "attachment; filename=" + myName);
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        memoryStream.CopyTo(Response.OutputStream);
        //        Response.End();
        //    }
        //}

//protected async void excelexport_Click(object sender, EventArgs e)
//{
//    string dbname = SessionMgr.DBName;
//    DataTable resultdt = GetDataView(ASPxGridView1);

//    // Fetch all child data at once
//    Dictionary<int, DataTable> childDataDict = await FetchAllChildData(resultdt, dbname);

//    string Filename = "TicketList";
//    var wb = new XLWorkbook();
//    var ws = wb.Worksheets.Add("Sheet1");

//    // Add headers for primary data with blue color
//    for (int i = 0; i < resultdt.Columns.Count; i++)
//    {
//        var cell = ws.Cell(1, i + 1);
//        cell.Value = resultdt.Columns[i].ColumnName;
//        cell.Style.Fill.BackgroundColor = XLColor.Blue; // Set header color
//    }

//    // Enable filtering for all columns
//    ws.RangeUsed().SetAutoFilter(true);

//    // Write primary and child data
//    WriteData(ws, resultdt, childDataDict);

//    // Auto-fit columns
//    ws.Columns().AdjustToContents();

//    string datestring = DateTime.Now.ToString("dd-MM-yyyy");
//    string myName = Server.UrlEncode(Filename + "_" + datestring + ".xlsx");

//    using (MemoryStream memoryStream = new MemoryStream())
//    {
//        wb.SaveAs(memoryStream);
//        memoryStream.Position = 0;

//        Response.Clear();
//        Response.Buffer = true;
//        Response.AddHeader("content-disposition", "attachment; filename=" + myName);
//        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
//        memoryStream.CopyTo(Response.OutputStream);
//        Response.End();
//    }
//}

//private async Task<Dictionary<int, DataTable>> FetchAllChildData(DataTable parentData, string dbname)
//{
//    Dictionary<int, DataTable> childDataDict = new Dictionary<int, DataTable>();

//    foreach (DataRow parentRow in parentData.Rows)
//    {
//        int ticketID = Convert.ToInt32(parentRow["TicketID"]);
//        DataTable childData =  myDALTicket.GetTicketStatusDetails(ticketID, dbname);
//        childDataDict.Add(ticketID, childData);
//    }

//    return childDataDict;
//}

//private void WriteData(IXLWorksheet ws, DataTable parentData, Dictionary<int, DataTable> childDataDict)
//{
//    int rowIndex = 2; // Starting row index for data

//    foreach (DataRow parentRow in parentData.Rows)
//    {
//        // Write parent data
//        for (int columnIndex = 0; columnIndex < parentData.Columns.Count; columnIndex++)
//        {
//            ws.Cell(rowIndex, columnIndex + 1).Value = parentRow[columnIndex];
//        }

//        int ticketID = Convert.ToInt32(parentRow["TicketID"]);

//        // Write child data
//        if (childDataDict.ContainsKey(ticketID))
//        {
//            DataTable childData = childDataDict[ticketID];

//            // Write headers for child data
//            int childColumnIndex = 0;
//            foreach (DataColumn column in childData.Columns)
//            {
//                string columnName = column.ColumnName;
//                if (columnName != "status_id" && columnName != "engineer_id" && columnName != "claimamount" && columnName != "remarks")
//                {
//                    var cell = ws.Cell(rowIndex + 1, childColumnIndex + 1);
//                    cell.Value = columnName;
//                    cell.Style.Fill.BackgroundColor = XLColor.Blue; // Set header color
//                    childColumnIndex++;
//                }
//            }

//            // Write child data
//            for (int childRowIndex = 0; childRowIndex < childData.Rows.Count; childRowIndex++)
//            {
//                childColumnIndex = 0; // Reset childColumnIndex for each row
//                foreach (DataColumn column in childData.Columns)
//                {
//                    string columnName = column.ColumnName;
//                    if (columnName != "status_id" && columnName != "engineer_id" && columnName != "claimamount" && columnName != "remarks")
//                    {
//                        ws.Cell(rowIndex + 2 + childRowIndex, childColumnIndex + 1).Value = childData.Rows[childRowIndex][column];
//                        childColumnIndex++;
//                    }
//                }
//            }
//        }

//        // Increment rowIndex for the next parent row
//        rowIndex += 2 + (childDataDict.ContainsKey(ticketID) ? childDataDict[ticketID].Rows.Count : 0);
//    }
//}
        protected void excelexport_Click(object sender, EventArgs e)
        {

          //  DateTime dtFrom = DateTime.Now.AddDays(-7);
           // DateTime dtTO = DateTime.Now;

          //  orderfromdt.Value = dtFrom;
           // ordertodt.Value = dtTO;
            getTicketlistExcel();
          //  getTicketlistsp();
            ////string dbname = SessionMgr.DBName;
            ////// Get data from ASPxGridView1
            ////DataTable resultdt = GetDataView(ASPxGridView1);

            ////// Get status details
            ////DataTable dtstatus = myDALTicket.GetStatusDetails(dbname);

            ////// Create a new DataTable to store the pivoted data
            ////DataTable pivotedStatus = new DataTable();

            ////// Add columns to the pivotedStatus DataTable using the unique status values
            ////foreach (DataRow statusRow in dtstatus.Rows)
            ////{
            ////    string status = statusRow["Status"].ToString();
            ////    pivotedStatus.Columns.Add(status, typeof(string));
            ////}

            ////// Merge status information into resultdt
            ////for (int i = 0; i < resultdt.Rows.Count; i++)
            ////{
            ////    DataRow resultRow = resultdt.Rows[i];
            ////    DataRow statusRow = dtstatus.Rows[i];

            ////    // Create a new row in the pivotedStatus DataTable
            ////    DataRow newRow = pivotedStatus.NewRow();

            ////    // Fill in the values from statusRow into the newRow
            ////    foreach (DataColumn column in pivotedStatus.Columns)
            ////    {
            ////        string status = column.ColumnName;

            ////        // Check if the column exists in resultdt before assigning a value
            ////        if (resultRow.Table.Columns.Contains(status))
            ////        {
            ////            newRow[status] = statusRow[status];
            ////        }
            ////    }

            ////    // Add the newRow to the pivotedStatus DataTable
            ////    pivotedStatus.Rows.Add(newRow);
            ////}

            ////// Merge the pivotedStatus DataTable with resultdt
            ////resultdt.Merge(pivotedStatus, false, MissingSchemaAction.Add);

            ////// Create Excel workbook
            ////var wb = new XLWorkbook();
            ////wb.Worksheets.Add(resultdt, "Sheet1");

            ////// Prepare filename
            ////string Filename = "TicketList";
            ////string datestring = DateTime.Now.ToString("dd-MM-yyyy");
            ////string myName = Server.UrlEncode(Filename + "_" + datestring + ".xlsx");

            ////// Prepare response for file download
            ////Response.Clear();
            ////Response.Buffer = true;
            ////Response.AddHeader("content-disposition", "attachment; filename=" + myName);
            ////Response.ContentType = "application/vnd.ms-excel";

            ////// Write Excel data to response
            ////using (MemoryStream stream = new MemoryStream())
            ////{
            ////    wb.SaveAs(stream);
            ////    stream.WriteTo(Response.OutputStream);
            ////}

            ////// End response
            ////Response.End();
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
        protected void cmbstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnstatus.Value = cmbstatus.SelectedValue;
        }
        //protected void ddlengg_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(hdnEngineer.Value))
        //    {
        //        getTicketlist();
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), Guid.NewGuid().ToString(), " getTicketlist();", true);

        //    }
        //}
        protected void Btnclear_Click(object sender, EventArgs e)
        {
            // Reset DropDownList controls to their default state
            cmbstatus.SelectedIndex = -1;
            cmbcustomer.SelectedIndex = -1;
           // cmbbranch.SelectedIndex = -1;
            cmbproduct.SelectedIndex = -1;
        //    cmbbrand.SelectedIndex = -1;
         //   cmbmodel.SelectedIndex = -1;
            cmbNOP.SelectedIndex = -1;
            ddlengg.SelectedIndex = -1;
            cmbJoTys.SelectedIndex = -1;

            // Reset TextBox or other input types if you have any
            // exampleTextBox.Text = "";

            // Reset HiddenFields if necessary
            hdnstatus.Value = "";
            hdncustomer.Value = "";
            hdnBranch.Value = "";
            hdnProduct.Value = "";
            hdnModel.Value = "";
            hdnBrand.Value = "";
            hdnProblem.Value = "";
            hdnEngineer.Value = "";
            hdnJobtype.Value = "";
            DateTime dtFrom = DateTime.Now.AddDays(-7);
            DateTime dtTO = DateTime.Now;
            getTicketlist();
            // If you have any specific logic to rebind data or refresh components, add it here
            // For example, if you're using a GridView and want to refresh it
            // gridView.DataBind();
        }

        private void getTicketlist()
        {
            //  int Engineer = 0;
            int? stat = null;
            int? customer = null;
           // int? Branch = null;
            int? Product = null;
          //  int? Brand = null;
        //    int? Model = null;
            int? Problem = null;
        int? Engineer = null;
            int? Jobtype = null;
           // string selectedValue = hdnstatus.Value;

          // // if (ASPxComboBox1.SelectedIndex > 0)
          //      if (cmbstatus.SelectedIndex > 0)
          //  {
          //    //  stat = ASPxComboBox1.Value.ToString();
          ////      stat = cmbstatus.Value.ToString();
          //       stat = cmbstatus.SelectedValue; 
          //  }
          // // if (ASPxComboBox1.SelectedIndex == 0)
          //      if (cmbstatus.SelectedIndex == 0)
          //  {
          //      stat = "0";
          //  }
            // Use the value from the hidden field instead of the ComboBox directly
          //  if (!string.IsNullOrEmpty(hdnstatus) || hdnstatus!= "Please Select")
          //  if (!string.IsNullOrEmpty(hdnstatus) || hdnstatus.Value != "Please Select")
            if (!string.IsNullOrEmpty(hdnstatus.Value) && hdnstatus.Value != "Please Select")
            {
                stat = int.Parse(hdnstatus.Value);
            }
            else
            {
                stat = 0;
            }
            if (!string.IsNullOrEmpty(hdncustomer.Value) && hdncustomer.Value != "Please Select")
            //if (!string.IsNullOrEmpty(hdncustomer.Value))
            {
                customer = int.Parse(hdncustomer.Value);
            }
            else
            {
                customer = 0;
            }
          //  if (!string.IsNullOrEmpty(hdnBranch.Value) && hdnBranch.Value != "Please Select")
           // if (!string.IsNullOrEmpty(hdnBranch.Value))
         //   {
         //       Branch = int.Parse(hdnBranch.Value);
         //   }
         //   else
        //    {
        //        Branch = 0;
          //  }
            if (!string.IsNullOrEmpty(hdnProduct.Value) && hdnProduct.Value != "Please Select")
           // if (!string.IsNullOrEmpty(hdnProduct.Value))
            {
                Product = int.Parse(hdnProduct.Value);
            }
            else
            {
                Product = 0;
            }
       //     if (!string.IsNullOrEmpty(hdnModel.Value) && hdnModel.Value != "Please Select")
          // if (!string.IsNullOrEmpty(hdnModel.Value))
       //     {
            //    Model = int.Parse(hdnModel.Value);
           // }
          //  else
           // {
            //    Model = 0;
          //  }
           // if (!string.IsNullOrEmpty(hdnBrand.Value) && hdnBrand.Value != "Please Select")
          //  if (!string.IsNullOrEmpty(hdnBrand.Value))
          //  {
            //    Brand = int.Parse(hdnBrand.Value);
            //}
            //else
            //{
            //    Brand = 0;
            //}
            if (!string.IsNullOrEmpty(hdnProblem.Value) && hdnProblem.Value != "Please Select")
           // if (!string.IsNullOrEmpty(hdnProblem.Value))
            {
                Problem = int.Parse(hdnProblem.Value);
            }
            else
            {
                Problem = 0;
            }
            if (!string.IsNullOrEmpty(hdnEngineer.Value) && hdnEngineer.Value != "Please Select")
         //   if (!string.IsNullOrEmpty(hdnEngineer.Value))
            {
                Engineer = int.Parse(hdnEngineer.Value);
            }
            else
            {
                Engineer = 0;
            }
            //string id = ddlengg.SelectedValue.ToString();
            //if (!string.IsNullOrEmpty(id))
            //{
            //    Engineer = int.Parse(id);
            //}

            if (!string.IsNullOrEmpty(hdnJobtype.Value) && hdnJobtype.Value != "Please Select")
          //  if (!string.IsNullOrEmpty(hdnJobtype.Value))
            {
                Jobtype = int.Parse(hdnJobtype.Value);
            }
            else
            {
                Jobtype = 0;
            }

            string OrderFromDT = string.Empty;
            string OrderToDT = string.Empty;
            
            if (!string.IsNullOrEmpty(orderfromdt.Value.ToString()))
            {
                OrderFromDT = Convert.ToDateTime(orderfromdt.Value).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrEmpty(ordertodt.Value.ToString()))
            {
                OrderToDT = Convert.ToDateTime(ordertodt.Value).ToString("yyyy-MM-dd");
            }
            DataTable dt = new DataTable();
            //dt = bllTicket.GetTicketList(OrderFromDT, OrderToDT, stat, SessionMgr.DBName);
            dt = bllTicket.GetTicketList(OrderFromDT, OrderToDT,stat, customer, 0, Product, 0, 0, Problem, Engineer, Jobtype, SessionMgr.DBName);
            dt.Columns[1].ColumnName = "TicketNo";
            dt.Columns[2].ColumnName = "Date";
            //dt.Columns[3].ColumnName = "Customer Name";
            dt.AcceptChanges();
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
            // Resetting the values to 0
            //hdnstatus.Value = "0";
            //hdncustomer.Value = "0";
            //hdnBranch.Value = "0";
            //hdnProduct.Value = "0";
            //hdnModel.Value = "0";
            //hdnBrand.Value = "0";
            //hdnProblem.Value = "0";
            //hdnEngineer.Value = "0";
            //hdnJobtype.Value = "0";

        }

        private void getTicketlistExcel()
        {
            //  int Engineer = 0;
            int? stat = null;
            int? customer = null;
            // int? Branch = null;
            int? Product = null;
            //  int? Brand = null;
            //    int? Model = null;
            int? Problem = null;
            int? Engineer = null;
            int? Jobtype = null;
          //  bool ClosedStatusFlag;
            bool ClosedStatusFlag = true; 
            // string selectedValue = hdnstatus.Value;

            // // if (ASPxComboBox1.SelectedIndex > 0)
            //      if (cmbstatus.SelectedIndex > 0)
            //  {
            //    //  stat = ASPxComboBox1.Value.ToString();
            ////      stat = cmbstatus.Value.ToString();
            //       stat = cmbstatus.SelectedValue; 
            //  }
            // // if (ASPxComboBox1.SelectedIndex == 0)
            //      if (cmbstatus.SelectedIndex == 0)
            //  {
            //      stat = "0";
            //  }
            // Use the value from the hidden field instead of the ComboBox directly
            //  if (!string.IsNullOrEmpty(hdnstatus) || hdnstatus!= "Please Select")
            //  if (!string.IsNullOrEmpty(hdnstatus) || hdnstatus.Value != "Please Select")
            if (!string.IsNullOrEmpty(hdnstatus.Value) && hdnstatus.Value != "Please Select")
            {
                stat = int.Parse(hdnstatus.Value);
            }
            else
            {
                stat = 0;
              ClosedStatusFlag = false;
            }
            if (!string.IsNullOrEmpty(hdncustomer.Value) && hdncustomer.Value != "Please Select")
            //if (!string.IsNullOrEmpty(hdncustomer.Value))
            {
                customer = int.Parse(hdncustomer.Value);
            }
            else
            {
                customer = 0;
            }
            //  if (!string.IsNullOrEmpty(hdnBranch.Value) && hdnBranch.Value != "Please Select")
            // if (!string.IsNullOrEmpty(hdnBranch.Value))
            //   {
            //       Branch = int.Parse(hdnBranch.Value);
            //   }
            //   else
            //    {
            //        Branch = 0;
            //  }
            if (!string.IsNullOrEmpty(hdnProduct.Value) && hdnProduct.Value != "Please Select")
            // if (!string.IsNullOrEmpty(hdnProduct.Value))
            {
                Product = int.Parse(hdnProduct.Value);
            }
            else
            {
                Product = 0;
            }
            //     if (!string.IsNullOrEmpty(hdnModel.Value) && hdnModel.Value != "Please Select")
            // if (!string.IsNullOrEmpty(hdnModel.Value))
            //     {
            //    Model = int.Parse(hdnModel.Value);
            // }
            //  else
            // {
            //    Model = 0;
            //  }
            // if (!string.IsNullOrEmpty(hdnBrand.Value) && hdnBrand.Value != "Please Select")
            //  if (!string.IsNullOrEmpty(hdnBrand.Value))
            //  {
            //    Brand = int.Parse(hdnBrand.Value);
            //}
            //else
            //{
            //    Brand = 0;
            //}
            if (!string.IsNullOrEmpty(hdnProblem.Value) && hdnProblem.Value != "Please Select")
            // if (!string.IsNullOrEmpty(hdnProblem.Value))
            {
                Problem = int.Parse(hdnProblem.Value);
            }
            else
            {
                Problem = 0;
            }
            if (!string.IsNullOrEmpty(hdnEngineer.Value) && hdnEngineer.Value != "Please Select")
            //   if (!string.IsNullOrEmpty(hdnEngineer.Value))
            {
                Engineer = int.Parse(hdnEngineer.Value);
            }
            else
            {
                Engineer = 0;
            }
            //string id = ddlengg.SelectedValue.ToString();
            //if (!string.IsNullOrEmpty(id))
            //{
            //    Engineer = int.Parse(id);
            //}

            if (!string.IsNullOrEmpty(hdnJobtype.Value) && hdnJobtype.Value != "Please Select")
            //  if (!string.IsNullOrEmpty(hdnJobtype.Value))
            {
                Jobtype = int.Parse(hdnJobtype.Value);
            }
            else
            {
                Jobtype = 0;
            }

            string OrderFromDT = string.Empty;
            string OrderToDT = string.Empty;

            if (!string.IsNullOrEmpty(orderfromdt.Value.ToString()))
            {
                OrderFromDT = Convert.ToDateTime(orderfromdt.Value).ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrEmpty(ordertodt.Value.ToString()))
            {
                OrderToDT = Convert.ToDateTime(ordertodt.Value).ToString("yyyy-MM-dd");
            }
            DataTable dt = new DataTable();
            //dt = bllTicket.GetTicketList(OrderFromDT, OrderToDT, stat, SessionMgr.DBName);
            dt = bllTicket.GetTicketListExcel(OrderFromDT, OrderToDT, stat, customer, 0, Product, 0, 0, Problem, Engineer, Jobtype, ClosedStatusFlag, SessionMgr.DBName);
         //   dt.Columns[1].ColumnName = "TicketNo";
         //   dt.Columns[2].ColumnName = "Date";
            //dt.Columns[3].ColumnName = "Customer Name";
            dt.AcceptChanges();
          //  ASPxGridView1.DataSource = dt;
          //  ASPxGridView1.DataBind();
            // Resetting the values to 0
            //hdnstatus.Value = "0";
            //hdncustomer.Value = "0";
            //hdnBranch.Value = "0";
            //hdnProduct.Value = "0";
            //hdnModel.Value = "0";
            //hdnBrand.Value = "0";
            //hdnProblem.Value = "0";
            //hdnEngineer.Value = "0";
            //hdnJobtype.Value = "0";
            string Filename = "TicketList";
            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt, "Sheet1");
            //wb.SaveAs("E:\\Error_Download.xlsx");
            string datestring = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
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

        //private void getTicketlistsp()
        //{
        //    //  int Engineer = 0;
        //    int? stat = null;
        //    int? customer = null;
        //    // int? Branch = null;
        //    int? Product = null;
        //    //  int? Brand = null;
        //    //    int? Model = null;
        //    int? Problem = null;
        //    int? Engineer = null;
        //    int? Jobtype = null;
        //    // string selectedValue = hdnstatus.Value;

        //    // // if (ASPxComboBox1.SelectedIndex > 0)
        //    //      if (cmbstatus.SelectedIndex > 0)
        //    //  {
        //    //    //  stat = ASPxComboBox1.Value.ToString();
        //    ////      stat = cmbstatus.Value.ToString();
        //    //       stat = cmbstatus.SelectedValue; 
        //    //  }
        //    // // if (ASPxComboBox1.SelectedIndex == 0)
        //    //      if (cmbstatus.SelectedIndex == 0)
        //    //  {
        //    //      stat = "0";
        //    //  }
        //    // Use the value from the hidden field instead of the ComboBox directly
        //    //  if (!string.IsNullOrEmpty(hdnstatus) || hdnstatus!= "Please Select")
        //    //  if (!string.IsNullOrEmpty(hdnstatus) || hdnstatus.Value != "Please Select")
        //    if (!string.IsNullOrEmpty(hdnstatus.Value) && hdnstatus.Value != "Please Select")
        //    {
        //        stat = int.Parse(hdnstatus.Value);
        //    }
        //    else
        //    {
        //        stat = 0;
        //    }
        //    if (!string.IsNullOrEmpty(hdncustomer.Value) && hdncustomer.Value != "Please Select")
        //    //if (!string.IsNullOrEmpty(hdncustomer.Value))
        //    {
        //        customer = int.Parse(hdncustomer.Value);
        //    }
        //    else
        //    {
        //        customer = 0;
        //    }
        //    //  if (!string.IsNullOrEmpty(hdnBranch.Value) && hdnBranch.Value != "Please Select")
        //    // if (!string.IsNullOrEmpty(hdnBranch.Value))
        //    //   {
        //    //       Branch = int.Parse(hdnBranch.Value);
        //    //   }
        //    //   else
        //    //    {
        //    //        Branch = 0;
        //    //  }
        //    if (!string.IsNullOrEmpty(hdnProduct.Value) && hdnProduct.Value != "Please Select")
        //    // if (!string.IsNullOrEmpty(hdnProduct.Value))
        //    {
        //        Product = int.Parse(hdnProduct.Value);
        //    }
        //    else
        //    {
        //        Product = 0;
        //    }
        //    //     if (!string.IsNullOrEmpty(hdnModel.Value) && hdnModel.Value != "Please Select")
        //    // if (!string.IsNullOrEmpty(hdnModel.Value))
        //    //     {
        //    //    Model = int.Parse(hdnModel.Value);
        //    // }
        //    //  else
        //    // {
        //    //    Model = 0;
        //    //  }
        //    // if (!string.IsNullOrEmpty(hdnBrand.Value) && hdnBrand.Value != "Please Select")
        //    //  if (!string.IsNullOrEmpty(hdnBrand.Value))
        //    //  {
        //    //    Brand = int.Parse(hdnBrand.Value);
        //    //}
        //    //else
        //    //{
        //    //    Brand = 0;
        //    //}
        //    if (!string.IsNullOrEmpty(hdnProblem.Value) && hdnProblem.Value != "Please Select")
        //    // if (!string.IsNullOrEmpty(hdnProblem.Value))
        //    {
        //        Problem = int.Parse(hdnProblem.Value);
        //    }
        //    else
        //    {
        //        Problem = 0;
        //    }
        //    if (!string.IsNullOrEmpty(hdnEngineer.Value) && hdnEngineer.Value != "Please Select")
        //    //   if (!string.IsNullOrEmpty(hdnEngineer.Value))
        //    {
        //        Engineer = int.Parse(hdnEngineer.Value);
        //    }
        //    else
        //    {
        //        Engineer = 0;
        //    }
        //    //string id = ddlengg.SelectedValue.ToString();
        //    //if (!string.IsNullOrEmpty(id))
        //    //{
        //    //    Engineer = int.Parse(id);
        //    //}

        //    if (!string.IsNullOrEmpty(hdnJobtype.Value) && hdnJobtype.Value != "Please Select")
        //    //  if (!string.IsNullOrEmpty(hdnJobtype.Value))
        //    {
        //        Jobtype = int.Parse(hdnJobtype.Value);
        //    }
        //    else
        //    {
        //        Jobtype = 0;
        //    }

        //    string OrderFromDT = string.Empty;
        //    string OrderToDT = string.Empty;

        //    if (!string.IsNullOrEmpty(orderfromdt.Value.ToString()))
        //    {
        //        OrderFromDT = Convert.ToDateTime(orderfromdt.Value).ToString("yyyy-MM-dd");
        //    }
        //    if (!string.IsNullOrEmpty(ordertodt.Value.ToString()))
        //    {
        //        OrderToDT = Convert.ToDateTime(ordertodt.Value).ToString("yyyy-MM-dd");
        //    }
        //    DataTable dt = new DataTable();
        //    //dt = bllTicket.GetTicketList(OrderFromDT, OrderToDT, stat, SessionMgr.DBName);
        //    dt = bllTicket.GetTicketListsp(OrderFromDT, OrderToDT, stat, customer, 0, Product, 0, 0, Problem, Engineer, Jobtype, SessionMgr.DBName);
        //    dt.Columns[1].ColumnName = "TicketNo";
        //    dt.Columns[2].ColumnName = "Date";
        //    //dt.Columns[3].ColumnName = "Customer Name";
        //    dt.AcceptChanges();
        //    ASPxGridView1.DataSource = dt;
        //    ASPxGridView1.DataBind();
        //    // Resetting the values to 0
        //    //hdnstatus.Value = "0";
        //    //hdncustomer.Value = "0";
        //    //hdnBranch.Value = "0";
        //    //hdnProduct.Value = "0";
        //    //hdnModel.Value = "0";
        //    //hdnBrand.Value = "0";
        //    //hdnProblem.Value = "0";
        //    //hdnEngineer.Value = "0";
        //    //hdnJobtype.Value = "0";

        //}
    }
}