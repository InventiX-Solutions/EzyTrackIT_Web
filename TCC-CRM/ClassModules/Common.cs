using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Reflection;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Text;

namespace TrackIT.ClassModules
{
    public class Common
    {
        public static void LogInfo(DateTime ReportingTime, string Message)
        {
            System.IO.StreamWriter oWrite = null;
            string strReportingTime = null;
            string FileName = null;

            try
            {
                strReportingTime = ReportingTime.ToString("yyyy-MM-dd HH:mm:ss");
                string Path = ConfigurationManager.AppSettings["LogInfo"].ToString();

                if (!Directory.Exists(Path + "..\\LogInfo\\"))
                {
                    Directory.CreateDirectory(Path + "..\\LogInfo\\");
                }

                FileName = Path + "..\\LogInfo\\" + "Log_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";

                //Check fle is already available else create a new file.
                if (File.Exists(FileName))
                {
                    oWrite = File.AppendText(FileName);
                }
                else
                {
                    oWrite = File.CreateText(FileName);
                }

                oWrite.WriteLine("Reporting Time : " + strReportingTime);
                oWrite.WriteLine(Message);
                oWrite.WriteLine(" ");
                oWrite.Close();
            }
            catch (Exception ex)
            {
                //LogInfo(DateTime.Now, ex.Message);
            }
        }

        //Email Configuration for KH - OneErp project
        public static string HangfireSMTPUserName = "ezytrackit.alerts@gmail.com";
        public static string HangfireSMTPPassword = "PGSEzyTrackIt@123";
        public static int HangfireSMTPPort = 587;
        public static string HangfireSMTPHost = "smtp.gmail.com";

        //public static string EmailTo = "arif.khit@khindia.com,office.khit@khindia.com,ganesh.khit@khindia.com,saravanan@aletheia.co.in";
        public static string EmailTo = "velu@peritusglobal.com";
        public static string EmailCC = "";
        //public static string EmailBCC = "";
        public static string EmailBCC = "velu@peritusglobal.com,sufiyan@peritusglobal.com";
        //Email Configuration for KH - OneErp project

        public class ListToDataTableConverter
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
        }


        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public class FCMPushNotification
        {
            public FCMPushNotification()
            {
                // TODO: Add constructor logic here  
            }
            public bool Successful
            {
                get;
                set;
            }
            public string Response
            {
                get;
                set;
            }
            public Exception Error
            {
                get;
                set;
            }


            //public static DataTable GetNotificationType()
            //{
            //    DataTable dtNotificationType = new DataTable();

            //    dtNotificationType.Columns.Add("Value");
            //    dtNotificationType.Columns.Add("Name");

            //    dtNotificationType.Rows.Add("1", "Check In Notification to Host/Visitor");
            //    dtNotificationType.Rows.Add("2", "Check Out Notification to Host/Visitor");
            //    //dtNotificationType.Rows.Add("3", "Pre-Registration Notification Host/Visitor");
            //    dtNotificationType.Rows.Add("3", "Pre-Registration Notification Visitor");
            //    dtNotificationType.Rows.Add("4", "Pass Expired Notification to Host/Visitor");
            //    dtNotificationType.Rows.Add("5", "Courier Notification to Employee");
            //    dtNotificationType.Rows.Add("6", "Canteen Incharge Notification");
            //    dtNotificationType.Rows.Add("7", "Check In Notification for Canteen Incharge");
            //    dtNotificationType.Rows.Add("8", "Room Booking Expiry");
            //    dtNotificationType.Rows.Add("9", "Parking Slot Expiry");
            //    dtNotificationType.Rows.Add("10", "Pre-Registration Rejection Notification"); // added for pre registration rejection

            //    dtNotificationType.Rows.Add("11", "Visitor Self-Registration Approval Notification");
            //    dtNotificationType.Rows.Add("12", "Visitor Self-Registration Rejection Notification"); // added for pre registration rejection

            //    return dtNotificationType;
            //}

            //public static DataTable GetNotificationRecipient()
            //{
            //    DataTable dtNotificationRecipient = new DataTable();

            //    dtNotificationRecipient.Columns.Add("Value");
            //    dtNotificationRecipient.Columns.Add("Name");
            //    dtNotificationRecipient.Columns.Add("Type");

            //    dtNotificationRecipient.Rows.Add("A", "All", "1");
            //    dtNotificationRecipient.Rows.Add("H", "Host", "1");
            //    dtNotificationRecipient.Rows.Add("V", "Visitor", "1");
            //    dtNotificationRecipient.Rows.Add("C", "Canteen", "2");
            //    dtNotificationRecipient.Rows.Add("E", "Employee", "3");

            //    return dtNotificationRecipient;
            //}

            public  FCMPushNotification SendNotification(string _title, string _message, string _topic, string tokenId, String userId, string _statusType, string TicketId)
            {
                FCMPushNotification result = new FCMPushNotification();
                try
                {
                    result.Successful = true;
                    result.Error = null;
                    string serverKey = "AAAAvDmHcvY:APA91bFFzROISpZKbBr0RuO2-tUqRoUpZfhFFjbPa2KiKXhz1L-Tdm8G6cChARgQp4i9YlzT_F0vQf4lbIcH7cMpjkbyr0b_JW9bonMB2HBgNf5_WTK1U4PzD1ylPrWRr-EYowvtGkGI";
                    string senderId = "808419029750";
                    string deviceId = "EzytrackIT";


                    var requestUri = "https://fcm.googleapis.com/fcm/send";
                    WebRequest webRequest = WebRequest.Create(requestUri);
                    webRequest.Method = "POST";
                    webRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                    webRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                    webRequest.ContentType = "application/json";



                    var data_value = new
                    {


                        registration_ids = new List<string>() { tokenId },


                        priority = "high",


                        data = new
                        {
                            title = _title,
                            body = _message,
                            StatusType = _statusType,
                            show_in_foreground = "True",
                            show_in_background = "True",
                            // image_url = "https://freeiconshop.com/wp-content/uploads/edd/notification-flat.png",
                            count = "1",
                            user_id = userId.ToString(),
                            ticket_id = TicketId.ToString()
                        },


                    };
                    var serializer = new JavaScriptSerializer();


                    var json = serializer.Serialize(data_value);
                    Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                    webRequest.ContentLength = byteArray.Length;
                    using (Stream dataStream = webRequest.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        using (WebResponse webResponse = webRequest.GetResponse())
                        {
                            using (Stream dataStreamResponse = webResponse.GetResponseStream())
                            {
                                using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();
                                    result.Response = sResponseFromServer;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Successful = false;
                    result.Response = null;
                    result.Error = ex;


                   
                }
                return result;
            }


          

        }

        //added
        public static DataTable GetNotificationType()
        {
            DataTable dtNotificationType = new DataTable();

            dtNotificationType.Columns.Add("Value");
            dtNotificationType.Columns.Add("Name");

            dtNotificationType.Rows.Add("1", "Job Creation");
            dtNotificationType.Rows.Add("2", "Job Engineer Assign");
            dtNotificationType.Rows.Add("3", "24 Hour job not close");
            dtNotificationType.Rows.Add("4", "Job Closed");

            return dtNotificationType;
        }
        public static string NullToString(object Value)
        {
            return Value == null ? "" : Value.ToString();
        }

        public static DataTable GetNotificationRecipient()
        {
            DataTable dtNotificationRecipient = new DataTable();

            dtNotificationRecipient.Columns.Add("Value");
            dtNotificationRecipient.Columns.Add("Name");
            dtNotificationRecipient.Columns.Add("Type");

            dtNotificationRecipient.Rows.Add("A", "All", "1");
            dtNotificationRecipient.Rows.Add("H", "Host", "1");
            dtNotificationRecipient.Rows.Add("V", "Visitor", "1");
            dtNotificationRecipient.Rows.Add("C", "Canteen", "2");
            dtNotificationRecipient.Rows.Add("E", "Employee", "3");

            return dtNotificationRecipient;
        }
    }
}