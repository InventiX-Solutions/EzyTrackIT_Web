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
    public class DALSeverity : Base.Data.BaseSql
    {
        public DataTable GetSeverityList(string dbname)
        {
            string strSql = @"select sevID,sevCode ,sevName from severity WHERE Active=1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyId", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public int CheckDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id, string dbname)
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
        public int InsertOrUpdateRecord(BESeverity br, string dbname)
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
                            if (br.sevID == 0)
                            {
                                cmd.CommandText = @" INSERT INTO severity(sevCode, sevName, CreatedBy,ModifiedBy,Active)
                                             VALUES (@sevCode, @sevName, @CreatedBy, @ModifiedBy,1) ";
                            }
                            else
                            {
                                cmd.CommandText = @" UPDATE severity SET sevCode = @sevCode, sevName = @sevName, ModifiedBy=@ModifiedBy WHERE  sevID = @sevID ";

                            }
                            cmd.Parameters.Add(AddTypedParam("@CreatedBy", MySqlDbType.Int32, br.CreatedBy));
                            cmd.Parameters.Add(AddTypedParam("@ModifiedBy", MySqlDbType.Int64, br.ModifiedBy));
                            cmd.Parameters.Add(AddTypedParam("@sevCode", MySqlDbType.VarChar, br.sevCode));
                            cmd.Parameters.Add(AddTypedParam("@sevName", MySqlDbType.VarChar, br.sevName));
                            cmd.Parameters.Add(AddTypedParam("@sevID", MySqlDbType.VarChar, br.sevID));
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
        public int DeleteSeverity(int SevID, int modifiedBy, string dbname)
        {
            int m_rowsAffected = 0;
            try
            {
                string strSql = " Update severity Set Active = 0, ModifiedBy=@ModifiedBy, ModifiedDT=Now() Where sevID = @sevID ";


                MySqlParameter[] parameters = { 
                                                new MySqlParameter("@sevID", MySqlDbType.Int64),
                                                new MySqlParameter("@ModifiedBy", MySqlDbType.Int64)};

                parameters[0].Value = SevID;
                parameters[1].Value = modifiedBy;

                m_rowsAffected = RunSQLWithParam(strSql, dbname, parameters, out m_rowsAffected);
            }
            catch (Exception)
            {
                m_rowsAffected = 0;
            }

            return m_rowsAffected;
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

        private string GetConnectionString(string dbname)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["PeritusConnectionString"].ConnectionString);
            builder.Database = dbname;
            return builder.ToString();
        }
    }
}
