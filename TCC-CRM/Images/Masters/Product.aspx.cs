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
    public partial class Product : System.Web.UI.Page
    {
            public BEProduct m_BEProduct;
            public BLLProduct bllProduct = new BLLProduct();
         
            public BLLCommon m_BLLCommon = new BLLCommon();
            public BECommom BeCommon = new BECommom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionMgr.DBName))
                {
                    hdnProductID.Value = Request.QueryString["product_id"].ToString();
                    getproductlist(Convert.ToInt32((hdnProductID.Value)));
                    DerializebrandDataTable();
                    DerializeModelDataTable();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        private void DerializebrandDataTable()
        {
            try
            {

                DataTable dtLoadList = bllProduct.GetBrandlist(SessionMgr.DBName);
                cmbbrandname.DataSource = dtLoadList;
                cmbbrandname.DataTextField = "brand_name";
                cmbbrandname.DataValueField = "brand_id";
                cmbbrandname.DataBind();
                cmbbrandname.Items.Insert(0, "Please Select");
            }
            catch (Exception ex)
            {
                AlertMsg(ex.Message);
            }

        }
        private void DerializeModelDataTable()
        {
            try
            {

                DataTable dtLoadList = bllProduct.GetModellist(SessionMgr.DBName);
                cmbmodelname.DataSource = dtLoadList;
                cmbmodelname.DataTextField = "model_name";
                cmbmodelname.DataValueField = "Model_id";
                cmbmodelname.DataBind();
                cmbmodelname.Items.Insert(0, "Please Select");
            }
            catch (Exception ex)
            {
                AlertMsg(ex.Message);
            }

        }
        private void getproductlist(int product_id)
        {
            if (m_BEProduct == null) m_BEProduct = new BEProduct();

            m_BEProduct = bllProduct.GetProduct(SessionMgr.CompanyID, product_id, SessionMgr.DBName);
            if (m_BEProduct != null)
            {
                txtpcode.Text = m_BEProduct.product_code;
                txtpname.Text = m_BEProduct.product_name;
                txtpno.Text =m_BEProduct.PartNo;
                cmbbrandname.SelectedValue = m_BEProduct.brand_name;
                cmbmodelname.SelectedValue = m_BEProduct.model_name;

            }
        }

        protected void cmbbrandselectedvalue(object sender, EventArgs e)
        {
            int s=Convert.ToInt32(cmbbrandname.SelectedValue);
            
                DataTable dtproduct = new DataTable();
                dtproduct = bllProduct.GetDropDownValues("model_name", "model_id", "models", "Brand_ID = " + cmbbrandname.SelectedValue, SessionMgr.DBName);
                cmbmodelname.DataSource = dtproduct;
                cmbmodelname.DataTextField = "Name";
                cmbmodelname.DataValueField = "Value";
                cmbmodelname.DataBind();
                if (dtproduct.Rows.Count>1)
                {
                    cmbmodelname.SelectedIndex = 1;
                }
                else
                    cmbmodelname.SelectedIndex = -1;

        }
        protected void OnbtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Masters/ProductList.aspx", false);
        }
        protected void OnBtnSaveClick(object sender, EventArgs e)
        {

            try
            {
                hdnProductID.Value = Request.QueryString["product_id"].ToString();
                int intExist = 0;
                int id = 0;

                //string lblid = lblcustomerid.Text;
                string lblid = hdnProductID.Value;

                if (!string.IsNullOrEmpty(lblid))
                {
                    id = Convert.ToInt32(lblid);
                }
                if (id == 0)
                {
                    intExist = bllProduct.GetDuplicateExists(SessionMgr.CompanyID, "products", "product_code", txtpcode.Text.Trim().ToLower(), "product_id", id, SessionMgr.DBName);
                    if (intExist > 0)
                    {
                        lblval.Text = "Code Already Exists";
                        lblval.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                        return;

                    }
                }
                BEProduct br = new BEProduct();
                br.product_code = txtpcode.Text.Trim();
                br.product_name = txtpname.Text.Trim();
                br.PartNo =  txtpno.Text;
                br.product_id = id;
                br.model_id = Convert.ToInt32(cmbmodelname.SelectedValue);
                br.brand_id = Convert.ToInt32(cmbbrandname.SelectedValue);
                br.Created_By = SessionMgr.UserID;
                br.Modified_By = SessionMgr.UserID;
                int insertorupdate = bllProduct.InsertOrUpdateRecord(br, SessionMgr.DBName);
                if (insertorupdate > 0)
                {
                    //AlertMsg("Saved Successfully");
                    Response.Redirect("~/Masters/ProductList.aspx", false);

                }

            }
            catch (Exception ex)
            {

                BeCommon.FormName = "ProductList";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BLLCommon.UpdateLog(BeCommon, SessionMgr.DBName);
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