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
    public class DALBrand : Base.Data.BaseSql
    {
        
        public DataTable GetBrandList(string dbname)
        {
            string strSql = @"select b.brand_id,b.brand_code ,b.brand_name,p.product_code as Product from brands b, products p WHERE b.Active=1 and p.active=1 and b.product_id=p.product_id";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyId", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql,dbname, parameters, "MyDataTable");
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

            dtResult = RunExecuteScalarSQLIntWithParams(sqlStr,dbname, parameters);
            return dtResult;
        }
        public int InsertOrUpdateRecord(BEBrand br,string dbname)
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
                            if (br.BrandID == 0)
                            {
                                cmd.CommandText = @" INSERT INTO brands(brand_code,  brand_name, Created_By,Modified_By,Active,product_id)
                                             VALUES (@BrandCode, @BrandName, @createdby, @modifiedby,1,@product_id) ";
                            }
                            else
                            {
                                cmd.CommandText = @" UPDATE brands SET brand_code = @BrandCode, brand_name = @BrandName, Modified_By=@modifiedby,product_id=@product_id WHERE  brand_id = @brand_id ";

                            }
                            cmd.Parameters.Add(AddTypedParam("@createdby", MySqlDbType.Int32, br.createdby));
                            cmd.Parameters.Add(AddTypedParam("@modifiedby", MySqlDbType.Int64, br.modifiedby));
                            cmd.Parameters.Add(AddTypedParam("@BrandCode", MySqlDbType.VarChar, br.BrandCode));
                            cmd.Parameters.Add(AddTypedParam("@BrandName", MySqlDbType.VarChar, br.BrandName));
                            cmd.Parameters.Add(AddTypedParam("@brand_id", MySqlDbType.VarChar, br.BrandID));
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
        public int DeleteBrand( int BrandID, int modifiedBy, string dbname)
        {
            int m_rowsAffected = 0;
            try
            {
                string strSql = " Update brands Set Active = 0, Modified_By=@modifiedBy, Modified_DT=Now() Where brand_id = @BrandID ";


                MySqlParameter[] parameters = { 
                                                new MySqlParameter("@BrandID", MySqlDbType.Int64),
                                                new MySqlParameter("@ModifiedBy", MySqlDbType.Int64)};
               
                parameters[0].Value = BrandID;
                parameters[1].Value = modifiedBy;

                m_rowsAffected = RunSQLWithParam(strSql,dbname, parameters, out m_rowsAffected);
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
