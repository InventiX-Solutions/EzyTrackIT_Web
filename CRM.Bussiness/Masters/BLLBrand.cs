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
    public class BLLBrand
    {
        public DataTable GetBrandList(string dbname)
        {
            DALBrand myDALBrand = new DALBrand();
            return myDALBrand.GetBrandList(dbname);
        }
        public int GetDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id, string dbname)
        {
            DALBrand myDALBrand = new DALBrand();
            return myDALBrand.CheckDuplicateExists(companyID, tableName, columnName, codeORName, idcolumnname, id,dbname);
        }
        public int InsertOrUpdateRecord(BEBrand br, string dbname)
        {
            DALBrand myDALBrand = new DALBrand();
            return myDALBrand.InsertOrUpdateRecord(br,dbname);
        }
        public int DeleteBrand( int BrandID, int modifiedBy, string dbname)
        {
            DALBrand myDALBrand = new DALBrand();
            return myDALBrand.DeleteBrand( BrandID, modifiedBy,dbname);
        }

    }
}
