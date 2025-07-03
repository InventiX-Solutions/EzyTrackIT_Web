using CRM.Artifacts;
using CRM.Artifacts.Masters;
using CRM.Bussiness;
using CRM.Bussiness.Masters;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.XtraCharts;
using DevExpress.XtraPivotGrid;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackIT.ClassModules;

namespace TCC_CRM.Transactions
{
    public partial class CustomerDashboard : System.Web.UI.Page
    {
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BLLTicket bllTicket = new BLLTicket();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SessionMgr.DBName))
            {
                if (!IsPostBack)
                {


                    DateTime dtFrom = DateTime.Now.AddDays(-7);
                    DateTime dtTO = DateTime.Now;

                    ASPxDateEdit1.Value = dtFrom;
                    ASPxDateEdit2.Value = dtTO;

                    LoadDefaultVaues();

                }
                System.Drawing.Color c = System.Drawing.ColorTranslator.FromHtml("#DADADA");
                ASPxPivotGrid2.BackColor = c;
                ASPxPivotGrid2.Font.Bold = true;

                ASPxPivotGrid2.OptionsPager.RowsPerPage = 20;
                WebChartControl1.SeriesTemplate.ChangeView(ViewType.Bar);
                ASPxPivotGrid2.Visible = false;
                WebChartControl1.Visible = false;
                
                loadpivot();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        protected void btn_go_click(object sender, EventArgs e)
        {

            loadpivot();

        }

        protected void Btnclear_Click(object sender, EventArgs e)
        {
            // Reset DropDownList controls to their default state

            cmbcustomer.SelectedIndex = -1;
           // cmbbranch.SelectedIndex = -1;
            cmbproduct.SelectedIndex = -1;
           // cmbbrand.SelectedIndex = -1;
           // cmbmodel.SelectedIndex = -1;
            cmbNOP.SelectedIndex = -1;
            ddlengg.SelectedIndex = -1;
            cmbJoTys.SelectedIndex = -1;

            // Reset TextBox or other input types if you have any
            // exampleTextBox.Text = "";

            // Reset HiddenFields if necessary

            hdncustomerSelectedValue.Value = "";
            hdnbranch.Value = "";
            hdnproduct.Value = "";
            hdnmodel.Value = "";
            hdnbrand.Value = "";
            hdnNOP.Value = "";
            hdnengg.Value = "";
            hdnjob.Value = "";
            DateTime dtFrom = DateTime.Now.AddDays(-7);
            DateTime dtTO = DateTime.Now;
            loadpivot();
            // If you have any specific logic to rebind data or refresh components, add it here
            // For example, if you're using a GridView and want to refresh it
            // gridView.DataBind();
        }
        private void LoadDefaultVaues()
        {

            //DerializebrandDataTable();
            //DerializeModelDataTable();
            DerializeproductDataTable();
            //DerializeSTDataTable();
            DerializeNOPDataTable();
            DerializeCustomerDataTable();
            //DerializestatusDataTable();
            //DerializeSevereDataTable();
            //DerializePriorityDataTable();
            LoadEngineers();
            //LoadAssignedTo();
            DerializecmbJoTysDataTable();


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
                AlertMsg(ex.Message);
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
                AlertMsg(ex.Message);
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
                AlertMsg(ex.Message);
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
                AlertMsg(ex.Message);
            }

        }

        #region Alerts
        private void AlertMsg(string Msg)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + Msg + "');", true);
        }
        #endregion
        //public void loadpivot()
        //{
        //    ASPxPivotGrid2.Fields.Clear();
        //    DataTable ds = new DataTable();
        //    string StartTime = Convert.ToDateTime(ASPxDateEdit1.Value).ToString("yyyy-MM-dd");
        //    string FinishTime = Convert.ToDateTime(ASPxDateEdit2.Value).ToString("yyyy-MM-dd");
        //    int CustomerID;// = Convert.ToInt32(cmbcustomer.SelectedValue);
        //    if (hdncustomerSelectedValue.Value == "Please Select" || hdncustomerSelectedValue.Value == "")
        //    {
        //        CustomerID = 0;
        //    }
        //    else
        //    {
        //        CustomerID = Convert.ToInt32(hdncustomerSelectedValue.Value);
        //    }
        //    int BranchID; //= Convert.ToInt32(hdnbranch.Value);
        //    //if (hdnbranch.Value == "Please Select" || hdnbranch.Value == "")
        //    //{
        //        BranchID = 0;
        //    //}
        //    //else
        //    //{
        //    //    BranchID = Convert.ToInt32(hdnbranch.Value);
        //    //}
        //    int EngineerID;
        //    if (hdnengg.Value == "" || hdnengg.Value == "Please Select")
        //    {
        //        EngineerID = 0;
        //    }
        //    else
        //    {
        //        EngineerID = Convert.ToInt32(hdnengg.Value);
        //    }
        //    int ModelID; //= Convert.ToInt32(hdnmodel.Value);
        //    //if (hdnmodel.Value == "Please Select" || hdnmodel.Value == "")
        //    //{
        //        ModelID = 0;
        //    //}
        //    //else
        //    //{
        //    //    ModelID = Convert.ToInt32(hdnmodel.Value);
        //    //}
        //    int BrandID;// = Convert.ToInt32(hdnbrand.Value);
        //    //if (hdnbrand.Value == "Please Select" || hdnbrand.Value == "")
        //    //{
        //        BrandID = 0;
        //    //}
        //    //else
        //    //{
        //    //    BrandID = Convert.ToInt32(hdnbrand.Value);
        //    //}
        //    int ProductID;// = Convert.ToInt32(hdnproduct.Value);
        //    if (hdnproduct.Value == "Please Select" || hdnproduct.Value == "")
        //    {
        //        ProductID = 0;
        //    }
        //    else
        //    {
        //        ProductID = Convert.ToInt32(hdnproduct.Value);
        //    }
        //    int ProblemID;// = Convert.ToInt32(cmbNOP.SelectedValue);
        //    if (hdnNOP.Value == "Please Select" || hdnNOP.Value == "")
        //    {
        //        ProblemID = 0;
        //    }
        //    else
        //    {
        //        ProblemID = Convert.ToInt32(hdnNOP.Value);
        //    }
        //    int jobtypeID;
        //    if (hdnjob.Value == "Please Select" || hdnjob.Value == "")
        //    {
        //        jobtypeID = 0;
        //    }
        //    else
        //    {
        //        jobtypeID = Convert.ToInt32(hdnjob.Value);
        //    }
        //    ds = m_BLLCommon.GetStatusData("C", StartTime, FinishTime, CustomerID, BranchID, ProductID, BrandID, ModelID, ProblemID, EngineerID,jobtypeID, SessionMgr.DBName);

        //    if (ds.Rows.Count > 0)
        //    {
        //        ASPxPivotGrid2.Visible = true;
        //        WebChartControl1.Visible = true;
        //        int exists1 = 0;
        //        int exists2 = 0;
        //        int exists3 = 0;
        //        int exists4 = 0;
        //        int exists5 = 0;
        //        int exists6 = 0;
        //        int exists7 = 0;
        //        int exists8 = 0;
        //        foreach (DataRow dr in ds.Rows)
        //        {
        //            if (dr["ColumnValue"].ToString() == "1_New")
        //            {
        //                exists1 = 1;
        //                break;
        //            }
        //        }
        //        foreach (DataRow dr in ds.Rows)
        //        {
        //            if (dr["ColumnValue"].ToString() == "2_Open")
        //            {
        //                exists2 = 1;
        //                break;
        //            }
        //        }
        //        foreach (DataRow dr in ds.Rows)
        //        {
        //            if (dr["ColumnValue"].ToString() == "3_Assigned")
        //            {
        //                exists3 = 1;
        //                break;
        //            }
        //        }
        //        foreach (DataRow dr in ds.Rows)
        //        {
        //            if (dr["ColumnValue"].ToString() == "4_On Hold")
        //            {
        //                exists4 = 1;
        //                break;
        //            }
        //        }
        //        foreach (DataRow dr in ds.Rows)
        //        {
        //            if (dr["ColumnValue"].ToString() == "5_Rejected")
        //            {
        //                exists5 = 1;
        //                break;
        //            }
        //        }
        //        foreach (DataRow dr in ds.Rows)
        //        {
        //            if (dr["ColumnValue"].ToString() == "6_Cust_NA")
        //            {
        //                exists6 = 1;
        //                break;
        //            }
        //        }
        //        foreach (DataRow dr in ds.Rows)
        //        {
        //            if (dr["ColumnValue"].ToString() == "7_Apr_Pend")
        //            {
        //                exists7 = 1;
        //                break;
        //            }
        //        }
        //        foreach (DataRow dr in ds.Rows)
        //        {
        //            if (dr["ColumnValue"].ToString() == "8_Closed")
        //            {
        //                exists7 = 1;
        //                break;
        //            }
        //        }

        //        if (exists1 == 0)
        //        {

        //            DataRow dr = ds.NewRow();

        //            dr["ColumnValue"] = "1_New";
        //            dr["RowValue"] = "";
        //            dr["StatusCount"] = 0;
        //            ds.Rows.Add(dr);

        //        }
        //        if (exists2 == 0)
        //        {

        //            DataRow dr = ds.NewRow();

        //            dr["ColumnValue"] = "2_Open";
        //            dr["RowValue"] = "";
        //            dr["StatusCount"] = 0;
        //            ds.Rows.Add(dr);

        //        }
        //        if (exists3 == 0)
        //        {

        //            DataRow dr = ds.NewRow();

        //            dr["ColumnValue"] = "3_Assigned";
        //            dr["RowValue"] = "";
        //            dr["StatusCount"] = 0;
        //            ds.Rows.Add(dr);

        //        }
        //        if (exists4 == 0)
        //        {

        //            DataRow dr = ds.NewRow();

        //            dr["ColumnValue"] = "4_On Hold";
        //            dr["RowValue"] = "";
        //            dr["StatusCount"] = 0;
        //            ds.Rows.Add(dr);

        //        }
        //        if (exists5 == 0)
        //        {

        //            DataRow dr = ds.NewRow();

        //            dr["ColumnValue"] = "5_Rejected";
        //            dr["RowValue"] = "";
        //            dr["StatusCount"] = 0;
        //            ds.Rows.Add(dr);

        //        }
        //        if (exists6 == 0)
        //        {

        //            DataRow dr = ds.NewRow();

        //            dr["ColumnValue"] = "6_Cust_NA";
        //            dr["RowValue"] = "";
        //            dr["StatusCount"] = 0;
        //            ds.Rows.Add(dr);

        //        }
        //        if (exists7 == 0)
        //        {

        //            DataRow dr = ds.NewRow();

        //            dr["ColumnValue"] = "7_Apr_Pend";
        //            dr["RowValue"] = "";
        //            dr["StatusCount"] = 0;
        //            ds.Rows.Add(dr);

        //        }
        //        if (exists8 == 0)
        //        {

        //            DataRow dr = ds.NewRow();

        //            dr["ColumnValue"] = "8_Closed";
        //            dr["RowValue"] = "";
        //            dr["StatusCount"] = "0";
        //            ds.Rows.Add(dr);

        //        }
        //        ASPxPivotGrid2.DataSource = ds;


        //        //PivotGridField fieldSequence_No = new PivotGridField("Sequence_No", PivotArea.ColumnArea);
        //        PivotGridField fieldMileStone_Name = new PivotGridField("ColumnValue", PivotArea.ColumnArea);
        //        PivotGridField fieldActualDate = new PivotGridField("RowValue", PivotArea.RowArea);
        //        //fieldActualDate.CellFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
        //        //fieldActualDate.CellFormat.FormatString = "dd/MMM/yyyy";

        //        //fieldActualDate.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
        //        //fieldActualDate.ValueFormat.FormatString = "dd/MMM/yyyy";

        //        PivotGridField fieldStatusCount = new PivotGridField("StatusCount", PivotArea.DataArea);
        //        fieldStatusCount.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;


        //        //fieldSequence_No.Visible = false;
        //        //ASPxPivotGrid2.Fields.AddField(fieldSequence_No);
        //        ASPxPivotGrid2.Fields.AddField(fieldMileStone_Name);

        //        ASPxPivotGrid2.Fields.AddField(fieldActualDate);
        //        ASPxPivotGrid2.Fields.AddField(fieldStatusCount);
        //        ASPxPivotGrid2.OptionsView.ShowColumnGrandTotals = false;


        //    }
        //    else
        //    {

        //    }
        //}

        //above working fine comment for dynamic changes

        public void loadpivot()
        {
            ASPxPivotGrid2.Fields.Clear();
            DataTable ds = new DataTable();
            string StartTime = Convert.ToDateTime(ASPxDateEdit1.Value).ToString("yyyy-MM-dd");
            string FinishTime = Convert.ToDateTime(ASPxDateEdit2.Value).ToString("yyyy-MM-dd");
            int CustomerID;// = Convert.ToInt32(cmbcustomer.SelectedValue);
            if (hdncustomerSelectedValue.Value == "Please Select" || hdncustomerSelectedValue.Value == "")
            {
                CustomerID = 0;
            }
            else
            {
                CustomerID = Convert.ToInt32(hdncustomerSelectedValue.Value);
            }
            int BranchID; //= Convert.ToInt32(hdnbranch.Value);
            //if (hdnbranch.Value == "Please Select" || hdnbranch.Value == "")
            //{
            BranchID = 0;
            //}
            //else
            //{
            //    BranchID = Convert.ToInt32(hdnbranch.Value);
            //}
            int EngineerID;
            if (hdnengg.Value == "" || hdnengg.Value == "Please Select")
            {
                EngineerID = 0;
            }
            else
            {
                EngineerID = Convert.ToInt32(hdnengg.Value);
            }
            int ModelID; //= Convert.ToInt32(hdnmodel.Value);
            //if (hdnmodel.Value == "Please Select" || hdnmodel.Value == "")
            //{
            ModelID = 0;
            //}
            //else
            //{
            //    ModelID = Convert.ToInt32(hdnmodel.Value);
            //}
            int BrandID;// = Convert.ToInt32(hdnbrand.Value);
            //if (hdnbrand.Value == "Please Select" || hdnbrand.Value == "")
            //{
            BrandID = 0;
            //}
            //else
            //{
            //    BrandID = Convert.ToInt32(hdnbrand.Value);
            //}
            int ProductID;// = Convert.ToInt32(hdnproduct.Value);
            if (hdnproduct.Value == "Please Select" || hdnproduct.Value == "")
            {
                ProductID = 0;
            }
            else
            {
                ProductID = Convert.ToInt32(hdnproduct.Value);
            }
            int ProblemID;// = Convert.ToInt32(cmbNOP.SelectedValue);
            if (hdnNOP.Value == "Please Select" || hdnNOP.Value == "")
            {
                ProblemID = 0;
            }
            else
            {
                ProblemID = Convert.ToInt32(hdnNOP.Value);
            }
            int jobtypeID;
            if (hdnjob.Value == "Please Select" || hdnjob.Value == "")
            {
                jobtypeID = 0;
            }
            else
            {
                jobtypeID = Convert.ToInt32(hdnjob.Value);
            }
            ds = m_BLLCommon.GetStatusData("C", StartTime, FinishTime, CustomerID, BranchID, ProductID, BrandID, ModelID, ProblemID, EngineerID, jobtypeID, SessionMgr.DBName);

            if (ds.Rows.Count > 0)
            {
                ASPxPivotGrid2.Visible = true;
                WebChartControl1.Visible = true;

                // Retrieve expected status values into a DataTable
                DataTable expectedStatusValues = m_BLLCommon.GetExpectedStatusValuesAsDataTable(SessionMgr.DBName);

                if (expectedStatusValues.Rows.Count == 0)
                {
                    // Handle case when no status values are retrieved from the database
                }
                else
                {
                    // Your existing code to populate the pivot grid with expected status values
                    foreach (DataRow expectedStatusRow in expectedStatusValues.Rows)
                    {
                        bool exists = false;
                        foreach (DataRow dr in ds.Rows)
                        {
                            if (dr["ColumnValue"].ToString() == expectedStatusRow["ColumnValue"].ToString())
                            {
                                exists = true;
                                break;
                            }
                        }

                        if (!exists)
                        {
                            DataRow newRow = ds.NewRow();
                            newRow["ColumnValue"] = expectedStatusRow["ColumnValue"].ToString();
                            newRow["RowValue"] = "";
                            newRow["StatusCount"] = 0;
                            ds.Rows.Add(newRow);
                        }
                    }
                }

                ASPxPivotGrid2.DataSource = ds;

                // Add pivot grid fields
                PivotGridField fieldMileStone_Name = new PivotGridField("ColumnValue", PivotArea.ColumnArea);
                PivotGridField fieldActualDate = new PivotGridField("RowValue", PivotArea.RowArea);
                PivotGridField fieldStatusCount = new PivotGridField("StatusCount", PivotArea.DataArea);
                fieldStatusCount.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;

                // Add pivot grid fields
                ASPxPivotGrid2.Fields.Add(fieldMileStone_Name);
                ASPxPivotGrid2.Fields.Add(fieldActualDate);
                ASPxPivotGrid2.Fields.Add(fieldStatusCount);
                ASPxPivotGrid2.OptionsView.ShowColumnGrandTotals = false;

                //  ASPxPivotGrid2.Fields.AddRange(new PivotGridField[] { fieldMileStone_Name, fieldActualDate, fieldStatusCount });
                //ASPxPivotGrid2.OptionsView.ShowColumnGrandTotals = false;
            }
            else
            {
                // Handle case when there is no data
            }
        }
    }
}