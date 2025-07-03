using CRM.Artifacts.Masters;
using CRM.Data.Masters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Bussiness.Masters
{
    public class BLLJobType

    {
        public DataTable GetJobTypeList(string dbname)
        {
            DALJobType myDALJobType = new DALJobType();
            return myDALJobType.GetJobTypeList(dbname);
        }
        public int GetDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id, string dbname)
        {
            DALJobType myDALJobType = new DALJobType();
            return myDALJobType.CheckDuplicateExists(companyID, tableName, columnName, codeORName, idcolumnname, id, dbname);
        }
        public int InsertOrUpdateRecord(BEJobType br, string dbname)
        {
            DALJobType myDALJobType = new DALJobType();
            return myDALJobType.InsertOrUpdateRecord(br, dbname);
        }
        public int DeleteJobType(int JobTypeID, int modifiedBy, string dbname)
        {
            DALJobType myDALJobType = new DALJobType();
            return myDALJobType.DeleteJobType(JobTypeID, modifiedBy, dbname);
        }


    }
}
