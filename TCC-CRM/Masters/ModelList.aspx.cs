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
    public partial class ModelList : System.Web.UI.Page
    {
        public BLLModel bllModel = new BLLModel();
        public BLLProduct bllProduct = new BLLProduct();
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionMgr.DBName))
                {
                    lblval.Text = string.Empty;
                    lblval.Visible = false;

                    getModellist();
                    DerializeproductDataTable();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        private void DerializeproductDataTable()
        {
            try
            {
              
                DataTable dtLoadList = bllModel.GetBrandlist(SessionMgr.DBName);
                cmbbrand.DataSource = dtLoadList;
                cmbbrand.DataTextField = "brand_name";
                cmbbrand.DataValueField = "brand_id";
                cmbbrand.DataBind();
                cmbbrand.Items.Insert(0, "Please Select");

                dtLoadList = bllProduct.GetProductList(SessionMgr.DBName);
                ddlproduct.DataSource = dtLoadList;
                ddlproduct.DataTextField = "product_name";
                ddlproduct.DataValueField = "product_id";
                ddlproduct.DataBind();
                ddlproduct.Items.Insert(0, "Please Select");
            }
            catch (Exception ex)
            {
                AlertMsg(ex.Message);
            }

        }
        private void getModellist()
        {
            DataTable dt = new DataTable();
            dt = bllModel.GetModelList(SessionMgr.DBName);

            dt.Columns[1].ColumnName = "Code";
            dt.Columns[2].ColumnName = "Name";
            dt.Columns[4].ColumnName = "Brand Name";

            dt.AcceptChanges();
            if (dt.Rows.Count > 0)
            {
                gvModellist.DataSource = dt;
                gvModellist.DataBind();
                btnnew.Visible = false;

            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                gvModellist.DataSource = dt;
                gvModellist.DataBind();
                int TotalColumns = gvModellist.Rows[0].Cells.Count;
                gvModellist.Rows[0].Cells.Clear();
                gvModellist.Rows[0].Cells.Add(new TableCell());
                gvModellist.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                gvModellist.Rows[0].Cells[0].Text = "No Record Found";


                btnnew.Visible = true;
            }
           
        }
        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[9].Visible = false;
           
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int intExist = 0;
                int id = 0;
                string lblid = lblModelid.Text;
                if (!string.IsNullOrEmpty(lblid))
                {
                    id = Convert.ToInt32(lblid);
                }

                int productid = 0;
                string selprd = hdnprod.Value.ToString();
                if (string.IsNullOrEmpty(selprd) || selprd == "Please Select")
                {
                    lblval.Text = "Please Select a Product";
                    lblval.Visible = true;
                    lblval.ForeColor = System.Drawing.Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                    return;
                }
                else
                {
                    productid = Convert.ToInt32(selprd);
                }

                int brandid = 0;
                string selbrnd = hdnbrand.Value.ToString();
                if (string.IsNullOrEmpty(selbrnd) || selbrnd == "Please Select")
                {
                    lblval.Text = "Please Select a Brand";
                    lblval.Visible = true;
                    lblval.ForeColor = System.Drawing.Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                    return;
                }
                else
                {
                    brandid = Convert.ToInt32(selbrnd);
                }

                
                //if (id == 0)
                //{
                    intExist = bllModel.GetDuplicateExists(SessionMgr.CompanyID, "models", "model_code", txtbcode.Text.Trim().ToLower(), "model_id", id, SessionMgr.DBName);
                    if (intExist > 0)
                    {
                        lblval.Text = "Code Already Exists";
                        lblval.Visible = true;
                        lblval.ForeColor = System.Drawing.Color.Red;
                        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                        return;

                    }
                    //}//Commented by Velu on 19-02-2020, Edit mode also must check the code duplicate

                BEModel br = new BEModel();
                br.ModelCode = txtbcode.Text.Trim();
                br.ModelName = txtbname.Text.Trim();
                br.ModelID = id;
                br.productid = productid;
                br.Brand_ID = brandid;
                br.createdby = SessionMgr.UserID;
                br.modifiedby = SessionMgr.UserID;
                int insertorupdate = bllModel.InsertOrUpdateRecord(br, SessionMgr.DBName);
                if (insertorupdate > 0)
                {
                    AlertMsg("Saved Successfully");
                    getModellist();
                }
                //Response.Redirect("~/Masters/BrandList.aspx?Saved=" + insertorupdate, false);
            }
            catch (Exception ex)
            {

                BeCommon.FormName = "ModelList";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BLLCommon.UpdateLog(BeCommon, SessionMgr.DBName);
            }

        }
        protected void Display(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvModellist.Rows[rowIndex];

            lblval.Text = string.Empty;
            lblModelid.Text = (row.FindControl("lblModel_id") as Label).Text;
            txtbcode.Text = (row.FindControl("lblcode") as Label).Text; 
            txtbname.Text = (row.FindControl("lblname") as Label).Text;

            ddlproduct.ClearSelection();
            ddlproduct.Items.FindByText((row.FindControl("lblprdname") as Label).Text).Selected = true;
            hdnprod.Value = ddlproduct.SelectedValue.ToString();

            cmbbrand.ClearSelection();
            cmbbrand.Items.FindByText((row.FindControl("lblbrandName") as Label).Text).Selected = true;
            hdnbrand.Value = cmbbrand.SelectedValue.ToString();
                  
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
        }
        protected void Delete(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                GridViewRow row = gvModellist.Rows[rowIndex];
                lblModelid.Text = (row.FindControl("lblModel_id") as Label).Text;
                int ModelID = Convert.ToInt32(lblModelid.Text);
                int deleteModel = bllModel.DeleteModel(ModelID, SessionMgr.UserID, SessionMgr.DBName);
                if (deleteModel > 0)
                {
                    AlertMsg("Record Deleted");
                    lblModelid.Text = string.Empty;
                    getModellist();
                }
                // getbrandlist();
            }
            catch (Exception ex)
            {
                AlertMsg(ex.Message);
            }
        }
        protected void GridView_PreRender(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;

            if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                || (gv.ShowHeaderWhenEmpty == true))
            {
                //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
                gv.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            if (gv.ShowFooter == true && gv.Rows.Count > 0)
            {
                //Force GridView to use <tfoot> instead of <tbody> - 11/03/2013 - MCR.
                gv.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }
        #region Alerts
        private void AlertMsg(string Msg)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + Msg + "');", true);
        }
        #endregion

        protected void ddlproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtLoadList = bllProduct.GetDropDownValues("brand_name", "brand_id", "brands", "product_id = " + ddlproduct.SelectedValue + "", SessionMgr.DBName);
            if (dtLoadList.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dtLoadList.Rows[0]["Name"].ToString()))
                {
                    cmbbrand.DataSource = dtLoadList;
                    cmbbrand.DataTextField = "Name";
                    cmbbrand.DataValueField = "Value";
                    cmbbrand.DataBind();
                    cmbbrand.Items.Insert(0, "Please Select");
                }
                else
                {
                    cmbbrand.Items.Clear();
                    //cmbbrand.DataSource = null;
                    cmbbrand.Items.Insert(0, "Please Select");
                }
            }
            else
            {
                cmbbrand.Items.Clear();
                cmbbrand.Items.Insert(0, "Please Select");
            }
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);

        }
    }
}