using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Bussiness;
using DevExpress.Web;
using TrackIT.ClassModules;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;

namespace TCC_CRM.Transactions
{
    public partial class Tracking : System.Web.UI.Page
    {
        public BLLCommon m_BLLCommon = new BLLCommon();


        //if (!IsPostBack)
        //{
        //    if (!string.IsNullOrEmpty(SessionMgr.DBName))
        //    {
        //    }
        //    else
        //    {
        //        Response.Redirect("~/Login.aspx");
        //    }
        //}




        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //loaddefaultvalues();
                    DateTime dt = DateTime.Now;
                    ASPxDateEdit1.Value = dt.AddDays(-1);
                    ASPxDateEdit2.Value = dt.AddDays(1);

                    LoadDropdown();
                }
                bindgridview();
            }
            catch (Exception ex)
            {
                try
                {

                }
                catch (Exception ew)
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        private void LoadDropdown()
        {
            DataTable dds = m_BLLCommon.GetEngineerDetails(SessionMgr.DBName);
            ASPxListBox cuslistss = ((ASPxListBox)ASPxDropDownEdit_Product.FindControl("ListBoxLocation"));
            cuslistss.DataSource = dds;
            cuslistss.TextField = "Name";
            cuslistss.ValueField = "Value";
            cuslistss.DataBind();
        }

        //protected void Image_Init(object sender, EventArgs e)
        //{
        //    ASPxImage image = sender as ASPxImage;
        //    GridViewDataItemTemplateContainer container = image.NamingContainer as GridViewDataItemTemplateContainer;
        //    string base64str = ASPxGridView1.GetRowValues(container.VisibleIndex, "Intime_img").ToString();
        //    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64str)))
        //    {
        //        byte[] byteImage = ms.ToArray();
        //        image.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
        //    }
        //}

        //private void loaddefaultvalues()
        //{
        //    DataTable dt = new DataTable();
        //    dt = bllcommon.GetDropDownValues("EmployeeName", "EmployeeID", "EmployeeMaster ", string.Empty);
        //    ddlemp.DataSource = dt;
        //    ddlemp.DataTextField = "Name";
        //    ddlemp.DataValueField = "Value";
        //    ddlemp.DataBind();
        //    ddlemp.Items.Insert(0, "Please Select");
        //}

        private void bindgridview()
        {
            string StartTime = Convert.ToDateTime(ASPxDateEdit1.Value).ToString("dd/MM/yyyy");
            string FinishTime = Convert.ToDateTime(ASPxDateEdit2.Value).ToString("dd/MM/yyyy");
            //string userid = ddlemp.SelectedValue.ToString();
            string userid = string.Empty;
            ASPxListBox Locationlist = ((ASPxListBox)ASPxDropDownEdit_Product.FindControl("ListBoxLocation"));
            if (Locationlist.SelectedItems.Count > 0)
            {

                for (int i = 0; i < Locationlist.SelectedItems.Count; i++)
                {

                    int strselecteditems = Convert.ToInt32(Locationlist.SelectedItems[i].Value.ToString());
                    if (!string.IsNullOrEmpty(userid))
                    {
                        userid = userid + "','" + strselecteditems;
                    }
                    else
                    {
                        userid = strselecteditems.ToString();
                    }
                }
            }
            else
            {
                userid = string.Empty;
            }



            DataTable dts = m_BLLCommon.GetClosedJobMapList(StartTime, FinishTime, userid, SessionMgr.DBName);
            ASPxGridView1.DataSource = dts;
            ASPxGridView1.KeyFieldName = "TicketID";
            ASPxGridView1.DataBind();
        }

        protected void btn_go_click(object sender, EventArgs e)
        {
            bindgridview();
        }
        //private void bindgridview()
        //{
        //    string userid = string.Empty;
        //    ASPxListBox Locationlist = ((ASPxListBox)ASPxDropDownEdit_Product.FindControl("ListBoxLocation"));
        //    if (Locationlist.SelectedItems.Count > 0)
        //    {

        //        for (int i = 0; i < Locationlist.SelectedItems.Count; i++)
        //        {

        //            int strselecteditems = Convert.ToInt32(Locationlist.SelectedItems[i].Value.ToString());
        //            if (!string.IsNullOrEmpty(userid))
        //            {
        //                userid = userid + "','" + strselecteditems;
        //            }
        //            else
        //            {
        //                userid = strselecteditems.ToString();
        //            }
        //        }
        //        //var url = string.Empty;
        //        //var iframe = string.Empty;
        //        //MySqlDataReader dr = m_BLLCommon.GetMap(userid, SessionMgr.DBName);
        //        //if (dr.Read())
        //        //{
        //        //    url = "https://www.google.com/maps/embed/v1/place?key=AIzaSyB_KRurVRkcQ7TpyXf2-m3-4Oef6N-s2IY&q=" + dr["Intime_Lat"].ToString() + "," + dr["Intime_Long"].ToString() + "";
        //        //    iframe = "<iframe width='1200'  height='450' frameborder='0' style='border:0' src='" + url + "' allowfullscreen></iframe>'";

        //        //}
        //        //Map.InnerHtml = iframe;
        //    }

        //    else
        //    {
               
        //    }
           
        //}
    }
}