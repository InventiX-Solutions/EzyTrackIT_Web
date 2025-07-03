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
    public partial class ProblemList : System.Web.UI.Page
    {
        public BLLProblem bllProblem = new BLLProblem();
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionMgr.DBName))
                {
                    getProblemlist();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        private void getProblemlist()
        {
            DataTable dt = new DataTable();
            dt = bllProblem.GeProblemList(SessionMgr.DBName);

            dt.Columns[1].ColumnName = "Code";
            dt.Columns[2].ColumnName = "Name";
            dt.AcceptChanges();
            if (dt.Rows.Count > 0)
            {
                gvProblemlist.DataSource = dt;
                gvProblemlist.DataBind();
                btnnew.Visible = false;

            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                gvProblemlist.DataSource = dt;
                gvProblemlist.DataBind();
                int TotalColumns = gvProblemlist.Rows[0].Cells.Count;
                gvProblemlist.Rows[0].Cells.Clear();
                gvProblemlist.Rows[0].Cells.Add(new TableCell());
                gvProblemlist.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                gvProblemlist.Rows[0].Cells[0].Text = "No Record Found";


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
                string lblid = lblProblemid.Text;
                if (!string.IsNullOrEmpty(lblid))
                {
                    id = Convert.ToInt32(lblid);
                }
                //if (id == 0)
                //{
                    intExist = bllProblem.GetDuplicateExists(SessionMgr.CompanyID, "Problems", "Problem_code", txtbcode.Text.Trim().ToLower(), "Problem_id", id, SessionMgr.DBName);
                    if (intExist > 0)
                    {
                        lblval.Text = "Code Already Exists";
                        lblval.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                        return;

                    }
                //}
                    //Commented by Velu on 19-02-2020, Edit mode also must check the code duplicate
                BEProblem br = new BEProblem();
                br.ProblemCode = txtbcode.Text.Trim();
                br.ProblemName = txtbname.Text.Trim();
                br.ProblemID = id;
                br.createdby = SessionMgr.UserID;
                br.modifiedby = SessionMgr.UserID;
                int insertorupdate = bllProblem.InsertOrUpdateRecord(br, SessionMgr.DBName);
                if (insertorupdate > 0)
                {
                    AlertMsg("Saved Successfully");
                   
                }
                Response.Redirect("~/Masters/ProblemList.aspx?Saved=" + insertorupdate, false);
            }
            catch (Exception ex)
            {

                BeCommon.FormName = "ProblemList";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BLLCommon.UpdateLog(BeCommon, SessionMgr.DBName);
            }

        }
        protected void Display(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvProblemlist.Rows[rowIndex];

            lblval.Text = string.Empty;
            lblProblemid.Text = (row.FindControl("lblProblem_id") as Label).Text;
            txtbcode.Text = (row.FindControl("lblcode") as Label).Text; ;
            txtbname.Text = (row.FindControl("lblname") as Label).Text;
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
        }
        protected void Delete(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                GridViewRow row = gvProblemlist.Rows[rowIndex];
                lblProblemid.Text = (row.FindControl("lblProblem_id") as Label).Text;
                int ProblemID = Convert.ToInt32(lblProblemid.Text);
                int deleteProblem = bllProblem.DeleteProblem(ProblemID, SessionMgr.UserID, SessionMgr.DBName);
                if (deleteProblem > 0)
                {
                    AlertMsg("Record Deleted");
                    lblProblemid.Text = string.Empty;
                }
                getProblemlist();
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