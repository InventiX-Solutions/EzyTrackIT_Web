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
    public partial class VehicleList : System.Web.UI.Page
    {
        public BLLVehicle bllVehicle = new BLLVehicle();
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionMgr.DBName))
                {
                    GetVehicleList();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        private void GetVehicleList()
        {
            DataTable dt = new DataTable();
            dt = bllVehicle.GetVehicleList(SessionMgr.DBName);

            dt.Columns[1].ColumnName = "Code";
            dt.Columns[2].ColumnName = "Name";
            dt.Columns[3].ColumnName = "VehicleNo";
            dt.Columns[4].ColumnName = "Remarks";
            dt.AcceptChanges();
            if (dt.Rows.Count > 0)
            {
                gvVehiclelist.DataSource = dt;
                gvVehiclelist.DataBind();
                btnnew.Visible = false;

            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                gvVehiclelist.DataSource = dt;
                gvVehiclelist.DataBind();
                int TotalColumns = gvVehiclelist.Rows[0].Cells.Count;
                gvVehiclelist.Rows[0].Cells.Clear();
                gvVehiclelist.Rows[0].Cells.Add(new TableCell());
                gvVehiclelist.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                gvVehiclelist.Rows[0].Cells[0].Text = "No Record Found";


                btnnew.Visible = true;
            }

        }
        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[6].Visible = false;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int intExist = 0;
                int id = 0;
                string lblid = lblVehicleid.Text;
                if (!string.IsNullOrEmpty(lblid))
                {
                    id = Convert.ToInt32(lblid);
                }
                //if (id == 0)
                //{
                    intExist = bllVehicle.GetDuplicateExists(SessionMgr.CompanyID, "engineerVehicle", "VehicleCode", txtcode.Text.Trim().ToLower(), "VehicleID", id, SessionMgr.DBName);
                    if (intExist > 0)
                    {
                        lblval.Text = "Code Already Exists";
                        lblval.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                        return;

                    }
                //}
                    //Commented by Velu on 19-02-2020, Edit mode also must check the code duplicate
                BEVehicle br = new BEVehicle();
                br.VehicleCode = txtcode.Text.Trim();
                br.VehicleName = txtname.Text.Trim();
                br.VehicleNo = txtvehicleno.Text.Trim();
                br.Remarks = txtremarks.Text.Trim();
                br.VehicleID = id;
                br.createdby = SessionMgr.UserID;
                br.modifiedby = SessionMgr.UserID;
                br.CompanyID = SessionMgr.UserID;
                int insertorupdate = bllVehicle.InsertOrUpdateRecord(br, SessionMgr.DBName);
                if (insertorupdate > 0)
                {
                    AlertMsg("Saved Successfully");
                    GetVehicleList();
                    lblVehicleid.Text = string.Empty;
                }
                //Response.Redirect("~/Masters/BrandList.aspx?Saved=" + insertorupdate, false);
            }
            catch (Exception ex)
            {

                BeCommon.FormName = "VehicleList";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BLLCommon.UpdateLog(BeCommon, SessionMgr.DBName);
            }

        }
        protected void Display(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvVehiclelist.Rows[rowIndex];

            lblval.Text = string.Empty;
            lblVehicleid.Text = (row.FindControl("lblVehicle_ID") as Label).Text;
            txtcode.Text = (row.FindControl("lblcode") as Label).Text; ;
            txtname.Text = (row.FindControl("lblname") as Label).Text;
            txtvehicleno.Text = (row.FindControl("lblvehicleno") as Label).Text;
            txtremarks.Text = (row.FindControl("lblremarks") as Label).Text;
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
        }
        protected void Delete(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                GridViewRow row = gvVehiclelist.Rows[rowIndex];
                lblVehicleid.Text = (row.FindControl("lblVehicle_ID") as Label).Text;
                int VehicleID = Convert.ToInt32(lblVehicleid.Text);
                int deleteVehicle = bllVehicle.DeleteVehicle(VehicleID, SessionMgr.UserID, SessionMgr.DBName);
                if (deleteVehicle > 0)
                {
                    AlertMsg("Record Deleted");
                    GetVehicleList();
                    lblVehicleid.Text = string.Empty;
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

            { ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "successalert('" + Msg + "');", true); }
        }
        #endregion
    }
}