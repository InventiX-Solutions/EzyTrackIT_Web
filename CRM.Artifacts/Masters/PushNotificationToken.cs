using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Masters
{
    public class PushNotificationToken
    {
        public string TokenId { get; set; }
    }
    public class PushNotificationDetails
    {
        public int TicketID { get; set; }
        public string TicketNo { get; set; }
        public int engineer_id { get; set; }
        public string Assigned_to { get; set; }
        public string CreateBY { get; set; }
        public string TokenID { get; set; }
        public string StatusCode { get; set; }
        public string PushNotification_Content { get; set; }

    }
}
