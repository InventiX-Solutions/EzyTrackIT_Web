using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Masters
{
    public class BEVehicle
    {
        public string VehicleCode { get; set; }
        public string VehicleName { get; set; }
        public int VehicleID { get; set; }
        public int CompanyID { get; set; }
        public string VehicleNo { get; set; }
        public string Remarks { get; set; }
        public int EngineerID { get; set; }
        public int createdby { get; set; }
        public int modifiedby { get; set; }
    }
}
