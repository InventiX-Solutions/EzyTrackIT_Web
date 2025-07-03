using Base.Data;
using CRM.Artifacts.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace CRM.Data.Security
{
    public class DALUser : BaseSql
    {
        public DataSet GetLoginInfo(BEUsers myBEUsers, string dbname)
        {
            MySqlParameter[] parameters ={  new MySqlParameter("IcompanyID", MySqlDbType.Int32),
                                            new MySqlParameter("VUserName", MySqlDbType.VarChar,50),
                                            new MySqlParameter("VPassword", MySqlDbType.VarChar,50)
                                        };

            parameters[0].Value = myBEUsers.CompanyID;
            parameters[1].Value = myBEUsers.UserName;
            parameters[2].Value = myBEUsers.UserPassword;


            DataSet dsResult = RunStoredProcedure("usp_getlogininfo", dbname, parameters, "MyDataTable");
            return dsResult;
        }

        private MySqlConnection CreateConnection(string connStr)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            return conn;
        }
       
    }
}
