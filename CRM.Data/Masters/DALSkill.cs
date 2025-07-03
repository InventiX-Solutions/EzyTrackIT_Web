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
    public class DALSkill : Base.Data.BaseSql
    {
        public DataTable GetSkillList(string dbname)
        {
            string strSql = @"select SkillID,SkillCode ,SkillName from engineerskill WHERE Active=1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyId", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname,parameters, "MyDataTable");
            return dsResult.Tables[0];
        }

        public int InsertOrUpdateRecord(BESkill br,string dbname)
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
                            if (br.SkillID == 0)
                            {
                                cmd.CommandText = @" INSERT INTO engineerskill(CompanyID,SkillCode,  SkillName, CreatedBy,ModifiedBy,Active)
                                             VALUES (@CompanyID,@SkillCode, @SkillName, @createdby, @modifiedby,1) ";
                            }
                            else
                            {
                                cmd.CommandText = @" UPDATE engineerskill SET SkillCode = @SkillCode, SkillName = @SkillName, ModifiedBy=@modifiedby WHERE  SkillID = @SkillID ";

                            }
                            cmd.Parameters.Add(AddTypedParam("@createdby", MySqlDbType.Int32, br.createdby));
                            cmd.Parameters.Add(AddTypedParam("@modifiedby", MySqlDbType.Int64, br.modifiedby));                           
                            cmd.Parameters.Add(AddTypedParam("@SkillCode", MySqlDbType.VarChar, br.SkillCode));
                            cmd.Parameters.Add(AddTypedParam("@SkillName", MySqlDbType.VarChar, br.SkillName));
                            cmd.Parameters.Add(AddTypedParam("@SkillID", MySqlDbType.VarChar, br.SkillID));
                            cmd.Parameters.Add(AddTypedParam("@CompanyID", MySqlDbType.Int64, br.CompanyID));
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
        public int CheckDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id,string dbname)
        {
            int dtResult = 0;
            string sqlStr = string.Empty;

            if (id == 0)
                sqlStr = " Select Count(*) From " + tableName + " Where  Active = 1 And lower(" + columnName + ") = @codeORName ";
            else
                sqlStr = " Select Count(*) From " + tableName + " Where  Active = 1 And lower(" + columnName + ") = @codeORName and " + idcolumnname + "<>" + id;

            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64), new MySqlParameter("@codeORName", MySqlDbType.VarChar) };

            parameters[0].Value = companyID;
            parameters[1].Value = codeORName;

            dtResult = RunExecuteScalarSQLIntWithParams(sqlStr, dbname, parameters);
            return dtResult;
        }
        public int DeleteEngineer(int SkillID, int modifiedBy,string dbname)
        {
            int m_rowsAffected = 0;
            try
            {
                string strSql = " Update engineerskill Set Active = 0, ModifiedBy=@modifiedBy, ModifiedDT=Now() Where SkillID = @SkillID ";


                MySqlParameter[] parameters = { 
                                                new MySqlParameter("@SkillID", MySqlDbType.Int64),
                                                new MySqlParameter("@ModifiedBy", MySqlDbType.Int64)};

                parameters[0].Value = SkillID;
                parameters[1].Value = modifiedBy;

                m_rowsAffected = RunSQLWithParam(strSql, dbname,parameters, out m_rowsAffected);
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

