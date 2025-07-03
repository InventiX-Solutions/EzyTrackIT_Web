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
    public partial class JobType : System.Web.UI.Page
    {

        // public BLLJobType bllJobType = new BLLJobType();
        public BLLJobType bllJobType = new BLLJobType();

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

                    getJobTypeList();
                //    DerializeproductDataTable();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }

        }

        //private void DerializeproductDataTable()
        //{
        //    throw new NotImplementedException();
        //}

        //private void DerializeproductDataTable()
        //{
        //    try
        //    {

        //        DataTable dtLoadList = bllProduct.GetProductList(SessionMgr.DBName);
        //        ddlproduct.DataSource = dtLoadList;
        //        ddlproduct.DataTextField = "product_name";
        //        ddlproduct.DataValueField = "product_id";
        //        ddlproduct.DataBind();
        //        ddlproduct.Items.Insert(0, "Please Select");
        //    }
        //    catch (Exception ex)
        //    {
        //        AlertMsg(ex.Message);
        //    }

        //}

        private void getJobTypeList()
        {
            DataTable dt = new DataTable();
            dt = bllJobType.GetJobTypeList(SessionMgr.DBName);

            dt.Columns[1].ColumnName = "Code";
            dt.Columns[2].ColumnName = "Name";
            dt.AcceptChanges();
            if (dt.Rows.Count > 0)
            {
                gvJobType.DataSource = dt;
                gvJobType.DataBind();
                btnnew.Visible = false;

            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                gvJobType.DataSource = dt;
                gvJobType.DataBind();
                int TotalColumns = gvJobType.Rows[0].Cells.Count;
                gvJobType.Rows[0].Cells.Clear();
                gvJobType.Rows[0].Cells.Add(new TableCell());
                gvJobType.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                gvJobType.Rows[0].Cells[0].Text = "No Record Found";


                btnnew.Visible = true;
            }

        }
        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            e.Row.Cells[4].Visible = false;
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
                string lblid = lbljobtypeid.Text;
               // int productid = 0;
             //   string selprd = hdnprd.Value.ToString();

                //if (string.IsNullOrEmpty(selprd) )
                //{
                //  //  lblval.Text = "Please Select a Product";
                //    lblval.Visible = true;
                //    lblval.ForeColor = System.Drawing.Color.Red;
                //    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                //    return;
                //}
                //else
                //{
                // //   productid = Convert.ToInt32(selprd);
                //}
                if (!string.IsNullOrEmpty(lblid))
                {
                    id = Convert.ToInt32(lblid);
                }


                //if (ddlproduct.SelectedValue.ToString() == "0")
                //{
                //    lblval.Text = "Please Select a Product";
                //    lblval.Visible = true;
                //    lblval.ForeColor = System.Drawing.Color.Red;
                //    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                //    return;
                //}


                //if (id == 0)
                //{
                intExist = bllJobType.GetDuplicateExists(SessionMgr.CompanyID, "jobtype", "JobCode", txtbcode.Text.Trim().ToLower(), "JobTypeId", id, SessionMgr.DBName);
                if (intExist > 0)
                {
                    lblval.Text = "Code Already Exists";
                    lblval.Visible = true;
                    lblval.ForeColor = System.Drawing.Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
              //      ddlproduct.SelectedValue = productid.ToString();
                    return;

                }
                //}//Commented by Velu on 19-02-2020, Edit mode also must check the code duplicate




                BEJobType br = new BEJobType();
                br.JobTypeCode = txtbcode.Text.Trim();
                br.JobTypName = txtbname.Text.Trim();
               // br.productid = productid;
                br.JobTypeID = id;
                br.createdby = SessionMgr.UserID;
                br.modifiedby = SessionMgr.UserID;
                int insertorupdate = bllJobType.InsertOrUpdateRecord(br, SessionMgr.DBName);
                if (insertorupdate > 0)
                {
                    AlertMsg("Saved Successfully");

                }
                Response.Redirect("~/Masters/JobType.aspx?Saved=" + insertorupdate, false);
            }
            catch (Exception ex)
            {

                BeCommon.FormName = "JobType";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BLLCommon.UpdateLog(BeCommon, SessionMgr.DBName);
            }

        }
        protected void Display(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvJobType.Rows[rowIndex];

            lblval.Text = string.Empty;

            lbljobtypeid.Text = (row.FindControl("lblJobTypeid") as Label).Text;
            txtbcode.Text = (row.FindControl("lblcode") as Label).Text; ;
            txtbname.Text = (row.FindControl("lblname") as Label).Text;
       //     ddlproduct.ClearSelection();
      //      ddlproduct.Items.FindByText((row.FindControl("lblprdname") as Label).Text).Selected = true;
       //     hdnprd.Value = ddlproduct.SelectedValue.ToString();

            //ddlproduct.SelectedItem.Text = (row.FindControl("lblprdname") as Label).Text;

            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "updateselectedval();", true);
        }
        protected void Delete(object sender, EventArgs e)
        {
            try

            {
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                GridViewRow row = gvJobType.Rows[rowIndex];
                lbljobtypeid.Text = (row.FindControl("lblJobTypeid") as Label).Text;
                int JobTypeID = Convert.ToInt32(lbljobtypeid.Text);
                int deleteJobType = bllJobType.DeleteJobType(JobTypeID, SessionMgr.UserID, SessionMgr.DBName);
                if (deleteJobType > 0)
                {
                    AlertMsg("Record Deleted");
                    lbljobtypeid.Text = string.Empty;
                }
                getJobTypeList();
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