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
    public partial class BrandList : System.Web.UI.Page
    {
        public BLLBrand bllbrand = new BLLBrand();
        public BLLProduct bllProduct = new BLLProduct();
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionMgr.ClientCode))
                {
                    lblval.Text = string.Empty;
                    lblval.Visible = false;

                    getbrandlist();
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

                DataTable dtLoadList = bllProduct.GetProductList(SessionMgr.DBName);
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
        private void getbrandlist()
        {
            DataTable dt = new DataTable();
            dt = bllbrand.GetBrandList(SessionMgr.DBName);
            
            dt.Columns[1].ColumnName = "Code";
            dt.Columns[2].ColumnName = "Name";
            dt.AcceptChanges();
            if (dt.Rows.Count > 0)
            {
                gvbrandlist.DataSource = dt;
                gvbrandlist.DataBind();
                btnnew.Visible = false;
              
            }
            else 
            {
                dt.Rows.Add(dt.NewRow());
                gvbrandlist.DataSource = dt;
                gvbrandlist.DataBind();
                int TotalColumns = gvbrandlist.Rows[0].Cells.Count;
                gvbrandlist.Rows[0].Cells.Clear();
                gvbrandlist.Rows[0].Cells.Add(new TableCell());
                gvbrandlist.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                gvbrandlist.Rows[0].Cells[0].Text = "No Record Found";

               
                btnnew.Visible = true;
            }
         
        }
        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            e.Row.Cells[5].Visible = false;
            //e.Row.Cells[5].Visible = false;
        }

        //protected void btn_Addbrand(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/Masters/Brand.aspx", false);
        //}
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int intExist = 0;
                int id = 0;
                string lblid = lblbrandid.Text;
                int productid = 0;
                string selprd = hdnprd.Value.ToString();

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
                if (!string.IsNullOrEmpty(lblid))
                {
                    id = Convert.ToInt32(lblid);
                }
                if(ddlproduct.SelectedValue.ToString()=="0")
                {
                    lblval.Text = "Please Select a Product";
                    lblval.Visible = true;
                    lblval.ForeColor = System.Drawing.Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                    return;
                }
                //if (id == 0)
                //{
                    intExist = bllbrand.GetDuplicateExists(SessionMgr.CompanyID, "brands", "brand_code", txtbcode.Text.Trim().ToLower(), "brand_id", id, SessionMgr.DBName);
                    if (intExist > 0)
                    {
                        lblval.Text = "Code Already Exists";
                        lblval.Visible = true;
                        lblval.ForeColor = System.Drawing.Color.Red;
                        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                        ddlproduct.SelectedValue = productid.ToString();
                        return;
                      
                    }
                    //}//Commented by Velu on 19-02-2020, Edit mode also must check the code duplicate

                BEBrand br = new BEBrand();
                br.BrandCode = txtbcode.Text.Trim();
                br.BrandName = txtbname.Text.Trim();
                br.productid = productid;
                br.BrandID = id;
                br.createdby = SessionMgr.UserID;
                br.modifiedby = SessionMgr.UserID;
                int insertorupdate = bllbrand.InsertOrUpdateRecord(br, SessionMgr.DBName);
                if (insertorupdate > 0)
                {
                    AlertMsg("Saved Successfully");
                   
                }
                Response.Redirect("~/Masters/BrandList.aspx?Saved=" + insertorupdate, false);
            }
            catch (Exception ex)
            {

                BeCommon.FormName = "BrandList";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BLLCommon.UpdateLog(BeCommon, SessionMgr.DBName);
            }
            
        }
        protected void Display(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvbrandlist.Rows[rowIndex];

            lblval.Text = string.Empty;

            lblbrandid.Text = (row.FindControl("lblbrand_id") as Label).Text;
            txtbcode.Text = (row.FindControl("lblcode") as Label).Text; ;
            txtbname.Text = (row.FindControl("lblname") as Label).Text;
            ddlproduct.ClearSelection();
            ddlproduct.Items.FindByText((row.FindControl("lblprdname") as Label).Text).Selected = true;
            hdnprd.Value = ddlproduct.SelectedValue.ToString();
            //ddlproduct.SelectedItem.Text = (row.FindControl("lblprdname") as Label).Text;
            
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "updateselectedval();", true);
        }
        protected void Delete(object sender, EventArgs e)
        {
            try
            
            {
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                GridViewRow row = gvbrandlist.Rows[rowIndex];
                lblbrandid.Text = (row.FindControl("lblbrand_id") as Label).Text;
                int  BrandID = Convert.ToInt32(lblbrandid.Text);
                int deletebrand = bllbrand.DeleteBrand(BrandID, SessionMgr.UserID, SessionMgr.DBName);
                if (deletebrand > 0)
                {
                    AlertMsg("Record Deleted");
                    lblbrandid.Text = string.Empty;
                }
               getbrandlist();
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

            { ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('"+Msg+"');", true); }
        }
        #endregion

      
    }
}