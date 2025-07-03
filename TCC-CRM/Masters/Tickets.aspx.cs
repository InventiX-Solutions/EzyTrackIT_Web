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
    public partial class Tickets : System.Web.UI.Page
    {
        public BETicket m_BETicket;
        public BLLTicket bllTicket = new BLLTicket();

        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                hdnTicketID.Value = Request.QueryString["TicketID"].ToString();
                getTicketlist(Convert.ToInt32((hdnTicketID.Value)));
                DerializebrandDataTable();
                DerializeModelDataTable();
                DerializeproductDataTable();
                DerializeSTDataTable();
                DerializeNOPDataTable();
                DerializeCustomerDataTable();
                DerializestatusDataTable();
                txtdate.Text = DateTime.Now.ToString("d/MM/yyyy");
                txtdate.Enabled = false;
                //cmbstatus.Enabled = false;
                LoadDefaultVaues();
            }}

        private void DerializebrandDataTable()
        {
            try
            {

                DataTable dtLoadList = bllTicket.GetBrandlist();
                cmbbrand.DataSource = dtLoadList;
                cmbbrand.DataTextField = "brand_name";
                cmbbrand.DataValueField = "brand_id";
                cmbbrand.DataBind();
                cmbbrand.Items.Insert(0, "Please Select");
            }
            catch (Exception ex)
            {
                AlertMsg(ex.Message);
            }

        }
        private void LoadDefaultVaues()
        {


            txttno.Text = bllTicket.GetDocumentNumber(SessionMgr.CompanyID, "TKT", "S");
            txttno.Enabled = false;


        }
        private void DerializeModelDataTable()
        {
            try
            {

                DataTable dtLoadList = bllTicket.GetModellist();
                cmbmodel.DataSource = dtLoadList;
                cmbmodel.DataTextField = "model_name";
                cmbmodel.DataValueField = "Model_id";
                cmbmodel.DataBind();
                cmbmodel.Items.Insert(0, "Please Select");
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

                DataTable dtLoadList = bllTicket.GetProductlist();
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
         private void DerializeSTDataTable()
         {
             try
             {

                 DataTable dtLoadList = bllTicket.GetSTlist();
                 cmbST.DataSource = dtLoadList;
                 cmbST.DataTextField = "service_type_name";
                 cmbST.DataValueField = "service_typeid";
                 cmbST.DataBind();
                 cmbST.Items.Insert(0, "Please Select");
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

                 DataTable dtLoadList = bllTicket.GetProblemlist();
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
         private void DerializeCustomerDataTable()
         {
             try
             {

                 DataTable dtLoadList = bllTicket.GetCustomerlist();
                 cmbcustomer.DataSource = dtLoadList;
                 cmbcustomer.DataTextField = "customer_name";
                 cmbcustomer.DataValueField = "customer_id";
                 cmbcustomer.DataBind();
                 cmbcustomer.Items.Insert(0, "Please Select");
             }
             catch (Exception ex)
             {
                 AlertMsg(ex.Message);
             }

         }

         private void DerializestatusDataTable()
         {
             try
             {

                 DataTable dtLoadList = bllTicket.GetStatuslist();
                 cmbstatus.DataSource = dtLoadList;
                 cmbstatus.DataTextField = "StatusName";
                 cmbstatus.DataValueField = "StatusID";
                 cmbstatus.DataBind();
                 cmbstatus.Items.Insert(0, "Open");

             }
             catch (Exception ex)
             {
                 AlertMsg(ex.Message);
             }

         }
         private void getTicketlist(int TicketID)
         {
             if (m_BETicket == null) m_BETicket = new BETicket();

             m_BETicket = bllTicket.GetTicket(SessionMgr.CompanyID, TicketID);
             if (m_BETicket != null)
             {
                 //txtdate.Text = Convert.ToDateTime(m_BETicket.Date);
                 txttno.Text = m_BETicket.TicketNo;
                 txtaddrl1.Text = m_BETicket.AddressLine1;
                 txtaddrl2.Text = m_BETicket.AddressLine2;
                 txtpno.Text = m_BETicket.Status;
                 txtpserialno.Text = m_BETicket.ProductSerialNo;
                 cmbcustomer.SelectedValue = m_BETicket.CustomerName;
                 cmbbrand.SelectedValue = m_BETicket.BrandName;
                 cmbmodel.SelectedValue = m_BETicket.ModelName;
                 cmbproduct.SelectedValue = m_BETicket.ProductName;
                 cmbST.SelectedValue = m_BETicket.ServiceType;
                 cmbNOP.SelectedValue = m_BETicket.NatureOfProblem;
             }
         }
         protected void cmbbrandselectedvalue(object sender, EventArgs e)
         {
             int s = Convert.ToInt32(cmbbrand.SelectedValue);

             DataTable dtTicket = new DataTable();
             dtTicket = bllTicket.GetDropDownValues("model_name", "model_id", "models", "Brand_ID = " + cmbbrand.SelectedValue);
             cmbmodel.DataSource = dtTicket;
             cmbmodel.DataTextField = "Name";
             cmbmodel.DataValueField = "Value";
             cmbmodel.DataBind();
             if (dtTicket.Rows.Count > 1)
             {
                 cmbmodel.SelectedIndex = 1;
             }
             else
                 cmbmodel.SelectedIndex = -1;
            // cmbmodel.Enabled = false;

         }
         protected void cmbmodelselectedvalue(object sender, EventArgs e)
         {
             int s = Convert.ToInt32(cmbmodel.SelectedValue);

             DataTable dtTicket = new DataTable();
             dtTicket = bllTicket.GetDropDownValues("product_name", "product_id", "products", "product_id = " + cmbmodel.SelectedValue);
             cmbproduct.DataSource = dtTicket;
             cmbproduct.DataTextField = "Name";
             cmbproduct.DataValueField = "Value";
             cmbproduct.DataBind();
             if (dtTicket.Rows.Count > 1)
             {
                 cmbproduct.SelectedIndex = 1;
             }
             else
                 cmbproduct.SelectedIndex = -1;

         }
         protected void cmbproductselectedvalue(object sender, EventArgs e)
         {
             int s = Convert.ToInt32(cmbproduct.SelectedValue);

             DataTable dtTicket = new DataTable();
             dtTicket = bllTicket.GetDropDownValues("PartNumber", "product_id", "products", "product_id = " + cmbproduct.SelectedValue);
             
             txtpserialno.Text = dtTicket.Rows[1]["Name"].ToString();
             txtpserialno.Enabled = false;
         }
         protected void cmbcustomerselectedvalue(object sender, EventArgs e)
         {
            int CustomerID= Convert.ToInt32(cmbcustomer.SelectedValue);
             DataTable dtLoadDropdownvalues;

             dtLoadDropdownvalues = new DataTable();
             dtLoadDropdownvalues = bllTicket.GeMultipletDropDownValues(CustomerID);
             txtaddrl1.Text = dtLoadDropdownvalues.Rows[0]["address_line_1"].ToString();
             txtaddrl2.Text = dtLoadDropdownvalues.Rows[0]["address_line_2"].ToString();
             txtpno.Text = dtLoadDropdownvalues.Rows[0]["phone_no"].ToString();

             txtaddrl1.Enabled = false;
             txtaddrl2.Enabled = false;
             txtpno.Enabled = false;

            // int s = Convert.ToInt32(cmbcustomer.SelectedValue);

            // DataTable dtTicket = new DataTable();
            // dtTicket = bllTicket.GetDropDownValues("address_line_1", "customer_id", "customers", "customer_id = " + cmbcustomer.SelectedValue);
            // txtaddrl1.Text = dtTicket.Rows[1]["Name"].ToString();
            //// txtaddrl2.Text = dtTicket.Rows[2]["name"].ToString();
            //// txtpno.Text = dtTicket.Rows[3]["name"].ToString();
            
         }
         protected void OnbtnBackClick(object sender, EventArgs e)
         {
             Response.Redirect("~/Masters/TicketList.aspx", false);
         }
         protected void OnBtnSaveClick(object sender, EventArgs e)
         {

             try
             {
                 hdnTicketID.Value = Request.QueryString["TicketID"].ToString();
                 int intExist = 0;
                 int id = 0;

                 //string lblid = lblcustomerid.Text;
                 string lblid = hdnTicketID.Value;

                 if (!string.IsNullOrEmpty(lblid))
                 {
                     id = Convert.ToInt32(lblid);
                 }
                 if (id == 0)
                 {
                     intExist = bllTicket.GetDuplicateExists(SessionMgr.CompanyID, "ticket", "TicketNo", txttno.Text.Trim().ToLower(), "TicketID", id);
                     if (intExist > 0)
                     {
                         lblval.Text = "Code Already Exists";
                         lblval.Visible = true;
                         ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                         return;

                     }
                 }
                 BETicket br = new BETicket();
                 br.TicketDt =txtdate.Text.Trim();
                 br.TicketNo = txttno.Text.Trim();               
                 br.ProductSerialNo = txtpserialno.Text;
                 br.TicketID = id;
                 br.CustomerID = Convert.ToInt32(cmbcustomer.SelectedValue);
                 br.AddressLine1 = txtaddrl1.Text.Trim();
                 br.AddressLine2 = txtaddrl2.Text.Trim();
                 br.Status = txtpno.Text.Trim();
                 br.ModelID = Convert.ToInt32(cmbmodel.SelectedValue);
                 br.BrandID = Convert.ToInt32(cmbbrand.SelectedValue);
                 br.ProductID = Convert.ToInt32(cmbproduct.SelectedValue);
                 br.ServiceType = cmbST.SelectedValue;
                 br.ServiceTypeID = Convert.ToInt32(cmbST.SelectedValue);
                 br.ProblemID = Convert.ToInt32(cmbNOP.SelectedValue);
                 br.NatureOfProblem = cmbNOP.SelectedValue;
                 br.ProductSerialNo = txtpserialno.Text.Trim();
                 br.CreatedBy = SessionMgr.UserID;
                 br.ModifiedBy = SessionMgr.UserID;
                 int insertorupdate = bllTicket.InsertOrUpdateRecord(br);
                 if (insertorupdate > 0)
                 {
                     //AlertMsg("Saved Successfully");
                     Response.Redirect("~/Masters/TicketList.aspx", false);

                 }

             }
             catch (Exception ex)
             {

                 BeCommon.FormName = "TicketList";
                 BeCommon.ErrorDescription = ex.Message;
                 BeCommon.ErrorType = ex.Message;
                 BeCommon.CreatedDate = DateTime.Now;

                 m_BLLCommon.UpdateLog(BeCommon);
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