using ClosedXML.Excel;
using CRM.Artifacts;
using CRM.Business;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackIT.ClassModules;
using System.Diagnostics;
using System.Reflection;
using System.Web.Services;
using System.Xml;
using System.Configuration;
using CRM.Bussiness;

namespace TCC_CRM.Masters
{
    public partial class NotificationTemplateList : System.Web.UI.Page
    {
        public BLLCommon bll = new BLLCommon();
        public Common cm = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SessionMgr.ClientCode))
            {
                //HiddenField hfIdType = (HiddenField)Page.Master.FindControl("hdnSaveSuccessfully");
                if (!IsPostBack)
                {
                   // cm.ActivityLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name);
                }
                getNotificationTemplateList();
              
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
           
                
            
        }

        private void getNotificationTemplateList()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = bll.getDefaultList(SessionMgr.DBName, SessionMgr.CompanyID.ToString(), "NotificationTemplateList");
                ASPxGridView1.DataSource = dt;
                ASPxGridView1.DataBind();
               // GridLanguageLoad();
              //  ASPxGridView1.Columns["WhatsappFlag"].Visible = false;
                //if (SessionMgr.UserName.ToUpper() == "PeritusAdmin".ToUpper())
                //{
                //    ASPxGridView1.Columns["WhatsappFlag"].Visible = true;

                //}
            }
            catch (Exception ex)
            {
            //    cm.ErrorLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.GetType().ToString(), ex.Source.ToString(), SessionMgr.ClientCode);
            }
        }

        //private void GridLanguageLoad()
        //{
        //    try
        //    {
        //        foreach (XmlNode node in Common.XmlRead(SessionMgr.Language, "NotificationTemplateMaster"))
        //        {
        //            ASPxGridView1.Columns["Template_ID"].Caption = Common.LoadLanguageValue(node.SelectSingleNode("gridNotificationTemplateID").InnerText, "Action");
        //            ASPxGridView1.Columns["TemplateType"].Caption = Common.LoadLanguageValue(node.SelectSingleNode("gridNotificationTemplateType").InnerText, "Type");
        //            ASPxGridView1.Columns["EmailFlag"].Caption = Common.LoadLanguageValue(node.SelectSingleNode("gridNotificationTemplateEmailFlag").InnerText, "Email");
        //            ASPxGridView1.Columns["SMSFlag"].Caption = Common.LoadLanguageValue(node.SelectSingleNode("gridNotificationTemplateSMSFlag").InnerText, "SMS");
        //          //  ASPxGridView1.Columns["WhatsappFlag"].Caption = Common.LoadLanguageValue(node.SelectSingleNode("gridNotificationTemplateWhatsappFlag").InnerText, "Whatsapp");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //       // cm.ErrorLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.GetType().ToString(), ex.Source.ToString(), SessionMgr.ClientCode);
        //    }
        //}

        protected void excelexport_Click(object sender, EventArgs e)
        {
            try
            {
              //  cm.ActivityLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name);
                DataTable resultdt = bll.getDefaultList(SessionMgr.ClientCode, SessionMgr.CompanyID.ToString(), "NotificationTemplateList");
                string Filename = "NotificationTemplate";
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add(resultdt, "Sheet1");
                ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                ws.Columns().AdjustToContents();
                ws.Tables.FirstOrDefault().ShowRowStripes = false;
                //wb.SaveAs("E:\\Error_Download.xlsx");
                string datestring = DateTime.Now.ToString("dd-MM-yyyy");
                string myName = Server.UrlEncode(Filename + "_" + datestring + ".xlsx");
                MemoryStream stream = GetStream(wb);// The method is defined below
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition",
                "attachment; filename=" + myName);
                Response.ContentType = "application/vnd.ms-excel";
                Response.BinaryWrite(stream.ToArray());
                Response.End();
            }
            catch (Exception ex)
            {
               // cm.ErrorLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.GetType().ToString(), ex.Source.ToString(), SessionMgr.ClientCode);
            }
        }

        public MemoryStream GetStream(XLWorkbook excelWorkbook)
        {
            MemoryStream fs = new MemoryStream();
            excelWorkbook.SaveAs(fs);
            fs.Position = 0;
            return fs;
        }

        [WebMethod()]
        public static string DeleteNotificationTemplate(string idval)
        {
            Common cm = new Common();
            string result = string.Empty;
            try
            {
              
               
              //  cm.ActivityLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name);
                BLLCommon bll = new BLLCommon();
                int res = bll.DeleteMasters("NotificationTemplate", "Template_ID", Convert.ToInt32(idval), SessionMgr.CompanyID, SessionMgr.UserID, SessionMgr.ClientCode);
                if (res > 0)
                {
                    result = "Success";
                }
                else
                {
                    result = "Failure";
                }
            }
            catch (Exception ex)
            {
             //   cm.ErrorLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.GetType().ToString(), ex.Source.ToString(), SessionMgr.ClientCode);
                result = "Failure";
            }
            return result;
        }

        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        foreach (XmlNode node in Common.XmlRead((string.IsNullOrEmpty(SessionMgr.Language)) ? "en_GB" : SessionMgr.Language, "NotificationTemplateMaster"))
        //        {
        //            spnNotificationTemplateHeader.InnerText = Common.LoadLanguageValue(node.SelectSingleNode("spnNotificationTemplateHeader").InnerText, "Notification Template");
        //            spnNotificationTemplateToExcel.InnerText = Common.LoadLanguageValue(node.SelectSingleNode("spnNotificationTemplateToExcel").InnerText, "Export To Excel");
        //            spnNotificationTemplateNewNotificationTemplate.InnerText = Common.LoadLanguageValue(node.SelectSingleNode("spnNotificationTemplateNewNotificationTemplate").InnerText, "New Template");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //      //  cm.ErrorLog(Convert.ToString(HttpContext.Current.Request.Url.AbsoluteUri), MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.GetType().ToString(), ex.Source.ToString(), SessionMgr.ClientCode);
        //    }
        //}
    }
}