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
    public partial class CustomerBranch : System.Web.UI.Page
    {
        public BECustomerBranch m_BECustomer;
        public BLLCustomerBranch bllCustomer = new BLLCustomerBranch();
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtremarks.Attributes.Add("maxlength", txtremarks.MaxLength.ToString());
                lblHeader.Text = "New Customer Branch";

                if (!string.IsNullOrEmpty(SessionMgr.DBName))
                {
                    DerializeCustomerDataTable();
                    hdnCustomerD.Value = Request.QueryString["customer_branch_id"].ToString();
                    if (Convert.ToInt32((hdnCustomerD.Value)) > 0)
                        getcustomerlist(Convert.ToInt32((hdnCustomerD.Value)));
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }

        }
        private void DerializeCustomerDataTable()
        {
            try
            {

                DataTable dtLoadList = bllCustomer.GetCustomer(SessionMgr.DBName);
                ddlCustomer.DataSource = dtLoadList;
                ddlCustomer.DataTextField = "customer_Name";
                ddlCustomer.DataValueField = "customer_ID";
                ddlCustomer.DataBind();
                ddlCustomer.Items.Insert(0, "Please Select");
            }
            catch (Exception ex)
            {
                AlertMsg(ex.Message);
            }

        }
        private void getcustomerlist(int customer_branch_id)
        {
            if (m_BECustomer == null) m_BECustomer = new BECustomerBranch();

            m_BECustomer = bllCustomer.GetCustomer(SessionMgr.CompanyID, customer_branch_id, SessionMgr.DBName);
            if (m_BECustomer != null)
            {
                lblHeader.Text = "Edit Customer";
                ddlCustomer.SelectedValue = m_BECustomer.CustomerID.ToString();
                txtccode.Text = m_BECustomer.CustomerBranchCode;
                txtcname.Text = m_BECustomer.CustomerBranchName;
                txtadd1.Text = m_BECustomer.AddressLine1;
                txtadd2.Text = m_BECustomer.AddressLine2;
                txtcper.Text = m_BECustomer.ContactPerson;
                txtphonenno.Text = m_BECustomer.PhoneNo;
                txtslocation.Text = m_BECustomer.ServiceLocation;
                txtremarks.Text = m_BECustomer.Remarks;
            }
        }
       
        protected void OnBtnSaveClick(object sender, EventArgs e)
        {
            try
            {
                hdnCustomerD.Value = Request.QueryString["customer_branch_id"].ToString();
                int intExist = 0;
                int id = 0;

                //string lblid = lblcustomerid.Text;
                string lblid = hdnCustomerD.Value;

                if (!string.IsNullOrEmpty(lblid))
                {
                    id = Convert.ToInt32(lblid);
                }
                //if (id == 0)
                //{
                intExist = bllCustomer.GetDuplicateExists(SessionMgr.CompanyID, "customerbranch", "customer_branch_code", txtccode.Text.Trim().ToLower(), "customer_branch_id", id, SessionMgr.DBName);
                    if (intExist > 0)
                    {
                        lblval.Text = "Code Already Exists";
                        lblval.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                        return;

                    }
                    //}  //Commented by Velu on 19-02-2020, Edit mode also must check the code duplicate
                BECustomerBranch br = new BECustomerBranch();
                br.CustomerID = Int32.Parse(ddlCustomer.SelectedValue.Trim());
                br.CustomerBranchCode = txtccode.Text.Trim();
                br.CustomerBranchName = txtcname.Text.Trim();
                br.AddressLine1 = txtadd1.Text.Trim();
                br.AddressLine2 = txtadd2.Text.Trim();
                br.ContactPerson = txtcper.Text.Trim();
                br.PhoneNo = txtphonenno.Text.Trim();
                br.ServiceLocation = txtslocation.Text.Trim();
                br.Remarks = txtremarks.Text.Trim();
                br.CustomerBranchID = id;
                br.createdby = SessionMgr.UserID;
                br.modifiedby = SessionMgr.UserID;
                int insertorupdate = bllCustomer.InsertOrUpdateRecord(br, SessionMgr.DBName);
                if (insertorupdate > 0)
                {
                    //AlertMsg("Saved Successfully");
                    Response.Redirect("~/Masters/CustomerBranchList.aspx", false);

                }

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

        protected void OnBtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Masters/CustomerBranchList.aspx", false);
        }


        #region Alerts
        private void AlertMsg(string Msg)
        {

            { ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + Msg + "');", true); }
        }
        #endregion
    }
}