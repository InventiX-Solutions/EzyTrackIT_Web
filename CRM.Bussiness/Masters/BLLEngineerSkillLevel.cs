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
   public class BLLEngineerSkillLevel
    {
       public DataTable GetEngineerSkillLevelList(string dbname)
       {
           DALEngineerSkilllevel myDALEngineerSkilllevel = new DALEngineerSkilllevel();
           return myDALEngineerSkilllevel.GetSkilllevelList(dbname);
       }
       public int GetDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id, string dbname)
       {
           DALEngineerSkilllevel myDALEngineerSkilllevel = new DALEngineerSkilllevel();
           return myDALEngineerSkilllevel.CheckDuplicateExists(companyID, tableName, columnName, codeORName, idcolumnname, id, dbname);
       }
       public int InsertOrUpdateRecord(BEEngineerSkilllevel br, string dbname)
       {
           DALEngineerSkilllevel myDALEngineerSkilllevel = new DALEngineerSkilllevel();
           return myDALEngineerSkilllevel.InsertOrUpdateRecord(br, dbname);
       }
       public int DeleteBrand(int LevelID, int modifiedBy, string dbname)
       {
           DALEngineerSkilllevel myDALEngineerSkilllevel = new DALEngineerSkilllevel();
           return myDALEngineerSkilllevel.DeleteBrand(LevelID, modifiedBy, dbname);
       }
    }
}
