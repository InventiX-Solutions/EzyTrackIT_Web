using CRM.Artifacts;
using CRM.Artifacts.Masters;
using CRM.Bussiness;
using CRM.Bussiness.Masters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrackIT.ClassModules;

namespace TCC_CRM.Transactions
{
    public partial class TicketPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SessionMgr.ClientCode))
            {
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        [WebMethod()]
        public static BETicket TicketEdit(int TicketID)
        {
            BETicket tkt = new BETicket();
            BLLTicket bllTicket = new BLLTicket();
            tkt = bllTicket.GetTicket(SessionMgr.CompanyID, TicketID, SessionMgr.DBName);
            string brcode = tkt.TicketNo.ToString();
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            using (Bitmap bitMap = new Bitmap(tkt.TicketNo.ToString().Length * 20, 80))
            {
                using (Graphics graphics = Graphics.FromImage(bitMap))
                {
                    Font oFont = new Font("IDAutomationHC39M", 11);
                    PointF point = new PointF(2f, 2f);
                    SolidBrush blackBrush = new SolidBrush(Color.Black);
                    SolidBrush whiteBrush = new SolidBrush(Color.White);
                    graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                   
                    graphics.DrawString("*" + tkt.TicketNo.ToString() + "*", oFont, blackBrush, point);
                    //graphics.DrawString( tkt.TicketNo.ToString(), oFont, blackBrush, point);
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();


                    Convert.ToBase64String(byteImage);
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    tkt.barcodeimage = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
               
            } 
            return tkt;
        }
    }
}