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
   public class BLLSeverity
    {
        public DataTable GetSeverityList(string dbname)
        {
            DALSeverity myDALSeverity = new DALSeverity();
            return myDALSeverity.GetSeverityList(dbname);
        }
        public int GetDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id, string dbname)
        {
            DALSeverity myDALSeverity = new DALSeverity();
            return myDALSeverity.CheckDuplicateExists(companyID, tableName, columnName, codeORName, idcolumnname, id, dbname);
        }
        public int InsertOrUpdateRecord(BESeverity br, string dbname)
        {
            DALSeverity myDALSeverity = new DALSeverity();
            return myDALSeverity.InsertOrUpdateRecord(br, dbname);
        }
        public int DeleteSeverity(int SevID, int modifiedBy, string dbname)
        {
            DALSeverity myDALSeverity = new DALSeverity();
            return myDALSeverity.DeleteSeverity(SevID, modifiedBy, dbname);
        }
    }
}
