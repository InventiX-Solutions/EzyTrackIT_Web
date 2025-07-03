using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TCC_CRM.Masters
{
    public partial class NewEngineer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void OnbtnBackClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Transactions/Ticketlist.aspx", false);
        }
        protected void OnBtnSaveClick(object sender, EventArgs e)
        {
        }
    }
}