
using CRM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nexus
{


    public partial class Home : System.Web.UI.Page
    {
        public static string IPAddress = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //IPAddress = ConfigurationManager.AppSettings["IPAddress"].ToString();

           
        }
    }

        

    }
