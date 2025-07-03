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
    public partial class SkillList : System.Web.UI.Page
    {
        public BLLSkill bllskill = new BLLSkill();
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionMgr.DBName))
                {
                    GetSkillList();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        private void GetSkillList()
        {
            DataTable dt = new DataTable();
            dt = bllskill.GetSkillList(SessionMgr.DBName);

            dt.Columns[1].ColumnName = "Code";
            dt.Columns[2].ColumnName = "Name";
            dt.AcceptChanges();
            if (dt.Rows.Count > 0)
            {
                gvSkilllist.DataSource = dt;
                gvSkilllist.DataBind();
                btnnew.Visible = false;

            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                gvSkilllist.DataSource = dt;
                gvSkilllist.DataBind();
                int TotalColumns = gvSkilllist.Rows[0].Cells.Count;
                gvSkilllist.Rows[0].Cells.Clear();
                gvSkilllist.Rows[0].Cells.Add(new TableCell());
                gvSkilllist.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                gvSkilllist.Rows[0].Cells[0].Text = "No Record Found";


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
                string lblid = lblSkillid.Text;
                if (!string.IsNullOrEmpty(lblid))
                {
                    id = Convert.ToInt32(lblid);
                }
                //if (id == 0)
                //{
                    intExist = bllskill.GetDuplicateExists(SessionMgr.CompanyID, "engineerskill", "SkillCode", txtcode.Text.Trim().ToLower(), "SkillID", id, SessionMgr.DBName);
                    if (intExist > 0)
                    {
                        lblval.Text = "Code Already Exists";
                        lblval.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                        return;

                    }
                //}
                    //Commented by Velu on 19-02-2020, Edit mode also must check the code duplicate
                BESkill br = new BESkill();
                br.SkillCode = txtcode.Text.Trim();
                br.SkillName = txtname.Text.Trim();
                br.SkillID = id;
                br.createdby = SessionMgr.UserID;
                br.modifiedby = SessionMgr.UserID;
                br.CompanyID = SessionMgr.UserID;
                int insertorupdate = bllskill.InsertOrUpdateRecord(br, SessionMgr.DBName);
                if (insertorupdate > 0)
                {
                    AlertMsg("Saved Successfully");
                    GetSkillList();

                }
                //Response.Redirect("~/Masters/BrandList.aspx?Saved=" + insertorupdate, false);
            }
            catch (Exception ex)
            {

                BeCommon.FormName = "SkillList";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BLLCommon.UpdateLog(BeCommon, SessionMgr.DBName);
            }

        }
        protected void Display(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvSkilllist.Rows[rowIndex];

            lblval.Text = string.Empty;
            lblSkillid.Text = (row.FindControl("lblSkill_ID") as Label).Text;
            txtcode.Text = (row.FindControl("lblcode") as Label).Text; ;
            txtname.Text = (row.FindControl("lblname") as Label).Text;
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
        }
        protected void Delete(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                GridViewRow row = gvSkilllist.Rows[rowIndex];
                lblSkillid.Text = (row.FindControl("lblSkill_ID") as Label).Text;
                int SkillID = Convert.ToInt32(lblSkillid.Text);
                int deleteskill = bllskill.DeleteEngineer(SkillID, SessionMgr.UserID, SessionMgr.DBName);
                if (deleteskill > 0)
                {
                    AlertMsg("Record Deleted");
                    GetSkillList();
                    lblSkillid.Text = string.Empty;
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