using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GleamTech.DocumentUltimate;
using GleamTech.DocumentUltimate.AspNet;

namespace MPORT.Discussions
{
    public partial class WebForm2 : System.Web.UI.Page
    {
     

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["viwerfile"] != null)
            {
                uploaddocwithoutparm();
            }
        }

        public void uploaddoc(string name)
        {
           // string sComtext = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];
         
       
       
            string directoryFullPath = string.Empty;
            if (!string.IsNullOrEmpty(name))
            {
                string sComtext = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];
                string savepath = Server.MapPath(sComtext);
                string sFile = name;
                FileInfo fileInfo = new FileInfo(sFile);
                directoryFullPath = savepath + @"\" + fileInfo.Name;
                bool fileExists = (System.IO.File.Exists(directoryFullPath) ? true : false);
                if (fileExists == true)
                {
                    documentViewer.Document = directoryFullPath;
                   
                }
                else
                {
                    directoryFullPath = savepath + @"\" + "as_no_document.jpeg";
                    documentViewer.Document = directoryFullPath;
                }

            }

            else
            {
                string sComtext = name;
                string savepath = Server.MapPath(sComtext);
                directoryFullPath = savepath + @"\" + "as_no_document.jpeg";
                documentViewer.Document = directoryFullPath;
            }
        }

        [System.Web.Services.WebMethod]
        public static bool GetResponse()
        {
            return true;
        }

        public void uploaddocwithoutparm()
        {
            // string sComtext = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];


            string sComtext = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];
            string savepath = Server.MapPath(sComtext);
            string directoryFullPath = string.Empty;
            if (Request.QueryString["viwerfile"] != null)
            {
              
                string sFile = Convert.ToString((Request.QueryString["viwerfile"]));
                FileInfo fileInfo = new FileInfo(sFile);
                directoryFullPath = savepath + @"\" + fileInfo.Name;
                bool fileExists = (System.IO.File.Exists(directoryFullPath) ? true : false);
                if (fileExists == true)
                {
                    documentViewer.Document = directoryFullPath;
                }
                else
                {
                    directoryFullPath = savepath + @"\" + "as_no_document.jpeg";
                    documentViewer.Document = directoryFullPath;
                }

            }

            else
            {
                
                directoryFullPath = savepath + @"\" + "as_no_document.jpeg";
                documentViewer.Document = directoryFullPath;
            }
        }

        [System.Web.Services.WebMethod]
        public static void  GetDoc(string name)
        {
            WebForm2 ik = new WebForm2();
            ik.uploaddoc(name);

            
        }
    }
}