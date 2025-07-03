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
    public partial class EngineerSkillLevel : System.Web.UI.Page
    {
        public BLLEngineerSkillLevel bllEngineerSkillLevel = new BLLEngineerSkillLevel();
      
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionMgr.DBName))
                {
                    getskilllevellist();
                   
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        private void getskilllevellist()
        {
            DataTable dt = new DataTable();
            dt = bllEngineerSkillLevel.GetEngineerSkillLevelList(SessionMgr.DBName);

            dt.Columns[1].ColumnName = "Code";
            dt.Columns[2].ColumnName = "Name";
            dt.AcceptChanges();
            if (dt.Rows.Count > 0)
            {
                gvengineerskilllevel.DataSource = dt;
                gvengineerskilllevel.DataBind();
                btnnew.Visible = false;

            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                gvengineerskilllevel.DataSource = dt;
                gvengineerskilllevel.DataBind();
                int TotalColumns = gvengineerskilllevel.Rows[0].Cells.Count;
                gvengineerskilllevel.Rows[0].Cells.Clear();
                gvengineerskilllevel.Rows[0].Cells.Add(new TableCell());
                gvengineerskilllevel.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                gvengineerskilllevel.Rows[0].Cells[0].Text = "No Record Found";


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
                string lblid = lblskilllevelid.Text;
               

               
                if (!string.IsNullOrEmpty(lblid))
                {
                    id = Convert.ToInt32(lblid);
                }
               
                //if (id == 0)
                //{
                    intExist = bllEngineerSkillLevel.GetDuplicateExists(SessionMgr.CompanyID, "engineerskilllevel", "EngineerSkilllevelCode", txtskillcode.Text.Trim().ToLower(), "EngineerSkilllevelID", id, SessionMgr.DBName);
                    if (intExist > 0)
                    {
                        lblval.Text = "Code Already Exists";
                        lblval.Visible = true;
                        lblval.ForeColor = System.Drawing.Color.Red;
                        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                       
                        return;

                    }
                //}
                    //Commented by Velu on 19-02-2020, Edit mode also must check the code duplicate
                BEEngineerSkilllevel br = new BEEngineerSkilllevel();
                br.EngineerSkilllevelCode = txtskillcode.Text.Trim();
                br.EngineerSkilllevelName = txtskillname.Text.Trim();
              
                br.EngineerSkilllevelID = id;
                br.createdby = SessionMgr.UserID;
                br.modifiedby = SessionMgr.UserID;
                int insertorupdate = bllEngineerSkillLevel.InsertOrUpdateRecord(br, SessionMgr.DBName);
                if (insertorupdate > 0)
                {
                    AlertMsg("Saved Successfully");

                }
                Response.Redirect("~/Masters/EngineerSkillLevel.aspx?Saved=" + insertorupdate, false);
            }
            catch (Exception ex)
            {

                BeCommon.FormName = "EngineerSkillLevelList";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BLLCommon.UpdateLog(BeCommon, SessionMgr.DBName);
            }

        }
        protected void Display(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvengineerskilllevel.Rows[rowIndex];

            lblval.Text = string.Empty;
            lblskilllevelid.Text = (row.FindControl("lblEngineerSkilllevel_ID") as Label).Text;
            txtskillcode.Text = (row.FindControl("lblcode") as Label).Text;
            txtskillname.Text = (row.FindControl("lblname") as Label).Text;
           
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "updateselectedval();", true);
        }
        protected void Delete(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                GridViewRow row = gvengineerskilllevel.Rows[rowIndex];
                lblskilllevelid.Text = (row.FindControl("lblEngineerSkilllevel_ID") as Label).Text;
                int levelID = Convert.ToInt32(lblskilllevelid.Text);
                int deleteskilllevel = bllEngineerSkillLevel.DeleteBrand(levelID, SessionMgr.UserID, SessionMgr.DBName);
                if (deleteskilllevel > 0)
                {
                    AlertMsg("Record Deleted");
                    lblskilllevelid.Text = string.Empty;
                }
                getskilllevellist();
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