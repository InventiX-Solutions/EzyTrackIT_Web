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
    public class DALStatus : Base.Data.BaseSql
    {
        public DataTable GetStatusList(string dbname)
        {
            string strSql = @"select StatusID,StatusCode ,StatusName from status WHERE Active=1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyId", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public MySqlDataReader GetStatus(int CompanyId, int StatusID,string dbname)
        {
            string strSql = @" SELECT StatusID,StatusCode ,StatusName ,sequenceNo, StatusImage from status where StatusID=" + StatusID + " and Active=1";
            return RunProcedureWithOutParameter(strSql, dbname);
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
        public int InsertOrUpdateRecord(BEStatus br,string dbname)
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
                            if (br.StatusID == 0)
                            {
                                cmd.CommandText = @" INSERT INTO status(CompanyID,StatusCode,  StatusName,sequenceNo,StatusImage,CreatedBy,ModifiedBy,Active)
                                             VALUES (1,@StatusCode, @StatusName,@SequenceNo,@StatusImage,@createdBy, @ModifiedBy,1) ";
                            }
                            else
                            {
                                cmd.CommandText = @" UPDATE status SET StatusCode = @StatusCode, StatusName = @StatusName,sequenceNo=@SequenceNo,StatusImage=@StatusImage, ModifiedBy=@ModifiedBy WHERE  StatusID = @StatusID ";

                            }
                            cmd.Parameters.Add(AddTypedParam("@CreatedBy", MySqlDbType.Int32, br.CreatedBy));
                            cmd.Parameters.Add(AddTypedParam("@ModifiedBy", MySqlDbType.Int64, br.ModifiedBy));
                            cmd.Parameters.Add(AddTypedParam("@StatusCode", MySqlDbType.VarChar, br.StatusCode));
                            cmd.Parameters.Add(AddTypedParam("@StatusName", MySqlDbType.VarChar, br.StatusName));
                            cmd.Parameters.Add(AddTypedParam("@StatusID", MySqlDbType.VarChar, br.StatusID));
                            cmd.Parameters.Add(AddTypedParam("@SequenceNo", MySqlDbType.VarChar, br.SequenceNo));
                            cmd.Parameters.Add(AddTypedParam("@StatusImage", MySqlDbType.VarChar, br.StatusImage));
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
        public int DeleteStatus(int StatusID, int ModifiedBy,string dbname)
        {
            int m_rowsAffected = 0;
            try
            {
                string strSql = " Update status Set Active = 0, ModifiedBy=@ModifiedBy, ModifiedDt=Now() Where StatusID = @StatusID ";


                MySqlParameter[] parameters = { 
                                                new MySqlParameter("@StatusID", MySqlDbType.Int64),
                                                new MySqlParameter("@ModifiedBy", MySqlDbType.Int64)};

                parameters[0].Value = StatusID;
                parameters[1].Value = ModifiedBy;

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
