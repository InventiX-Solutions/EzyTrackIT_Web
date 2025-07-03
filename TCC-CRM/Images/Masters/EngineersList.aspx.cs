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
    public partial class Engineers : System.Web.UI.Page
    {
        public BLLEngineer bllengineer = new BLLEngineer();
        public BLLCommon m_BLLCommon = new BLLCommon();
        public BECommom BeCommon = new BECommom();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionMgr.DBName))
                {
                    lblval.Text = string.Empty;
                    lblval.Visible = false; 
                 
                    getengineerlist();
                    DerializeSkillDataTable();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        private void DerializeSkillDataTable()
        {
            try
            {
                DataTable dtLoadList = bllengineer.GetSkilllist(SessionMgr.DBName);
                cmbskill.DataSource = dtLoadList;
                cmbskill.DataTextField = "SkillName";
                cmbskill.DataValueField = "SkillID";
                cmbskill.DataBind();
                cmbskill.Items.Insert(0, "Please Select");
            }
            catch (Exception ex)
            {
                AlertMsg(ex.Message);
            }

        }
        private void getengineerlist()
        {
            DataTable dt = new DataTable();
            dt = bllengineer.GetengineerList(SessionMgr.DBName);

            dt.Columns[1].ColumnName = "Code";
            dt.Columns[2].ColumnName = "Name";
            dt.Columns[4].ColumnName = "MobileNo";
            dt.Columns[5].ColumnName = "EmailID";
            dt.AcceptChanges();
            if (dt.Rows.Count > 0)
            {
                gvEngineerlist.DataSource = dt;
                gvEngineerlist.DataBind();
                btnnew.Visible = false;

            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                gvEngineerlist.DataSource = dt;
                gvEngineerlist.DataBind();
                int TotalColumns = gvEngineerlist.Rows[0].Cells.Count;
                gvEngineerlist.Rows[0].Cells.Clear();
                gvEngineerlist.Rows[0].Cells.Add(new TableCell());
                gvEngineerlist.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                gvEngineerlist.Rows[0].Cells[0].Text = "No Record Found";


                btnnew.Visible = true;
            }
            
        }
        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[14].Visible = false;
           // e.Row.Cells[10].Visible = false;
           // e.Row.Cells[9].Visible = false;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int intExist = 0;
                int id = 0;
                string lblid = lblengineerid.Text;
                if (!string.IsNullOrEmpty(lblid))
                {
                    id = Convert.ToInt32(lblid);
                }
                //if (id == 0)
                //{
                    intExist = bllengineer.GetDuplicateExists(SessionMgr.CompanyID, "engineers", "engineer_code", txtecode.Text.Trim().ToLower(), "engineer_id", id, SessionMgr.DBName);
                    if (intExist > 0)
                    {
                        lblval.Text = "Code Already Exists";
                        lblval.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
                        return;

                    }
                    //} //Commented by Velu on 19-02-2020, Edit mode also must check the code duplicate

                BEEngineer br = new BEEngineer();
                br.EngineerCode = txtecode.Text.Trim();
                br.EngineerName = txtename.Text.Trim();
                br.MobileNo = txtmno.Text.Trim();
                br.EmailID = txtemailid.Text.Trim();
                br.EngineerID = id;
                br.password = txtpassword.Text;
                br.SkillID = Convert.ToInt32(cmbskill.SelectedValue);
                br.createdby = SessionMgr.UserID;
                br.modifiedby = SessionMgr.UserID;
                int insertorupdate = bllengineer.InsertOrUpdateRecord(br, SessionMgr.DBName);
                if (insertorupdate > 0)
                {
                    AlertMsg("Saved Successfully");
                    getengineerlist();

                }
                //Response.Redirect("~/Masters/BrandList.aspx?Saved=" + insertorupdate, false);
            }
            catch (Exception ex)
            {

                BeCommon.FormName = "EngineerList";
                BeCommon.ErrorDescription = ex.Message;
                BeCommon.ErrorType = ex.Message;
                BeCommon.CreatedDate = DateTime.Now;

                m_BLLCommon.UpdateLog(BeCommon, SessionMgr.DBName);
            }

        }
        protected void Display(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvEngineerlist.Rows[rowIndex];

            lblval.Text = string.Empty;

            lblengineerid.Text = (row.FindControl("lblengineer_id") as Label).Text;
            txtecode.Text = (row.FindControl("lblcode") as Label).Text; 
            txtename.Text = (row.FindControl("lblname") as Label).Text;
            txtmno.Text = (row.FindControl("lblmno") as Label).Text;
            
            txtemailid.Text = (row.FindControl("lblemailid") as Label).Text;
            cmbskill.ClearSelection();
            cmbskill.Items.FindByText((row.FindControl("lblskillname") as Label).Text).Selected=true;
            txtpassword.Text = (row.FindControl("lblpassword") as Label).Text;
            ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal();", true);
        }
        protected void Delete(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
                GridViewRow row = gvEngineerlist.Rows[rowIndex];
                lblengineerid.Text = (row.FindControl("lblengineer_id") as Label).Text;
                int EngineerID = Convert.ToInt32(lblengineerid.Text);
                int deleteengineer = bllengineer.DeleteEngineer(EngineerID, SessionMgr.UserID, SessionMgr.DBName);
                if (deleteengineer > 0)
                {
                    AlertMsg("Record Deleted");
                    lblengineerid.Text = string.Empty;
                    getengineerlist();
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