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
    public class DALEngineerSkilllevel : Base.Data.BaseSql
    {
        public DataTable GetSkilllevelList(string dbname)
        {
            string strSql = @"select b.EngineerSkilllevelID,b.EngineerSkilllevelCode ,b.EngineerSkilllevelName from engineerskilllevel b WHERE b.Active=1 ";
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
        public int InsertOrUpdateRecord(BEEngineerSkilllevel br, string dbname)
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
                            if (br.EngineerSkilllevelID == 0)
                            {
                                cmd.CommandText = @" INSERT INTO engineerskilllevel(EngineerSkilllevelCode,  EngineerSkilllevelName, Created_By,Modified_By,Active)
                                             VALUES (@EngineerSkilllevelCode, @EngineerSkilllevelName, @createdby, @modifiedby,1) ";
                            }
                            else
                            {
                                cmd.CommandText = @" UPDATE engineerskilllevel SET EngineerSkilllevelCode = @EngineerSkilllevelCode, EngineerSkilllevelName = @EngineerSkilllevelName, Modified_By=@modifiedby WHERE  EngineerSkilllevelID = @EngineerSkilllevelID ";

                            }
                            cmd.Parameters.Add(AddTypedParam("@createdby", MySqlDbType.Int32, br.createdby));
                            cmd.Parameters.Add(AddTypedParam("@modifiedby", MySqlDbType.Int64, br.modifiedby));
                            cmd.Parameters.Add(AddTypedParam("@EngineerSkilllevelCode", MySqlDbType.VarChar, br.EngineerSkilllevelCode));
                            cmd.Parameters.Add(AddTypedParam("@EngineerSkilllevelName", MySqlDbType.VarChar, br.EngineerSkilllevelName));
                            cmd.Parameters.Add(AddTypedParam("@EngineerSkilllevelID", MySqlDbType.VarChar, br.EngineerSkilllevelID));
                           
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
        public int DeleteBrand(int LevelID, int modifiedBy, string dbname)
        {
            int m_rowsAffected = 0;
            try
            {
                string strSql = " Update engineerskilllevel Set Active = 0, Modified_By=@modifiedBy, Modified_DT=Now() Where EngineerSkilllevelID = @EngineerSkilllevelID ";


                MySqlParameter[] parameters = { 
                                                new MySqlParameter("@EngineerSkilllevelID", MySqlDbType.Int64),
                                                new MySqlParameter("@ModifiedBy", MySqlDbType.Int64)};

                parameters[0].Value = LevelID;
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
