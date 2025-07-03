using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace TrackIT.ClassModules
{
    public static class SessionMgr
    {
        public static string Photo
        {
            get { return HttpContext.Current.Session["Photo"] == null ? null : HttpContext.Current.Session["Photo"].ToString(); }
            set { HttpContext.Current.Session["Photo"] = value; }
        }

        public static string DBName
        {
            get { return HttpContext.Current.Session["DBName"] == null ? null : HttpContext.Current.Session["DBName"].ToString(); }
            set { HttpContext.Current.Session["DBName"] = value; }
        }
        public static string ClientCode
        {
            get { return HttpContext.Current.Session["ClientCode"] == null ? null : HttpContext.Current.Session["ClientCode"].ToString(); }
            set { HttpContext.Current.Session["ClientCode"] = value; }
        }
        public static string CaptionDefaultValue
        {
            get { return HttpContext.Current.Session["CaptionDefaultValue"] == null ? null : HttpContext.Current.Session["CaptionDefaultValue"].ToString(); }
            set { HttpContext.Current.Session["CaptionDefaultValue"] = value; }
        }
        public static string SerKey
        {
            get { return HttpContext.Current.Session["SerKey"] == null ? null : HttpContext.Current.Session["SerKey"].ToString(); }
            set { HttpContext.Current.Session["SerKey"] = value; }
        }
        public static string RetriveType
        {
            get { return HttpContext.Current.Session["RetriveType"] == null ? null : HttpContext.Current.Session["RetriveType"].ToString(); }
            set { HttpContext.Current.Session["RetriveType"] = value; }
        }

        public static string TopMenuName
        {
            get { return HttpContext.Current.Session["TopMenuName"] == null ? null : HttpContext.Current.Session["TopMenuName"].ToString(); }
            set { HttpContext.Current.Session["TopMenuName"] = value; }
        }

        public static string SubMenuName
        {
            get { return HttpContext.Current.Session["SubMenuName"] == null ? null : HttpContext.Current.Session["SubMenuName"].ToString(); }
            set { HttpContext.Current.Session["SubMenuName"] = value; }
        }

        public static string GroupCode
        {
            get
            {
                return HttpContext.Current.Session["GroupCode"] == null ? null : HttpContext.Current.Session["GroupCode"].ToString();
            }
            set { HttpContext.Current.Session["GroupCode"] = value; }
        }

        public static int CompanyID
        {
            get { return HttpContext.Current.Session["CompanyID"] == null ? 0 : int.Parse(HttpContext.Current.Session["CompanyID"].ToString()); }
            set { HttpContext.Current.Session["CompanyID"] = value; }
        }

       

       
        public static int BOMFlag
        {
            get { return HttpContext.Current.Session["BOMFlag"] == null ? 0 : int.Parse(HttpContext.Current.Session["BOMFlag"].ToString()); }
            set { HttpContext.Current.Session["BOMFlag"] = value; }
        }




        public static string CustomerID
        {
            get
            {
                return HttpContext.Current.Session["CustomerID"] == null ? null : HttpContext.Current.Session["CustomerID"].ToString();
            }
            set { HttpContext.Current.Session["CustomerID"] = value; }
        }
        public static string FromDate
        {
            get
            {
                return HttpContext.Current.Session["FromDate"] == null ? null : HttpContext.Current.Session["FromDate"].ToString();
            }
            set { HttpContext.Current.Session["FromDate"] = value; }
        }

        public static string ToDate
        {
            get
            {
                return HttpContext.Current.Session["ToDate"] == null ? null : HttpContext.Current.Session["ToDate"].ToString();
            }
            set { HttpContext.Current.Session["ToDate"] = value; }
        }

        public static string CompanyName
        {
            get 
            { 
                return HttpContext.Current.Session["CompanyName"] == null ? null : HttpContext.Current.Session["CompanyName"].ToString(); 
            }
            set { HttpContext.Current.Session["CompanyName"] = value; }
        }
        public static string CompanyAddress
        {
            get
            {
                return HttpContext.Current.Session["CompanyAddress"] == null ? null : HttpContext.Current.Session["CompanyAddress"].ToString();
            }
            set { HttpContext.Current.Session["CompanyAddress"] = value; }
        }
        public static string CompanyLogo1
        {
            get
            {
                return HttpContext.Current.Session["CompanyLogo1"] == null ? null : HttpContext.Current.Session["CompanyLogo1"].ToString();
            }
            set { HttpContext.Current.Session["CompanyLogo1"] = value; }
        }
        public static string CompanyLogo2
        {
            get
            {
                return HttpContext.Current.Session["CompanyLogo2"] == null ? null : HttpContext.Current.Session["CompanyLogo2"].ToString();
            }
            set { HttpContext.Current.Session["CompanyLogo2"] = value; }
        }
        public static int FinancialYear
        {
            get { return HttpContext.Current.Session["FinancialYear"] == null ? 0 : int.Parse(HttpContext.Current.Session["FinancialYear"].ToString()); }
            set { HttpContext.Current.Session["FinancialYear"] = value; }
        }

        public static int UserGroupID
        {
            get { return HttpContext.Current.Session["UserGroupID"] == null ? 0 : int.Parse(HttpContext.Current.Session["UserGroupID"].ToString()); }
            set { HttpContext.Current.Session["UserGroupID"] = value; }
        }

        public static string UserGroupName
        {
            get
            {
                return HttpContext.Current.Session["UserGroupName"] == null ? null : HttpContext.Current.Session["UserGroupName"].ToString();
            }
            set { HttpContext.Current.Session["UserGroupName"] = value; }
        }

        public static int UserID
        {
            get { return HttpContext.Current.Session["UserID"] == null ? 0 : int.Parse(HttpContext.Current.Session["UserID"].ToString()); }
            set { HttpContext.Current.Session["UserID"] = value; }
        }

        public static string UserName
        {
            get 
            { 
                return HttpContext.Current.Session["UserName"] == null ? null : HttpContext.Current.Session["UserName"].ToString(); 
            }
            set { HttpContext.Current.Session["UserName"] = value; }
        }

        public static string UserType
        {
            get
            {
                return HttpContext.Current.Session["UserType"] == null ? null : HttpContext.Current.Session["UserType"].ToString();
            }
            set { HttpContext.Current.Session["UserType"] = value; }
        }

        public static string Role
        {
            get
            {
                //return HttpContext.Current.Session["Role"] == null ? null : HttpContext.Current.Session["Role"].ToString();
                return "F";
            }
            set { HttpContext.Current.Session["Role"] = value; }
        }

        public static string DefaultLanguage
        {
            get { return HttpContext.Current.Session["Lang"] == null ? null : HttpContext.Current.Session["Lang"].ToString(); }
            set { HttpContext.Current.Session["Lang"] = value; }
        }

        public static string UICultureName
        {
            get { return HttpContext.Current.Session["UICultureName"] == null ? null : HttpContext.Current.Session["UICultureName"].ToString(); }
            set { HttpContext.Current.Session["UICultureName"] = value; }
        }

        public static string SessionID
        {
            get { return HttpContext.Current.Session["SessionID"] == null ? null : HttpContext.Current.Session["SessionID"].ToString(); }
            set { HttpContext.Current.Session["SessionID"] = value; }
        }

        public static string MachineIP
        {
            get
            {
                return HttpContext.Current.Session["MachineIP"] == null ? null : HttpContext.Current.Session["MachineIP"].ToString();
            }
            set { HttpContext.Current.Session["MachineIP"] = value; }
        }
        public static int BaseUOMID
        {
            get { return HttpContext.Current.Session["BaseUOMID"] == null ? 0 : int.Parse(HttpContext.Current.Session["BaseUOMID"].ToString()); }
            set { HttpContext.Current.Session["BaseUOMID"] = value; }
        }
        public static int UOMID
        {
            get { return HttpContext.Current.Session["UOMID"] == null ? 0 : int.Parse(HttpContext.Current.Session["UOMID"].ToString()); }
            set { HttpContext.Current.Session["UOMID"] = value; }
        }

        public static string SearchColumnName
        {
            get { return HttpContext.Current.Session["SearchColumnName"] == null ? string.Empty : HttpContext.Current.Session["SearchColumnName"].ToString(); }
            set { HttpContext.Current.Session["SearchColumnName"] = value; }
        }

        public static string SearchText
        {
            get { return HttpContext.Current.Session["SearchText"] == null ? string.Empty : HttpContext.Current.Session["SearchText"].ToString(); }
            set { HttpContext.Current.Session["SearchText"] = value; }
        }

        public static string StoreType
        {
            get { return HttpContext.Current.Session["StoreType"] == null ? string.Empty : HttpContext.Current.Session["StoreType"].ToString(); }
            set { HttpContext.Current.Session["StoreType"] = value; }
        }

        public static string SortDirection
        {
            get { return HttpContext.Current.Session["SortDirection"] == null ? "ASC" : HttpContext.Current.Session["SortDirection"].ToString(); }
            set { HttpContext.Current.Session["SortDirection"] = value; }
        }

        public static string SortExpression
        {
            get { return HttpContext.Current.Session["SortExpression"] == null ? string.Empty : HttpContext.Current.Session["SortExpression"].ToString(); }
            set { HttpContext.Current.Session["SortExpression"] = value; }
        }

        public static int PageIndex
        {
            get { return HttpContext.Current.Session["PageIndex"] == null ? 0 : int.Parse(HttpContext.Current.Session["PageIndex"].ToString()); }
            set { HttpContext.Current.Session["PageIndex"] = value; }
        }
        public static string SMTPUserName
        {
            get { return HttpContext.Current.Session["SMTPUserName"] == null ? string.Empty : HttpContext.Current.Session["SMTPUserName"].ToString(); }
            set { HttpContext.Current.Session["SMTPUserName"] = value; }
        }
        public static string SMTPPassword
        {
            get { return HttpContext.Current.Session["SMTPPassword"] == null ? string.Empty : HttpContext.Current.Session["SMTPPassword"].ToString(); }
            set { HttpContext.Current.Session["SMTPPassword"] = value; }
        }
        public static string SMTPHost
        {
            get { return HttpContext.Current.Session["SMTPHost"] == null ? string.Empty : HttpContext.Current.Session["SMTPHost"].ToString(); }
            set { HttpContext.Current.Session["SMTPHost"] = value; }
        }
        public static string SMTPPort
        {
            get { return HttpContext.Current.Session["SMTPPort"] == null ? string.Empty : HttpContext.Current.Session["SMTPPort"].ToString(); }
            set { HttpContext.Current.Session["SMTPPort"] = value; }
        }
    }
}