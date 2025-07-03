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

namespace CRM.Bussiness
{
   public class BLLUserMaster
    {
       public int DeleteUser(int UserID, int modifiedBy,string dbname)
       {
           DALUser myDALUser = new DALUser();
           return myDALUser.DeleteUser(UserID, modifiedBy, dbname);
       }

       public DataTable GetUserlist(string dbname)
       {
           DALUser myDALUser = new DALUser();
           return myDALUser.GetUserlist(dbname);
       }

       public int InsertOrUpdateRecord(BEUser br,string dbname)
       {
           DALUser myDALUser = new DALUser();
           return myDALUser.InsertOrUpdateRecord(br, dbname);
       }

       public int GetDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id,string dbname)
       {
           DALUser myDALUser = new DALUser();
           return myDALUser.CheckDuplicateExists(companyID, tableName, columnName, codeORName, idcolumnname, id, dbname);
       }
       public DataTable GetUserRoles(int TypeID,string dbname)
       {
           DALUser myDALUser = new DALUser();
           return myDALUser.GetUserRoles(TypeID, dbname);
       }
       public BEUser GetUser(int UserID,string dbname)
       {
           BEUser beuser = new BEUser();

           MySqlDataReader mySqlDataReader;



           DALUser myDALUser = new DALUser();
           mySqlDataReader = myDALUser.GetUser(UserID, dbname);

           while (mySqlDataReader.Read())
           {

               beuser.UserID = int.Parse(mySqlDataReader["UserID"].ToString());
               beuser.UserCode = mySqlDataReader["UserCode"].ToString();
               beuser.UserName = mySqlDataReader["UserName"].ToString();
               beuser.Photo = mySqlDataReader["Photo"].ToString();
               beuser.LoginName = mySqlDataReader["LoginName"].ToString();
               beuser.MobileNo = mySqlDataReader["MobileNo"].ToString();
               beuser.EmailID = mySqlDataReader["EmailID"].ToString();
               beuser.usertype = int.Parse(mySqlDataReader["usertypeid"].ToString());
              beuser.Password = mySqlDataReader["Password"].ToString();
               //beuser.AddressLine1 = mySqlDataReader["AddressLine1"].ToString();
               //beuser.AddressLine2 = mySqlDataReader["AddressLine2"].ToString();
               //beuser.City = mySqlDataReader["City"].ToString();
               //beuser.State = mySqlDataReader["State"].ToString();
              

           }
           mySqlDataReader.Close();
           return beuser;
       }
    }
}
