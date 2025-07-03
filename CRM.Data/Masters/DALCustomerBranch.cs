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
    public class DALCustomerBranch : Base.Data.BaseSql
    {
        public DataTable GetCustomerList(string dbname)
        {
            string strSql = @"select customer_branch_id,customer_branch_code ,customer_branch_Name,customer_Name,address_line_1,address_line_2,contact_person,phone_no from customerbranch a, customers b WHERE a.Active=1 and b.Active=1 and a.customer_id=b.customer_ID";

            DataSet dsResult = RunSQLString(strSql, dbname);
            return dsResult.Tables[0];
        }
        public DataTable GetCustomer(string dbname)
        {
            string strSql = @"SELECT customer_ID,customer_Name FROM customers where Active=1;";
            
            DataSet dsResult = RunSQLString(strSql, dbname);
            return dsResult.Tables[0];
        }
        public MySqlDataReader GetCustomer(int CompanyId, int customer_branch_id, string dbname)
        {
            string strSql = @" SELECT customer_branch_id,customer_branch_code,customer_branch_Name ,customer_id,address_line_1,address_line_2,contact_person,phone_no,ServiceLocation,Remarks from customerbranch where customer_branch_id=" + customer_branch_id + " and Active=1";
          

            return RunProcedureWithOutParameter(strSql,dbname);
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
        public int InsertOrUpdateRecord(BECustomerBranch br, string dbname)
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
                            if (br.CustomerBranchID == 0)
                            {
                                cmd.CommandText = @" INSERT INTO customerbranch(customer_id,customer_branch_code, customer_branch_Name,address_line_1,address_line_2,contact_person,phone_no,ServiceLocation,Remarks, Created_By,Modified_By,Active)
                                             VALUES (@customer_id,@CustomerBranchCode, @CustomerBranchName,@AddressLine1,@AddressLine2,@ContactPerson,@PhoneNo,@ServiceLocation,@Remarks, @createdby, @modifiedby,1) ";
                            }
                            else
                            {
                                cmd.CommandText = @" UPDATE customerbranch SET customer_id=@customer_id,customer_branch_code = @CustomerBranchCode, customer_branch_Name = @CustomerBranchName,address_line_1=@AddressLine1,address_line_2=@AddressLine2,contact_person=@ContactPerson,phone_no=@PhoneNo,ServiceLocation=@ServiceLocation,Remarks=@Remarks, Modified_By=@modifiedby WHERE  customer_branch_id = @customerBranchId ";

                            }
                            cmd.Parameters.Add(AddTypedParam("@createdby", MySqlDbType.Int32, br.createdby));
                            cmd.Parameters.Add(AddTypedParam("@modifiedby", MySqlDbType.Int64, br.modifiedby));
                            cmd.Parameters.Add(AddTypedParam("@CustomerBranchCode", MySqlDbType.VarChar, br.CustomerBranchCode));
                            cmd.Parameters.Add(AddTypedParam("@CustomerBranchName", MySqlDbType.VarChar, br.CustomerBranchName));
                            cmd.Parameters.Add(AddTypedParam("@customer_id", MySqlDbType.Int32, br.CustomerID));
                            cmd.Parameters.Add(AddTypedParam("@AddressLine1", MySqlDbType.VarChar, br.AddressLine1));
                            cmd.Parameters.Add(AddTypedParam("@AddressLine2", MySqlDbType.VarChar, br.AddressLine2));
                            cmd.Parameters.Add(AddTypedParam("@ContactPerson", MySqlDbType.VarChar, br.ContactPerson));
                            cmd.Parameters.Add(AddTypedParam("@PhoneNo", MySqlDbType.VarChar, br.PhoneNo));
                            cmd.Parameters.Add(AddTypedParam("@ServiceLocation", MySqlDbType.VarChar, br.ServiceLocation));
                            cmd.Parameters.Add(AddTypedParam("@Remarks", MySqlDbType.VarChar, br.Remarks));
                            cmd.Parameters.Add(AddTypedParam("@customerBranchId", MySqlDbType.Int32, br.CustomerBranchID));
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
        public int DeleteCustomer(int CustomerID, int modifiedBy, string dbname)
        {
            int m_rowsAffected = 0;
            try
            {
                string strSql = " Update customerbranch Set Active = 0, Modified_By=@modifiedBy, Modified_DT=Now() Where customer_branch_id = @customerBranchId ";


                MySqlParameter[] parameters = { 
                                                new MySqlParameter("@customerBranchId", MySqlDbType.Int64),
                                                new MySqlParameter("@ModifiedBy", MySqlDbType.Int64)};

                parameters[0].Value = CustomerID;
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

        public int InsertBulkRecord(DataTable dtTable,int id, string dbname)
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
                            foreach (DataRow objRow in dtTable.Rows)
                            {
                                cmd.CommandText = @" INSERT INTO customerbranch(customer_id,customer_branch_code, customer_branch_Name,address_line_1,address_line_2,contact_person,phone_no,ServiceLocation,Remarks, Created_By,Modified_By,Active)
                                             VALUES (@customer_id,@CustomerBranchCode, @CustomerBranchName,@AddressLine1,@AddressLine2,@ContactPerson,@PhoneNo,@ServiceLocation,@Remarks, @createdby,@modifiedby,1) ";



                                cmd.Parameters.Add(AddTypedParam("@createdby", MySqlDbType.Int32, id));
                                cmd.Parameters.Add(AddTypedParam("@modifiedby", MySqlDbType.Int64, id));
                                cmd.Parameters.Add(AddTypedParam("@CustomerBranchCode", MySqlDbType.VarChar, objRow["customer_branch_code"]));
                                cmd.Parameters.Add(AddTypedParam("@CustomerBranchName", MySqlDbType.VarChar, objRow["customer_branch_Name"]));
                                cmd.Parameters.Add(AddTypedParam("@customer_id", MySqlDbType.Int32, objRow["customer_ID"]));
                                cmd.Parameters.Add(AddTypedParam("@AddressLine1", MySqlDbType.VarChar, objRow["address_line_1"]));
                                cmd.Parameters.Add(AddTypedParam("@AddressLine2", MySqlDbType.VarChar, objRow["address_line_2"]));
                                cmd.Parameters.Add(AddTypedParam("@ContactPerson", MySqlDbType.VarChar, objRow["contact_person"]));
                                cmd.Parameters.Add(AddTypedParam("@PhoneNo", MySqlDbType.VarChar, objRow["phone_no"]));
                                cmd.Parameters.Add(AddTypedParam("@ServiceLocation", MySqlDbType.VarChar, objRow["ServiceLocation"]));
                                cmd.Parameters.Add(AddTypedParam("@Remarks", MySqlDbType.VarChar, objRow["remarks"]));


                                cmd.ExecuteNonQuery();

                                cmd.Parameters.Clear();
                            }

                            cmd.CommandText = "truncate table temp_customerbranch; ";
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                        iResult = 1;
                        conn.Close();

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
    }
}
