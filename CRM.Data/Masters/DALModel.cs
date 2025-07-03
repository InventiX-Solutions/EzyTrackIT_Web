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
    public class DALModel : Base.Data.BaseSql
    {
        public DataTable GetBrandlist(string dbname)
        {
            string strSql = @"select brand_id,brand_name from brands WHERE Active=1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyId", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];

          
        }
        public DataTable GetModelList(string dbname)
        {

            string strSql = @"select m.model_id,m.model_code ,m.model_name,m.Brand_ID,b.brand_name,p.product_name as Product from models m,brands b,products p
                                       WHERE m.Active=1 and m.brand_id=b.brand_id and m.product_id=p.product_id and b.product_id=p.product_id and p.active=1";
            //string strSql = @"select model_id,model_code ,model_name,Brand_ID from models WHERE Active=1";
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
        public int InsertOrUpdateRecord(BEModel br,string  dbname)
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
                            if (br.ModelID == 0)
                            {
                                cmd.CommandText = @" INSERT INTO models(model_code,  model_name,Brand_ID,product_id, Created_By,Modified_By,Active)
                                             VALUES (@ModelCode, @ModelName,@Brand_ID,@product_id, @createdby, @modifiedby,1) ";
                            }
                            else
                            {
                                cmd.CommandText = @" UPDATE models SET model_code = @ModelCode, model_name = @ModelName,Brand_ID=@Brand_ID,product_id=@product_id, Modified_By=@modifiedby WHERE  model_id = @ModelID ";

                            }
                            cmd.Parameters.Add(AddTypedParam("@createdby", MySqlDbType.Int32, br.createdby));
                            cmd.Parameters.Add(AddTypedParam("@modifiedby", MySqlDbType.Int64, br.modifiedby));
                            cmd.Parameters.Add(AddTypedParam("@ModelCode", MySqlDbType.VarChar, br.ModelCode));
                            cmd.Parameters.Add(AddTypedParam("@ModelName", MySqlDbType.VarChar, br.ModelName));
                            cmd.Parameters.Add(AddTypedParam("@ModelID", MySqlDbType.VarChar, br.ModelID));
                            cmd.Parameters.Add(AddTypedParam("@Brand_ID", MySqlDbType.VarChar, br.Brand_ID));
                            cmd.Parameters.Add(AddTypedParam("@product_id", MySqlDbType.VarChar, br.productid));
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
        public int DeleteModel(int ModelID, int modifiedBy,string dbname)
        {
            int m_rowsAffected = 0;
            try
            {
                string strSql = " Update models Set Active = 0, Modified_By=@modifiedBy, Modified_DT=Now() Where model_id = @ModelID ";


                MySqlParameter[] parameters = { 
                                                new MySqlParameter("@ModelID", MySqlDbType.Int64),
                                                new MySqlParameter("@ModifiedBy", MySqlDbType.Int64)};

                parameters[0].Value = ModelID;
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
