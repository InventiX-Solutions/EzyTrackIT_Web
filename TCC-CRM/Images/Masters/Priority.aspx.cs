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
    public partial class Priority : System.Web.UI.Page
    {
        public BLLPriority bllPriority = new BLLPriority();
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionMgr.DBName))
                {
                    //lblval.Visible = false;
                    getPrioritylist();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        private void getPrioritylist()
        {
            DataTable dt = new DataTable();
            dt = bllPriority.GetPriorityList(SessionMgr.DBName);

            dt.Columns[1].ColumnName = "Code";
            dt.Columns[2].ColumnName = "Name";
            dt.AcceptChanges();
            if (dt.Rows.Count > 0)
            {
                gvPrioritylist.DataSource = dt;
                gvPrioritylist.DataBind();
                btnnew.Visible = false;

            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                gvPrioritylist.DataSource = dt;
                gvPrioritylist.DataBind();
                int TotalColumns = gvPrioritylist.Rows[0].Cells.Count;
                gvPrioritylist.Rows[0].Cells.Clear();
                gvPrioritylist.Rows[0].Cells.Add(new TableCell());
                gvPrioritylist.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                gvPrioritylist.Rows[0].Cells[0].Text = "No Record Found";


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
                string lblid = lblPrid.Text;
                if (!string.IsNullOrEmpty(lblid))
                {
                    id = Convert.ToInt32(lblid);
                }
                //if (id == 0)
                //{
                    intExist = bllPriority.GetDuplicateExists(SessionMgr.CompanyID, "priority", "prCode", txtpcode.Text.Trim().ToLower(), "prID", id, SessionMgr.DBName);
                    if (intExist > 0)
                    {
                        lblval.Text = "Code Already Exists";
                        lblval.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                        return;

                    }
                    //} //Commented by Velu on 19-02-2020, Edit mode also must check the code duplicate
                BEPriority br = new BEPriority();
                br.prCode = txtpcode.Text.Trim();
                br.prName = txtpname.Text.Trim();
                br.prID = id;
                br.CreatedBy = SessionMgr.UserID;
                br.ModifiedBy = SessionMgr.UserID;
                int insertorupdate = bllPriority.InsertOrUpdateRecord(br, SessionMgr.DBName);
                if (insertorupdate > 0)
                {
                    AlertMsg("Saved Successfully");

                }
                Response.Redirect("~/Masters/Priority.aspx?Saved=" + insertorupdate, false);
            }
            catch (Exception ex)
            {

                BeCommon.FormName = "PriorityList";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BLLCommon.UpdateLog(BeCommon, SessionMgr.DBName);
            }

        }
        protected void Display(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvPrioritylist.Rows[rowIndex];

            lblval.Text = string.Empty;
            lblPrid.Text = (row.FindControl("lblpr_id") as Label).Text;
            txtpcode.Text = (row.FindControl("lblcode") as Label).Text; ;
            txtpname.Text = (row.FindControl("lblname") as Label).Text;
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
        }
        protected void Delete(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                GridViewRow row = gvPrioritylist.Rows[rowIndex];
                lblPrid.Text = (row.FindControl("lblpr_id") as Label).Text;
                int sevID = Convert.ToInt32(lblPrid.Text);
                int deletePriority = bllPriority.DeletePriority(sevID, SessionMgr.UserID, SessionMgr.DBName);
                if (deletePriority > 0)
                {
                    AlertMsg("Record Deleted");
                    lblPrid.Text = string.Empty;
                }
                getPrioritylist();
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