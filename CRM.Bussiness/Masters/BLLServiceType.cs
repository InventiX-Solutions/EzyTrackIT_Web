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
   public class BLLServiceType
    {
       public DataTable GetServiceTypeList(string dbname)
       {
           DALServiveType myDALServiveType = new DALServiveType();
           return myDALServiveType.GetServiceTypeList(dbname);
       }
       public int GetDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id, string dbname)
       {
           DALServiveType myDALServiveType = new DALServiveType();
           return myDALServiveType.CheckDuplicateExists(companyID, tableName, columnName, codeORName, idcolumnname, id, dbname);
       }
       public int InsertOrUpdateRecord(BEServiceType br, string dbname)
       {
           DALServiveType myDALServiveType = new DALServiveType();
           return myDALServiveType.InsertOrUpdateRecord(br, dbname);
       }
       public int DeleteServicetype(int service_typeid, int modifiedBy, string dbname)
       {
           DALServiveType myDALServiveType = new DALServiveType();
           return myDALServiveType.DeleteServicetype(service_typeid, modifiedBy, dbname);
       }
    }
}
