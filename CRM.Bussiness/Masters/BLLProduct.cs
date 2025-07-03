using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Artifacts;
using CRM.Data.Masters;
using CRM.Artifacts.Masters;

using System.Configuration;
using MySql.Data.MySqlClient;

namespace CRM.Bussiness.Masters
{
   public class BLLProduct
    {
        public DataTable GetProductList(string dbname)
        {
            DALProduct myDALProduct = new DALProduct();
            return myDALProduct.GetProductList(dbname);
        }
        public DataTable GetModellist(string dbname)
        {
            DALProduct myDALProduct = new DALProduct();
            return myDALProduct.GetModelList(dbname);
        }
        public DataTable GetBrandlist(string dbname)
        {
            DALProduct myDALProduct = new DALProduct();
            return myDALProduct.GetBrandList(dbname);
        }

        //// Added mano //

        //public DataTable GetJobType(string dbname)
        //{
        //    DALProduct myDALProduct = new DALProduct();
        //    return myDALProduct.GetJobType(dbname);
        //}
        //// End mano //

        public BEProduct GetProduct(int CompanyId, int product_id,string dbname)
        {
            BEProduct beProduct = new BEProduct();

            MySqlDataReader mySqlDataReader;



            DALProduct myDALProduct = new DALProduct();
            mySqlDataReader = myDALProduct.GetProduct(CompanyId, product_id, dbname);

            while (mySqlDataReader.Read())
            {

                beProduct.product_id = int.Parse(mySqlDataReader["product_id"].ToString());
                beProduct.product_code = mySqlDataReader["product_code"].ToString();
                beProduct.product_name = mySqlDataReader["product_name"].ToString();
                beProduct.brand_name = mySqlDataReader["brand_name"].ToString();
                beProduct.model_name = mySqlDataReader["model_name"].ToString();
                beProduct.PartNo = mySqlDataReader["PartNumber"].ToString();

            }
            mySqlDataReader.Close();
            return beProduct;
        }
        public DataTable GetDropDownValues(string selectedTextName, string selectedValueName, string tableName, string filterCondition,string dbname)
        {
            DALProduct myDALProduct = new DALProduct();
            return myDALProduct.LoadDropDownList(selectedTextName, selectedValueName, tableName, filterCondition, dbname);
        }
        public int GetDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id,string dbname)
        {
            DALProduct myDALProduct = new DALProduct();
            return myDALProduct.CheckDuplicateExists(companyID, tableName, columnName, codeORName, idcolumnname, id, dbname);
        }
        public int InsertOrUpdateRecord(BEProduct br,string dbname)
        {
            DALProduct myDALProduct = new DALProduct();
            return myDALProduct.InsertOrUpdateRecord(br, dbname);
        }
        public int DeleteProduct(int product_id, int Modified_By,string dbname)
        {
            DALProduct myDALProduct = new DALProduct();
            return myDALProduct.DeleteProduct(product_id, Modified_By, dbname);
        }
    }
}
