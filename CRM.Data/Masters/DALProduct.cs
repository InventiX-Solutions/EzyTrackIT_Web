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
    public class DALProduct : Base.Data.BaseSql
    {
        public DataTable GetProductList(string dbname)
        {
           // string strSql = @"select product_id,product_code,product_name,model_id,brand_id,ProductSerialNo from products WHERE Active=1";
            string strSql = @"select product_id,product_code,product_name from products where Active=1";

            MySqlParameter[] parameters = { new MySqlParameter("@CompanyId", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }

        public DataTable GetModelList(string dbname)
        {
            string strSql = @"select model_id,model_code ,model_name from models WHERE Active=1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyId", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public DataTable GetBrandList(string dbname)
        {
            string strSql = @"select brand_id,brand_code ,brand_name from brands WHERE Active=1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyId", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname,parameters, "MyDataTable");
            return dsResult.Tables[0];
        }

        //added Mano//
        public DataTable GetJobType(string dbname)
        {
            string strSql = @"select brand_id,brand_code ,brand_name from brands WHERE Active=1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyId", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }

        //End mano//

        public MySqlDataReader GetProduct(int CompanyId, int product_id,string dbname)
        {
            string strSql = @" select p.product_id,p.product_code,p.product_name,p.model_id,p.brand_id,p.PartNumber,m.model_name,b.brand_name from products p,models m,brands b where p.model_id=m.model_id and p.brand_id=b.brand_id and p.product_id=" + product_id + " and p.Active=1";


            return RunProcedureWithOutParameter(strSql, dbname);
        }

        public int InsertOrUpdateRecord(BEProduct br,string dbname)
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
                            if (br.product_id == 0)
                            {
                                cmd.CommandText = @" INSERT INTO products(product_code,product_name,Created_By,Modified_By,Active)
                                             VALUES (@product_code, @product_name, @Created_By, @Modified_By,1) ";
                            }
                            else
                            {
                                cmd.CommandText = @" UPDATE products SET product_code = @product_code, product_name = @product_name,Modified_By=@Modified_By WHERE  product_id = @product_id and Active=1";

                            }
                            cmd.Parameters.Add(AddTypedParam("@Created_By", MySqlDbType.Int32, br.Created_By));
                            cmd.Parameters.Add(AddTypedParam("@Modified_By", MySqlDbType.Int64, br.Modified_By));
                            cmd.Parameters.Add(AddTypedParam("@product_code", MySqlDbType.VarChar, br.product_code));
                            cmd.Parameters.Add(AddTypedParam("@product_name", MySqlDbType.VarChar, br.product_name));
                                             
                            cmd.Parameters.Add(AddTypedParam("@product_id", MySqlDbType.VarChar, br.product_id));
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
        public int DeleteProduct(int product_id, int Modified_By,string dbname)
        {
            int m_rowsAffected = 0;
            try
            {
                string strSql = " Update products Set Active = 0, Modified_By=@Modified_By, Modified_DT=Now() Where product_id = @product_id ";


                MySqlParameter[] parameters = { 
                                                new MySqlParameter("@product_id", MySqlDbType.Int64),
                                                new MySqlParameter("@Modified_By", MySqlDbType.Int64)};

                parameters[0].Value = product_id;
                parameters[1].Value = Modified_By;

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
        public DataTable LoadDropDownList(string selectedTextName, string selectedValueName, string tableName, string filterCondition,string dbname)
        {
            DataTable dtResult = new DataTable();
            string sqlStr = string.Empty;
            //sqlStr = "Select '' Name, '' Value UNION ALL ";

            sqlStr += " Select " + selectedTextName + " Name, " + selectedValueName + " Value" + " From " + tableName + " where 1=1";
            if (!string.IsNullOrEmpty(filterCondition))
            {
                sqlStr += " And " + filterCondition;
            }
            DataSet dsResult = RunSQLString(sqlStr, dbname, "MyDataTable");

            return dsResult.Tables[0];
        }

        private string GetConnectionString(string dbname)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["PeritusConnectionString"].ConnectionString);
            builder.Database = dbname;
            return builder.ToString();
        }

    }
}
