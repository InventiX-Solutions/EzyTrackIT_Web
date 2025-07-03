using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CRM.Data.Masters
{
    public class DALAllNotificationAlert : Base.Data.BaseSql
    {
        //public DALAllNotificationAlert(string sqlConnectionString) : base(sqlConnectionString) { }

        public int Notification_Update(string message_to, string notification_id, string Type, string Status, int CompanyID, int UserID, string loginMode, string remarks, string dbname)
        {
            string strSql = @"insert into messagelog(CompanyID,loginMode,userid,message_to,notification_id,activity_datetime,Message_Type,Message_Status,remarks)values('" + CompanyID + "','" + loginMode + "','" + UserID + "','" + message_to + "','" + notification_id + "',now(),'" + Type + "','" + Status + "','" + remarks + "')";
            int i = RunSQLStringOnly(strSql, dbname);
            return i;
        }


        public int GetMonthNotificationCountByType(string type, int CompanyID, string dbname)
        {
            string strSql = @"SELECT count(logid) as count FROM messagelog where Message_Type='" + type + "'  and CompanyID=" + CompanyID + " and (date(activity_datetime) between  DATE_FORMAT(NOW() ,'%Y-%m-01') AND LAST_DAY(NOW()) ) ;";
            int i = RunExecuteScalarSQLInt(strSql, dbname);
            return i;
        }

      

       
    }
}
