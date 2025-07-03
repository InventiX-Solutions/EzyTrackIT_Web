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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackIT.ClassModules;

namespace TCC_CRM.Transactions
{
    public partial class TimeandCostAnalysis : System.Web.UI.Page
    {
        public BLLTimeCostAnalysis bllTimeCostAnalysis = new BLLTimeCostAnalysis();
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_go_click(object sender, EventArgs e)
        {
           int ReportType= Convert.ToInt32(cmbreporttype.SelectedValue);

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
           dt = bllTimeCostAnalysis.GetReportList(OrderFromDT, OrderToDT, ReportType, SessionMgr.DBName);
           dt.Columns[1].ColumnName = "TicketNo";
           dt.Columns[2].ColumnName = "Date";
          
           dt.AcceptChanges();
           ASPxGridView1.DataSource = dt;
           ASPxGridView1.DataBind();
        }
    }
}