using System;
using TrackIT.ClassModules;

namespace TCC_CRM.MasterTemplate
{
    public partial class TCC_CRM : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionMgr.ClientCode))
                {
                    profiles.InnerHtml = "<img id=\"profiles\" src=" + SessionMgr.Photo + " alt=\"user\" runat=\"server\">";
                    smallProfile.InnerHtml = "<img src=" + SessionMgr.Photo + " alt=\"user\" class=\"profile-pic\" style=\"margin-top:-10px\"/>";
                    companyimg.InnerHtml = "<img id=\"cmpprof\" src=" + SessionMgr.CompanyLogo1 + "  runat=\"server\" style=\"width: 90px; height: 45px;\">";

                    //homediv.InnerHtml = "<p id=\"comp\" style=\"Color:#fff\">" + SessionMgr.CompanyName + "<span>Inventix-Service Desk Version 1.0</span> </p>";

                    liNotification.Visible = false;
                    liVehicle.Visible = false;
                    liMileageReport.Visible = false;

                    if (SessionMgr.ClientCode != "TCC")
                    {
                        liNotification.Visible = true;
                        liVehicle.Visible = true;
                        liMileageReport.Visible = true;
                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
    }
}