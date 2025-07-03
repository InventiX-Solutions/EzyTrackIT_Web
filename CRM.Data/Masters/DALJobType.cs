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
   public class DALJobType : Base.Data.BaseSql
    {


        public DataTable GetJobTypeList(string dbname)
        {
            string strSql = @"SELECT JobTypeId,JobCode,JobTypes FROM jobtype where Active=1;"; 
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

      

        public int InsertOrUpdateRecord(BEJobType br, string dbname)
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
                            if (br.JobTypeID == 0)
                            {
                                cmd.CommandText = @" insert into jobtype (JobCode,JobTypes,Active,Created_By,Modified_By)values(@JobTypeCode,@JobTypName,1,@createdby,@modifiedby)";
                            }
                            else
                            {
                                cmd.CommandText = @" Update jobtype set JobCode= @JobTypeCode,JobTypes= @JobTypName, Modified_By= @modifiedby where JobTypeId= @JobTypeID ";

                            }
                            cmd.Parameters.Add(AddTypedParam("@createdby", MySqlDbType.Int32, br.createdby));
                            cmd.Parameters.Add(AddTypedParam("@modifiedby", MySqlDbType.Int32, br.modifiedby));
                            cmd.Parameters.Add(AddTypedParam("@JobTypeCode", MySqlDbType.VarChar, br.JobTypeCode));
                            cmd.Parameters.Add(AddTypedParam("@JobTypName", MySqlDbType.VarChar, br.JobTypName));
                            cmd.Parameters.Add(AddTypedParam("@JobTypeID", MySqlDbType.Int32, br.JobTypeID));
                          //  cmd.Parameters.Add(AddTypedParam("@product_id", MySqlDbType.VarChar, br.productid));
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
        public int DeleteJobType(int JobTypeID, int modifiedBy, string dbname)
        {
            int m_rowsAffected = 0;
            try
            {
                string strSql = " Update jobtype Set Active = 0, Modified_By=@modifiedBy, Modified_DT=Now() Where JobTypeId = @JobTypeID ";


                MySqlParameter[] parameters = {
                                                new MySqlParameter("@JobTypeID", MySqlDbType.Int64),
                                                new MySqlParameter("@ModifiedBy", MySqlDbType.Int64)};

                parameters[0].Value = JobTypeID;
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
