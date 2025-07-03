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
    public class BLLProblem
    {
        public DataTable GeProblemList(string dbname)
        {
            DALProblem myDALProblem = new DALProblem();
            return myDALProblem.GetProblemList(dbname);
        }
        public int GetDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id,string dbname)
        {
            DALProblem myDALProblem = new DALProblem();
            return myDALProblem.CheckDuplicateExists(companyID, tableName, columnName, codeORName, idcolumnname, id, dbname);
        }
        public int InsertOrUpdateRecord(BEProblem br,string dbname)
        {
            DALProblem myDALProblem = new DALProblem();
            return myDALProblem.InsertOrUpdateRecord(br, dbname);
        }
        public int DeleteProblem(int ProblemID, int modifiedBy,string dbname)
        {
            DALProblem myDALProblem = new DALProblem();
            return myDALProblem.DeleteProblem(ProblemID, modifiedBy, dbname);
        }
    }
}
