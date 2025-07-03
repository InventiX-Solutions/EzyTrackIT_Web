using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Masters
{
    [Serializable]
   public class BEmarkersOnMap
   {
        public string placeName { get; set; }
      
        public List<BELatLng> LatLng { get; set; }
    }
}
