using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.UI;
using System;
using System.Drawing;
using System.IO;

using CRM.Artifacts;
using CRM.Data.Masters;
using CRM.Artifacts.Masters;

using System.Configuration;
using MySql.Data.MySqlClient;

namespace CRM.Bussiness.Masters
{
   public class BLLTimeCostAnalysis
    {
       public DataTable GetReportList(string fromdate, string todate, int ReportType, string dbname)
       {
           DALTimeCostAnalysis myDALTimeCostAnalysis = new DALTimeCostAnalysis();
           return myDALTimeCostAnalysis.GetReportList(fromdate, todate, ReportType, dbname);
       }
    }
}
