using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackIT.ClassModules;
using CRM.Data.Masters;
using CRM.Bussiness.Masters;
using CRM.Artifacts.Masters;
using CRM.Bussiness;
using CRM.Artifacts;
using System;

namespace TCC_CRM.Masters
{
    public partial class severity : System.Web.UI.Page
    {
        public BLLSeverity bllSeverity = new BLLSeverity();
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionMgr.DBName))
                {
                    //lblval.Visible = false;
                    getSeveritylist();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        private void getSeveritylist()
        {
            DataTable dt = new DataTable();
            dt = bllSeverity.GetSeverityList(SessionMgr.DBName);

            dt.Columns[1].ColumnName = "Code";
            dt.Columns[2].ColumnName = "Name";
            dt.AcceptChanges();
            if (dt.Rows.Count > 0)
            {
                gvSeveritylist.DataSource = dt;
                gvSeveritylist.DataBind();
                btnnew.Visible = false;

            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                gvSeveritylist.DataSource = dt;
                gvSeveritylist.DataBind();
                int TotalColumns = gvSeveritylist.Rows[0].Cells.Count;
                gvSeveritylist.Rows[0].Cells.Clear();
                gvSeveritylist.Rows[0].Cells.Add(new TableCell());
                gvSeveritylist.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                gvSeveritylist.Rows[0].Cells[0].Text = "No Record Found";


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
                string lblid = lblSevid.Text;
                if (!string.IsNullOrEmpty(lblid))
                {
                    id = Convert.ToInt32(lblid);
                }
                //if (id == 0)
                //{
                    intExist = bllSeverity.GetDuplicateExists(SessionMgr.CompanyID, "severity", "sevCode", txtscode.Text.Trim().ToLower(), "sevID", id, SessionMgr.DBName);
                    if (intExist > 0)
                    {
                        lblval.Text = "Code Already Exists";
                        lblval.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                        return;

                    }
                //}
                    //Commented by Velu on 19-02-2020, Edit mode also must check the code duplicate
                BESeverity br = new BESeverity();
                br.sevCode = txtscode.Text.Trim();
                br.sevName = txtsname.Text.Trim();
                br.sevID = id;
                br.CreatedBy = SessionMgr.UserID;
                br.ModifiedBy = SessionMgr.UserID;
                int insertorupdate = bllSeverity.InsertOrUpdateRecord(br, SessionMgr.DBName);
                if (insertorupdate > 0)
                {
                    AlertMsg("Saved Successfully");

                }
                Response.Redirect("~/Masters/Severity.aspx?Saved=" + insertorupdate, false);
            }
            catch (Exception ex)
            {

                BeCommon.FormName = "SeverityList";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BLLCommon.UpdateLog(BeCommon, SessionMgr.DBName);
            }

        }
        protected void Display(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvSeveritylist.Rows[rowIndex];

            lblval.Text = string.Empty;
            lblSevid.Text = (row.FindControl("lblsev_id") as Label).Text;
            txtscode.Text = (row.FindControl("lblcode") as Label).Text; ;
            txtsname.Text = (row.FindControl("lblname") as Label).Text;
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
        }
        protected void Delete(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                GridViewRow row = gvSeveritylist.Rows[rowIndex];
                lblSevid.Text = (row.FindControl("lblsev_id") as Label).Text;
                int sevID = Convert.ToInt32(lblSevid.Text);
                int deleteSeverity = bllSeverity.DeleteSeverity(sevID, SessionMgr.UserID, SessionMgr.DBName);
                if (deleteSeverity > 0)
                {
                    AlertMsg("Record Deleted");
                    lblSevid.Text = string.Empty;
                } 
                getSeveritylist();
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