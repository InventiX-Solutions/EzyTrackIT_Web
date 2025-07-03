using ClosedXML.Excel;
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
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackIT.ClassModules;

namespace TCC_CRM.Transactions
{
    public partial class Milestone : System.Web.UI.Page
    {
        public BLLTicket bllTicket = new BLLTicket();
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();
        public int engineer_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SessionMgr.DBName))
            {

                if (!IsPostBack)
                {
                    DateTime dtFrom = DateTime.Now;
                    DateTime dtTO = DateTime.Now;

                    orderfromdt.Value = dtFrom;
                    ordertodt.Value = dtTO;
                    defaultvalues();
                    //engineer_id = Convert.ToInt32(Request.QueryString["id"].ToString());
                }
               
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

        }
        public class clsRowsOrderDetailsList
        {
          
            public string MilestoneName { get; set; }
            public string MilestoneTime { get; set; }
        
        }

        public class clsOrderHeaders
        {
            public clsOrderHeaders()
            {
                RowsOrderDetails = new List<clsRowsOrderDetailsList>();


            }
            public string engcode { get; set; }
            public string engname { get; set; }
            public string engemail { get; set; }
            public string engmobile { get; set; }
            public List<clsRowsOrderDetailsList> RowsOrderDetails { get; set; }
        }

        public class clsRowsMileStone
        {
            public string MilestoneName { get; set; }
            public string MilestoneTime { get; set; }
            public string StatusImage { get; set; }
            public string Image { get; set; }
            public int Status { get; set; }
            public double timediff { get; set; }
            public string jobdetails { get; set; }
        }
        public class clsMileStoneTable
        {
            public clsMileStoneTable()
            {
                RowsMileStone = new List<clsRowsMileStone>();


            }
            public List<clsRowsMileStone> RowsMileStone { get; set; }
            public string engcode { get; set; }
            public string engname { get; set; }
            public string engemail { get; set; }
            public string engmobile { get; set; }
        }
        private void defaultvalues()
        {
             DataTable dtLoadList = bllTicket.GetEngineerlist(SessionMgr.DBName);
             cmbeng.DataSource = dtLoadList;
             cmbeng.DataTextField = "engineer_name";
             cmbeng.DataValueField = "engineer_id";
             cmbeng.DataBind();
             cmbeng.Items.Insert(0, "Please Select");
        
        }
        private void getMilestone()
        {
            try
            {
                string id = cmbeng.SelectedValue.ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    engineer_id = Convert.ToInt32(id);
                }

                string FDate = string.Empty;
                string TDate = string.Empty;

                if (!string.IsNullOrEmpty(orderfromdt.Value.ToString()))
                {
                    FDate = Convert.ToDateTime(orderfromdt.Value).ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrEmpty(ordertodt.Value.ToString()))
                {
                    TDate = Convert.ToDateTime(ordertodt.Value).ToString("yyyy-MM-dd");
                }

                clsMileStoneTable MileStonetable = new clsMileStoneTable();
              
                DataTable dt = new DataTable();
                dt = bllTicket.GettMilestonebyEngineer(SessionMgr.DBName, engineer_id.ToString(), FDate, TDate);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    clsRowsMileStone MileStoneRow = new clsRowsMileStone();
                    if (!string.IsNullOrEmpty(dt.Rows[i]["MilestoneName"].ToString()))
                    {
                        MileStoneRow.Image = "../Images/green.jpg";
                        MileStoneRow.StatusImage = "../Images/green.jpg";
                        MileStoneRow.MilestoneName = dt.Rows[i]["MilestoneName"].ToString();
                        MileStoneRow.MilestoneTime = dt.Rows[i]["MilestoneTime"].ToString();
                        MileStoneRow.timediff=0;
                        MileStoneRow.jobdetails = dt.Rows[i]["jobdetails"].ToString();
                        if (i > 0)
                        {
                            string dtime = dt.Rows[i]["starttime"].ToString();
                            string prvdtime = dt.Rows[i-1]["starttime"].ToString();
                            DateTime dts = Convert.ToDateTime(dtime);
                            DateTime dtsprev = Convert.ToDateTime(prvdtime);
                           double val= dts.Subtract(dtsprev).TotalMinutes;
                           MileStoneRow.timediff = Convert.ToDouble(val.ToString("#.##"));
                        }

                        MileStonetable.RowsMileStone.Add(MileStoneRow);
                    }
                    MileStonetable.engcode = dt.Rows[i]["engineer_code"].ToString();
                    MileStonetable.engemail = dt.Rows[i]["emailid"].ToString();
                    MileStonetable.engmobile = dt.Rows[i]["mobileno"].ToString();
                   
                    
                }
                string json = JsonConvert.SerializeObject(MileStonetable);
                milestonedetails.Value = json;
            }
            catch (Exception ex)
            {

                BeCommon.FormName = "getMilestone";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BLLCommon.UpdateLog(BeCommon, SessionMgr.DBName);
            }
        }

        protected void btn_go_click(object sender, EventArgs e)
        {
            if (cmbeng.SelectedValue.ToString() != "Please Select")
            {
                getMilestone();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), Guid.NewGuid().ToString(), "getmilestone();", true);
            }
        }

        protected void cmbeng_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbeng.SelectedValue.ToString()!="Please Select")
            {
                getMilestone();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), Guid.NewGuid().ToString(), "getmilestone();", true);
                
            }
        }
    }
}