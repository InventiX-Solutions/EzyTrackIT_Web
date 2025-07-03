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
    public class DALServiveType : Base.Data.BaseSql
    {
        public DataTable GetServiceTypeList(string dbname)
        {
            string strSql = @"select service_typeid,service_type_code ,service_type_name from service_type WHERE Active=1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyId", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
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
        public int InsertOrUpdateRecord(BEServiceType br,string dbname)
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
                            if (br.ServiceTypeID == 0)
                            {
                                cmd.CommandText = @" INSERT INTO service_type(service_type_code,  service_type_name, Created_By,Modified_By,Active)
                                             VALUES (@ServiceTypeCode, @ServiceTypeName, @createdby, @modifiedby,1) ";
                            }
                            else
                            {
                                cmd.CommandText = @" UPDATE service_type SET service_type_code = @ServiceTypeCode, service_type_name = @ServiceTypeName, Modified_By=@modifiedby WHERE  service_typeid = @ServiceTypeID ";

                            }
                            cmd.Parameters.Add(AddTypedParam("@createdby", MySqlDbType.Int32, br.createdby));
                            cmd.Parameters.Add(AddTypedParam("@modifiedby", MySqlDbType.Int64, br.modifiedby));
                            cmd.Parameters.Add(AddTypedParam("@ServiceTypeCode", MySqlDbType.VarChar, br.ServiceTypeCode));
                            cmd.Parameters.Add(AddTypedParam("@ServiceTypeName", MySqlDbType.VarChar, br.ServiceTypeName));
                            cmd.Parameters.Add(AddTypedParam("@ServiceTypeID", MySqlDbType.VarChar, br.ServiceTypeID));
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
        public int DeleteServicetype(int service_typeid, int modifiedBy,string dbname)
        {
            int m_rowsAffected = 0;
            try
            {
                string strSql = " Update service_type Set Active = 0, Modified_By=@modifiedBy, Modified_DT=Now() Where service_typeid = @service_typeid ";


                MySqlParameter[] parameters = { 
                                                new MySqlParameter("@service_typeid", MySqlDbType.Int64),
                                                new MySqlParameter("@ModifiedBy", MySqlDbType.Int64)};

                parameters[0].Value = service_typeid;
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
