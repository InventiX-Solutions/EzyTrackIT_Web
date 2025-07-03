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
    public class BLLPriority
    {
        public DataTable GetPriorityList(string dbname)
        {
            DALPriority myDALPriority = new DALPriority();
            return myDALPriority.GetPriorityList(dbname);
        }
        public int GetDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id, string dbname)
        {
            DALPriority myDALPriority = new DALPriority();
            return myDALPriority.CheckDuplicateExists(companyID, tableName, columnName, codeORName, idcolumnname, id, dbname);
        }
        public int InsertOrUpdateRecord(BEPriority br, string dbname)
        {
            DALPriority myDALPriority = new DALPriority();
            return myDALPriority.InsertOrUpdateRecord(br, dbname);
        }
        public int DeletePriority(int prID, int modifiedBy, string dbname)
        {
            DALPriority myDALPriority = new DALPriority();
            return myDALPriority.DeletePriority(prID, modifiedBy, dbname);
        }
    }
}
