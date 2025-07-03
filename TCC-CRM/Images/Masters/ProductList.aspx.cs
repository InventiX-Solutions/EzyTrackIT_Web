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
    public partial class ProductList : System.Web.UI.Page
    {
       
        public BLLProduct bllProduct = new BLLProduct();
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblval.Text = string.Empty;
                lblval.Visible = false;

                if (!string.IsNullOrEmpty(SessionMgr.DBName))
                {
                    getproductlist();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }


        }
        
         private void getproductlist()
        {
            DataTable dt = new DataTable();
            dt = bllProduct.GetProductList(SessionMgr.DBName);

            dt.Columns[1].ColumnName = "Code";
            dt.Columns[2].ColumnName = "Name";
         
            dt.AcceptChanges();
            if (dt.Rows.Count > 0)
            {
                gvProductlist.DataSource = dt;
                gvProductlist.DataBind();
                btnnew.Visible = false;

            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                gvProductlist.DataSource = dt;
                gvProductlist.DataBind();
                int TotalColumns = gvProductlist.Rows[0].Cells.Count;
                gvProductlist.Rows[0].Cells.Clear();
                gvProductlist.Rows[0].Cells.Add(new TableCell());
                gvProductlist.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                gvProductlist.Rows[0].Cells[0].Text = "No Record Found";


                btnnew.Visible = true;
            }

        }
         private void getModellist()
         {
             DataTable dt = new DataTable();
             dt = bllProduct.GetModellist(SessionMgr.DBName);

             dt.Columns[3].ColumnName = "model_id";
           //  dt.Columns[2].ColumnName = "Name";
             dt.AcceptChanges();
             if (dt.Rows.Count > 0)
             {
                 gvProductlist.DataSource = dt;
                 gvProductlist.DataBind();
                 btnnew.Visible = false;

             }
             else
             {
                 dt.Rows.Add(dt.NewRow());
                 gvProductlist.DataSource = dt;
                 gvProductlist.DataBind();
                 int TotalColumns = gvProductlist.Rows[0].Cells.Count;
                 gvProductlist.Rows[0].Cells.Clear();
                 gvProductlist.Rows[0].Cells.Add(new TableCell());
                 gvProductlist.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                 gvProductlist.Rows[0].Cells[0].Text = "No Record Found";


                 btnnew.Visible = true;
             }

         }
         private void getbrandlist()
         {
             DataTable dt = new DataTable();
             dt = bllProduct.GetBrandlist(SessionMgr.DBName);
             dt.Columns[4].ColumnName = "brand_id";
          //   dt.Columns[2].ColumnName = "Name";
             dt.AcceptChanges();
             if (dt.Rows.Count > 0)
             {
                 gvProductlist.DataSource = dt;
                 gvProductlist.DataBind();
                 btnnew.Visible = false;

             }
             else
             {
                 dt.Rows.Add(dt.NewRow());
                 gvProductlist.DataSource = dt;
                 gvProductlist.DataBind();
                 int TotalColumns = gvProductlist.Rows[0].Cells.Count;
                 gvProductlist.Rows[0].Cells.Clear();
                 gvProductlist.Rows[0].Cells.Add(new TableCell());
                 gvProductlist.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                 gvProductlist.Rows[0].Cells[0].Text = "No Record Found";


                 btnnew.Visible = true;
             }

         }
         protected void btnSave_Click(object sender, EventArgs e)
         {
             try
             {
                 int intExist = 0;
                 int id = 0;
                 string lblid = lblProductid.Text;
                 if (!string.IsNullOrEmpty(lblid))
                 {
                     id = Convert.ToInt32(lblid);
                 }
                 //if (id == 0)
                 //{
                     intExist = bllProduct.GetDuplicateExists(SessionMgr.CompanyID, "products", "product_code", txtbcode.Text.Trim().ToLower(), "product_id", id, SessionMgr.DBName);
                     if (intExist > 0)
                     {
                         lblval.Text = "Code Already Exists";
                         lblval.Visible = true;
                         ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                         return;

                     }
                     //}
                     //Commented by Velu on 19-02-2020, Edit mode also must check the code duplicate
                 BEProduct br = new BEProduct();
                 br.product_code = txtbcode.Text.Trim();
                 br.product_name = txtbname.Text.Trim();
               
                 br.product_id = id;
             
                 br.Created_By = SessionMgr.UserID;
                 br.Modified_By = SessionMgr.UserID;
                 int insertorupdate = bllProduct.InsertOrUpdateRecord(br, SessionMgr.DBName);
                 if (insertorupdate > 0)
                 {
                     AlertMsg("Saved Successfully");

                 }
                 Response.Redirect("~/Masters/ProductList.aspx?Saved=" + insertorupdate, false);
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

         protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
         {
             e.Row.Cells[4].Visible = false;
         }

         protected void btn_AddnewProduct(object sender, EventArgs e)
         {
             int product_id = 0;
             Response.Redirect("~/Masters/Product.aspx?product_id=" + product_id, false);
         }
      
         protected void Display(object sender, EventArgs e)
         {
             int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
             GridViewRow row = gvProductlist.Rows[rowIndex];

             lblval.Text = string.Empty;

             lblProductid.Text = (row.FindControl("lblproduct_id") as Label).Text;
             txtbcode.Text = (row.FindControl("lblcode") as Label).Text; ;
             txtbname.Text = (row.FindControl("lblname") as Label).Text;
             lblval.Visible = false;
             ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);

         }
         protected void Delete(object sender, EventArgs e)
         {
             try
             {
                 int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                 GridViewRow row = gvProductlist.Rows[rowIndex];
                 int product_id = Convert.ToInt32((row.FindControl("lblproduct_id") as Label).Text);

                 int deleteproduct = bllProduct.DeleteProduct(product_id, SessionMgr.UserID, SessionMgr.DBName);
                 if (deleteproduct > 0)
                 {
                     AlertMsg("Record Deleted");
                     lblProductid.Text = string.Empty;
                 }
                 getproductlist();
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

             { ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + Msg + "');", true); }
         }
         #endregion
    }
}