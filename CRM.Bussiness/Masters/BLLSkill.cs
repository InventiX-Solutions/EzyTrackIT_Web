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
   public  class BLLSkill
    {
       public DataTable GetSkillList(string dbname)
        {
            DALSkill myDALSkill = new DALSkill();
            return myDALSkill.GetSkillList(dbname);
        }
       public int InsertOrUpdateRecord(BESkill br,string dbname)
        {
            DALSkill myDALSkill = new DALSkill();
            return myDALSkill.InsertOrUpdateRecord(br, dbname);
        }

        public int GetDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id,string dbname)
        {
            DALSkill myDALSkill = new DALSkill();
            return myDALSkill.CheckDuplicateExists(companyID, tableName, columnName, codeORName, idcolumnname, id, dbname);
        }

        public int DeleteEngineer(int SkillID, int modifiedBy,string dbname)
        {
            DALSkill myDALSkill = new DALSkill();
            return myDALSkill.DeleteEngineer(SkillID, modifiedBy, dbname);
        }
    }
}

