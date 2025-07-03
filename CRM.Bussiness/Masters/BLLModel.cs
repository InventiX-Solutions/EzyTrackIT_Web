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
    public class BLLModel
    {
        public DataTable GetBrandlist(string dbname)
        {
            DALModel myDALModel = new DALModel();
            return myDALModel.GetBrandlist(dbname);
        }
        public DataTable GetModelList(string dbname)
        {
            DALModel myDALModel = new DALModel();
            return myDALModel.GetModelList(dbname);
        }
        public int GetDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id,string dbname)
        {
            DALModel myDALModel = new DALModel();
            return myDALModel.CheckDuplicateExists(companyID, tableName, columnName, codeORName, idcolumnname, id, dbname);
        }
        public int InsertOrUpdateRecord(BEModel br, string dbname)
        {
            DALModel myDALModel = new DALModel();
            return myDALModel.InsertOrUpdateRecord(br, dbname);
        }
        public int DeleteModel(int ModelID, int modifiedBy,string dbname)
        {
            DALModel myDALModel = new DALModel();
            return myDALModel.DeleteModel(ModelID, modifiedBy, dbname);
        }
    }
}
