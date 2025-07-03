using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CRM.Artifacts;
using TrackIT.ClassModules;


namespace Nexus
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //SessionMgr.TokenNo = string.Empty;
                SessionMgr.UserID = 0;
                SessionMgr.ClientCode = string.Empty;
                //Session.Clear();
            }

        }
    }
}