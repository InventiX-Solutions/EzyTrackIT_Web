using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Artifacts;
using CRM.Data;
using CRM.Artifacts.Masters;
using CRM.Data.Masters;

namespace CRM.Business
{
    public class BLLNotificationTemplate
    {
        public int InsertOrUpdateRecord(BENotificationTemplate br, out string sMessage, string dbname)
        {
            DALNotificationTemplate myDALNotificationTemplate = new DALNotificationTemplate();
            return myDALNotificationTemplate.InsertOrUpdateRecord(br, dbname, out sMessage);
        }
    }
}
