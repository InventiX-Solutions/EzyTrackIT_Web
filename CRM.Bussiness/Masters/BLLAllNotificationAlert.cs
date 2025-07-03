using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Data.Masters;

namespace CRM.Bussiness.Masters
{
    public class BLLAllNotificationAlert
    {
        public int Notification_Update(string message_to, string notification_id, string Type, string Status, int CompanyID, int UserID, string loginMode, string remarks, string dbname)
        {
            DALAllNotificationAlert myDALDevice = new DALAllNotificationAlert();
            return myDALDevice.Notification_Update(message_to, notification_id, Type, Status, CompanyID, UserID, loginMode, remarks, dbname);
        }

        public int GetMonthNotificationCountByType(string type, int CompanyID, string dbname)
        {
            DALAllNotificationAlert myDALDevice = new DALAllNotificationAlert();
            return myDALDevice.GetMonthNotificationCountByType(type, CompanyID, dbname);
        }
       
    }
}
