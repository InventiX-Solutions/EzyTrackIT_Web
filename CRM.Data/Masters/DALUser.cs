using CRM.Artifacts;
using CRM.Artifacts.Masters;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data.Masters
{
    public class DALUser : Base.Data.BaseSql
    {
        public int DeleteUser(int UserID, int modifiedBy,string dbname)
        {
            int m_rowsAffected = 0;
            try
            {
                string strSql = " Update usersetup Set Active = 0, Modified_By=@modifiedBy, Modified_DT=Now() Where UserID = @UserID ";


                MySqlParameter[] parameters = { 
                                                new MySqlParameter("@UserID", MySqlDbType.Int64),
                                                new MySqlParameter("@ModifiedBy", MySqlDbType.Int64)};

                parameters[0].Value = UserID;
                parameters[1].Value = modifiedBy;

                m_rowsAffected = RunSQLWithParam(strSql, dbname,parameters, out m_rowsAffected);
            }
            catch (Exception)
            {
                m_rowsAffected = 0;
            }

            return m_rowsAffected;
        }
        public DataTable GetUserlist(string dbname)
        {
            string strSql = @"select UserID, UserCode, UserName,LoginName,MobileNo,EmailID from usersetup WHERE Active=1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyId", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public int InsertOrUpdateRecord(BEUser br,string dbname)
        {
            int iResult = 0;

            using (MySqlConnection conn = CreateConnection(GetConnectionString(dbname)))
            {
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            if (br.UserID == 0)
                            {
                                cmd.CommandText = @" INSERT INTO usersetup (CompanyID,UserCode, UserName,Password,Photo,LoginName,MobileNo,EmailID, Created_By,Modified_By,Active,usertypeid)
                                             VALUES (1,@UserCode, @UserName,@Password,@Photo,@LoginName,@MobileNo,@EmailID, @createdby, @modifiedby,1,@usertypeid) ";
                            }
                            else
                            {
                                cmd.CommandText = @" UPDATE usersetup SET UserCode = @UserCode, UserName = @UserName,Password=@Password,Photo=@Photo,LoginName=@LoginName,MobileNo=@MobileNo,EmailID=@EmailID,Modified_By=@modifiedby,usertypeid=@usertypeid WHERE  UserID = @UserID ";

                            }
                            
                            cmd.Parameters.Add(AddTypedParam("@UserCode", MySqlDbType.VarChar, br.UserCode));
                            cmd.Parameters.Add(AddTypedParam("@UserName", MySqlDbType.VarChar, br.UserName));
                            cmd.Parameters.Add(AddTypedParam("@Password", MySqlDbType.VarChar, br.Password));
                            cmd.Parameters.Add(AddTypedParam("@Photo", MySqlDbType.VarChar, br.Photo));
                            cmd.Parameters.Add(AddTypedParam("@LoginName", MySqlDbType.VarChar, br.LoginName));
                            cmd.Parameters.Add(AddTypedParam("@MobileNo", MySqlDbType.VarChar, br.MobileNo));
                            cmd.Parameters.Add(AddTypedParam("@EmailID", MySqlDbType.VarChar, br.EmailID));
                            cmd.Parameters.Add(AddTypedParam("@createdby", MySqlDbType.Int32, br.createdby));
                            cmd.Parameters.Add(AddTypedParam("@modifiedby", MySqlDbType.Int64, br.modifiedby));
                            cmd.Parameters.Add(AddTypedParam("@UserID", MySqlDbType.Int32, br.UserID));
                            cmd.Parameters.Add(AddTypedParam("@usertypeid", MySqlDbType.Int32, br.usertype));
                            cmd.ExecuteNonQuery();

                            cmd.Parameters.Clear();

                        }
                        trans.Commit();
                        iResult = 1;
                    }
                    catch (Exception ex)
                    {

                        trans.Rollback();
                        iResult = 0;

                    }
                }
            }
            return iResult;

        }
        public DataTable GetUserRoles(int TypeID,string dbname)
        {
            DataTable dtResult = new DataTable();
            string sqlStr = @"select m.ScreenID,m.MenuScreenName as ScreenName,c.CategoryName as Category,ifnull(r.access,0) as ViewUser,
                                (select usertype from usertype  where Active=1 and typeid=@TypeID) as usertype From 
                                menucategory c,menuscreens m left join usertype_roles r on m.ScreenID=r.screen_id and r.user_typeid=@TypeID
                                where m.CategoryID=c.CategoryID and m.Active=1 and c.Active=1";
            MySqlParameter[] parameters = { new MySqlParameter("@TypeID", MySqlDbType.Int64) };
            //set values
            parameters[0].Value = TypeID;

            DataSet dsResult = RunSQLWithParam(sqlStr, dbname, parameters, "MyDataTable");

            return dsResult.Tables[0];
        }
        public int CheckDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id,string dbname)
        {
            int dtResult = 0;
            string sqlStr = string.Empty;

            if (id == 0)
                sqlStr = " Select Count(*) From " + tableName + " Where  Active = 1 And lower(" + columnName + ") = @codeORName ";
            else
                sqlStr = " Select Count(*) From " + tableName + " Where  Active = 1 And lower(" + columnName + ") = @codeORName and " + idcolumnname + "<>" + id;

            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64), new MySqlParameter("@codeORName", MySqlDbType.VarChar) };
            //set values
            parameters[0].Value = companyID;
            parameters[1].Value = codeORName;

            dtResult = RunExecuteScalarSQLIntWithParams(sqlStr, dbname, parameters);
            return dtResult;
        }
        public MySqlDataReader GetUser(int UserID,string dbname)
        {
            string strSql = @" SELECT UserID, UserCode, UserName, Password,  Photo,LoginName,MobileNo,EmailID,usertypeid from usersetup where UserID=" + UserID + " and Active=1";


            return RunProcedureWithOutParameter(strSql, dbname);
        }
        private MySqlConnection CreateConnection(string connStr)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            return conn;
        }

        private MySqlParameter AddTypedParam(string paraName, MySqlDbType sQLType, object value)
        {
            MySqlParameter parm = new MySqlParameter(paraName, sQLType);
            parm.Value = value;
            return parm;
        }
        public string ValidateRegisteredClient(string Clientcode)
        {
            string message = string.Empty;

            string sql = "select db_name from clients where client_code='" + Clientcode + "' and Active=1";
            string variable;
            using (var connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["PeritusConnectionString"].ConnectionString))
            using (var command = new MySqlCommand(sql, connection))
            {
                //command.Parameters.AddWithValue("@Parameter", someValue);
                connection.Open();
                variable = (string)command.ExecuteScalar();
                if (!string.IsNullOrEmpty(variable))
                {
                    message = variable;
                }
                else
                {
                    message = "failed";
                }
            }
            return message;
            
        }
        private string GetConnectionString(string dbname)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["PeritusConnectionString"].ConnectionString);
            builder.Database = dbname;
            return builder.ToString();
        }
    }
}
