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
    public class BLLCustomer
    {
        public DataTable GetCustomerList(string dbname)
        {
            DALCustomer myDALLocation = new DALCustomer();
            return myDALLocation.GetCustomerList(dbname);
        }

        public int InsertOrUpdateRecord(BECustomer br, string dbname)
        {
            DALCustomer myDALLocation = new DALCustomer();
            return myDALLocation.InsertOrUpdateRecord(br, dbname);
        }

        public int GetDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id, string dbname)
        {
            DALCustomer myDALLocation = new DALCustomer();
            return myDALLocation.CheckDuplicateExists(companyID, tableName, columnName, codeORName, idcolumnname, id, dbname);
        }

        public int DeleteLocation(int LocationID, int modifiedBy, string dbname)
        {
            DALCustomer myDALLocation = new DALCustomer();
            return myDALLocation.DeleteLocation(LocationID, modifiedBy, dbname);
        }
        public int InsertBulkRecord(DataTable dtTable, int id, string dbname)
        {
            DALCustomer myDALCustomer = new DALCustomer();
            return myDALCustomer.InsertBulkRecord(dtTable, id, dbname);
        }
    }
}
