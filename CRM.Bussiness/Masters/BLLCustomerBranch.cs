using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Artifacts;
using CRM.Data.Masters;
using CRM.Artifacts.Masters;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace CRM.Bussiness.Masters
{
    public class BLLCustomerBranch
    {
        public DataTable GetCustomer(string dbname)
      {
          DALCustomerBranch myDALCustomer = new DALCustomerBranch();
          return myDALCustomer.GetCustomer(dbname);
      }
      public DataTable GetCustomerList(string dbname)
      {
          DALCustomerBranch myDALCustomer = new DALCustomerBranch();
          return myDALCustomer.GetCustomerList(dbname);
      }
      public BECustomerBranch GetCustomer(int CompanyId, int customer_branch_id, string dbname)
      {
          BECustomerBranch beCustomer = new BECustomerBranch();
         
          MySqlDataReader mySqlDataReader;



          DALCustomerBranch myDALCustomer = new DALCustomerBranch();
          mySqlDataReader = myDALCustomer.GetCustomer(CompanyId, customer_branch_id,dbname);

          while (mySqlDataReader.Read())
          {

              beCustomer.CustomerBranchID = int.Parse(mySqlDataReader["customer_branch_id"].ToString());
              beCustomer.CustomerBranchCode = mySqlDataReader["customer_branch_code"].ToString();
              beCustomer.CustomerBranchName = mySqlDataReader["customer_branch_Name"].ToString();
              beCustomer.CustomerID = int.Parse(mySqlDataReader["customer_id"].ToString());
              beCustomer.AddressLine1 = mySqlDataReader["address_line_1"].ToString();
              beCustomer.AddressLine2 = mySqlDataReader["address_line_2"].ToString();
              beCustomer.ContactPerson = mySqlDataReader["contact_person"].ToString();
              beCustomer.PhoneNo = mySqlDataReader["phone_no"].ToString();
              beCustomer.ServiceLocation = mySqlDataReader["ServiceLocation"].ToString();
              beCustomer.Remarks = mySqlDataReader["Remarks"].ToString();
          }
          mySqlDataReader.Close();
          return beCustomer;
      }
      public int GetDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id, string dbname)
      {
          DALCustomerBranch myDALCustomer = new DALCustomerBranch();
          return myDALCustomer.CheckDuplicateExists(companyID, tableName, columnName, codeORName, idcolumnname, id,dbname);
      }
      public int InsertOrUpdateRecord(BECustomerBranch br, string dbname)
      {
          DALCustomerBranch myDALCustomer = new DALCustomerBranch();
          return myDALCustomer.InsertOrUpdateRecord(br,dbname);
      }
      public int DeleteCustomer(int CustomerID, int modifiedBy, string dbname)
      {
          DALCustomerBranch myDALCustomer = new DALCustomerBranch();
          return myDALCustomer.DeleteCustomer(CustomerID, modifiedBy,dbname);
      }
    
      public int InsertBulkRecord(DataTable dtTable,int id, string dbname)
      {
          DALCustomerBranch myDALCustomer = new DALCustomerBranch();
          return myDALCustomer.InsertBulkRecord(dtTable,id, dbname);
      }
    }
}
