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
   public class BLLEngineer
    {

       public DataTable GetSkilllist(string dbname)
       {
           DALEngineer myDALEngineer = new DALEngineer();
           return myDALEngineer.GetSkilllist(dbname);
       }
       public DataTable Getvehiclelist(string dbname)
       {
           DALEngineer myDALEngineer = new DALEngineer();
           return myDALEngineer.Getvehiclelist(dbname);
       }

       public DataTable GetengineerList(string dbname)
        {
            DALEngineer myDALEngineer = new DALEngineer();
            return myDALEngineer.GetengineerList(dbname);
        }
       public int InsertOrUpdateRecord(BEEngineer br, string dbname)
       {
           DALEngineer myDALEngineer = new DALEngineer();
           return myDALEngineer.InsertOrUpdateRecord(br, dbname);
       }

      



        public int GetDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id, string dbname)
        {
            DALEngineer myDALEngineer = new DALEngineer();
            return myDALEngineer.CheckDuplicateExists(companyID, tableName, columnName, codeORName, idcolumnname, id, dbname);
        }

        public int DeleteEngineer(int EngineerID, int modifiedBy, string dbname)
        {
            DALEngineer myDALEngineer = new DALEngineer();
            return myDALEngineer.DeleteEngineer(EngineerID, modifiedBy, dbname);
        }
    }
}
