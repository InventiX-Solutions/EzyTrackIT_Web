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
    public partial class ServiceTypeList : System.Web.UI.Page
    {
        public BLLServiceType bllserviceType = new BLLServiceType();
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
                    getServiceType();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }

        }
        private void getServiceType()
        {
            DataTable dt = new DataTable();
            dt = bllserviceType.GetServiceTypeList(SessionMgr.DBName);

            dt.Columns[1].ColumnName = "Code";
            dt.Columns[2].ColumnName = "Name";
            dt.AcceptChanges();
            if (dt.Rows.Count > 0)
            {
                gvstypelist.DataSource = dt;
                gvstypelist.DataBind();
                btnnew.Visible = false;

            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                gvstypelist.DataSource = dt;
                gvstypelist.DataBind();
                int TotalColumns = gvstypelist.Rows[0].Cells.Count;
                gvstypelist.Rows[0].Cells.Clear();
                gvstypelist.Rows[0].Cells.Add(new TableCell());
                gvstypelist.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                gvstypelist.Rows[0].Cells[0].Text = "No Record Found";


                btnnew.Visible = true;
            }

           
        }
        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[4].Visible = false;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int intExist = 0;
                int id = 0;
                string lblid = lblstypeid.Text;
                if (!string.IsNullOrEmpty(lblid))
                {
                    id = Convert.ToInt32(lblid);
                }

                //if (id == 0)
                //{
                    intExist = bllserviceType.GetDuplicateExists(SessionMgr.CompanyID, "service_type", "service_type_code", txtscode.Text.Trim().ToLower(), "service_typeid", id, SessionMgr.DBName);
                    if (intExist > 0)
                    {
                        lblval.Text = "Code Already Exists";
                        lblval.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                        return;

                    }
                //}
                //Commented by Velu on 19-02-2020, Edit mode also must check the code duplicate
                BEServiceType br = new BEServiceType();
                br.ServiceTypeCode = txtscode.Text.Trim();
                br.ServiceTypeName = txtsname.Text.Trim();
                br.ServiceTypeID = id;
                br.createdby = SessionMgr.UserID;
                br.modifiedby = SessionMgr.UserID;
                int insertorupdate = bllserviceType.InsertOrUpdateRecord(br, SessionMgr.DBName);
                if (insertorupdate > 0)
                {
                    AlertMsg("Saved Successfully");

                }
                Response.Redirect("~/Masters/ServiceTypeList.aspx?Saved=" + insertorupdate, false);
            }
            catch (Exception ex)
            {

                BeCommon.FormName = "ServiceTypeList";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BLLCommon.UpdateLog(BeCommon, SessionMgr.DBName);
            }

        }
        protected void Display(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvstypelist.Rows[rowIndex];

            lblval.Text = string.Empty;
            lblstypeid.Text = (row.FindControl("lblservice_typeid") as Label).Text;
            txtscode.Text = (row.FindControl("lblcode") as Label).Text; ;
            txtsname.Text = (row.FindControl("lblname") as Label).Text;
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
        }
        protected void Delete(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                GridViewRow row = gvstypelist.Rows[rowIndex];
                lblstypeid.Text = (row.FindControl("lblservice_typeid") as Label).Text;
                int service_typeid = Convert.ToInt32(lblstypeid.Text);
                int deletebrand = bllserviceType.DeleteServicetype(service_typeid, SessionMgr.UserID, SessionMgr.DBName);
                if (deletebrand > 0)
                {
                    AlertMsg("Record Deleted");
                    lblstypeid.Text = string.Empty;
                }
                getServiceType();
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
    }
}