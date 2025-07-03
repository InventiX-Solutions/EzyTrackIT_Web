using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Masters
{
   public class BEProduct
    {
           public int product_id { get; set; }
           public string product_code { get; set; }
           public string product_name { get; set; }
           public string model_name { get; set; }
           public string brand_name { get; set; }
           public int model_id { get; set; }
           public int brand_id { get; set; }
           public string PartNo { get; set; }
           public int Created_By { get; set; }
           public int Modified_By { get; set; }
    }
}
