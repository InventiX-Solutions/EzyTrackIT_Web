using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Artifacts;
using CRM.Data.Masters;
using CRM.Artifacts.Masters;

namespace CRM.Bussiness.Masters
{
   public  class BLLVehicle
    {
       public DataTable GetVehicleList(string dbname)
        {
            DALVehicle myDALVehicle = new DALVehicle();
            return myDALVehicle.GetVehicleList(dbname);
        }
       public int InsertOrUpdateRecord(BEVehicle br,string dbname)
        {
            DALVehicle myDALVehicle = new DALVehicle();
            return myDALVehicle.InsertOrUpdateRecord(br, dbname);
        }

        public int GetDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id,string dbname)
        {
            DALVehicle myDALVehicle = new DALVehicle();
            return myDALVehicle.CheckDuplicateExists(companyID, tableName, columnName, codeORName, idcolumnname, id, dbname);
        }

        public int DeleteVehicle(int VehicleID, int modifiedBy,string dbname)
        {
            DALVehicle myDALVehicle = new DALVehicle();
            return myDALVehicle.DeleteVehicle(VehicleID, modifiedBy, dbname);
        }
    }
}

