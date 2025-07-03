using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Artifacts.Masters;
using Newtonsoft.Json;

namespace TCC_CRM.Transactions
{
    public partial class TrackingMultipleLocation : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }
         [WebMethod()]
        public static Object Test()
        {
            string jsonString = "";
            BEmarkersOnMap bEmarkersOnMap = new BEmarkersOnMap();
            List<BELatLng> listBeLatLng = new List<BELatLng>();
            
            BELatLng bELatLng = new BELatLng();
            
            bEmarkersOnMap.placeName = "Australia (Uluru)";
            bELatLng.lat = -25.344;
            bELatLng.lat = 131.036;
            listBeLatLng.Add(bELatLng);
            bEmarkersOnMap.LatLng = listBeLatLng;

            jsonString = JsonConvert.SerializeObject(bEmarkersOnMap);

            return jsonString;
            //dynamic flexible = new ExpandoObject();
            //flexible.Int = 3;
            //flexible.String = "hi";

            //var dictionary = (IDictionary<string, object>)flexible;
            //dictionary.Add("Bool", false);

          //  var serialized = JsonConvert.SerializeObject(dictionary);
            // {"Int":3,"String":"hi","Bool":false}
        }
    }
}