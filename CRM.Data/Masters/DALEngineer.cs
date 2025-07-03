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
    public class DALEngineer : Base.Data.BaseSql
    {
        public DataTable GetSkilllist(string dbname)
        {
            string strSql = @"SELECT SkillID,SkillCode,SkillName FROM engineerskill where Active=1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyId", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }

        public DataTable Getvehiclelist(string dbname)
        {
            string strSql = @"SELECT vehicleID,vehicleCode,vehicleName,vehicleno, CONCAT(vehicleName, ' / ', vehicleno) AS Vehicle FROM engineervehicle where Active=1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyId", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }

        public DataTable GetengineerList(string dbname)
        {
          //  string strSql = @"select e.engineer_id,e.engineer_code ,e.engineer_name,SkillName,e.mobileno,e.emailid,e.password,s.vehiclename from engineers e,engineerskill s,engineervehicle v WHERE e.Active=1 and e.SkillID=s.SkillID  and e.VehicleID =v.VehicleID ";
           
          //  string strSql = @"SELECT e.engineer_id, e.engineer_code, e.engineer_name, s.SkillName, e.mobileno, e.emailid, e.password,Engineertype, v.vehiclename as vehicle FROM engineers e JOIN engineerskill s ON e.SkillID = s.SkillID LEFT JOIN engineervehicle v ON e.VehicleID = v.VehicleID AND e.company_id = v.CompanyID AND v.Active = 1 WHERE e.Active = 1";
            string strSql = @"SELECT e.engineer_id, e.engineer_code, e.engineer_name, s.SkillName, e.mobileno, e.emailid, e.password, CASE WHEN e.EngineerType = 'E' THEN 'Engineer' WHEN e.EngineerType = 'S' THEN 'Supervisor' ELSE '' END AS Type,CONCAT(v.vehicleName, ' / ', v.vehicleno) AS Vehicle, v.vehiclename AS vehiclename FROM engineers e JOIN engineerskill s ON e.SkillID = s.SkillID LEFT JOIN engineervehicle v ON e.VehicleID = v.VehicleID AND e.company_id = v.CompanyID AND v.Active = 1 WHERE e.Active = 1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyId", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }

        public int InsertOrUpdateRecord(BEEngineer br,string dbname)
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
                            if (br.EngineerID == 0)
                            {
                                cmd.CommandText = @" INSERT INTO engineers(engineer_code,  engineer_name,SkillID,mobileno,emailid, Created_By,Modified_By,Active,password,company_id,VehicleID,Engineertype)
                                             VALUES (@EngineerCode, @EngineerName,@SkillID,@MobileNo,@EmailID, @createdby, @modifiedby,1,@password,1,@VehicleID,@EngineerType) ";
                            }
                            else
                            {
                                cmd.CommandText = @" UPDATE engineers SET password=@password,engineer_code = @EngineerCode, engineer_name = @EngineerName,SkillID=@SkillID,mobileno = @MobileNo,emailid = @EmailID, Modified_By=@modifiedby,VehicleID= @VehicleID ,Engineertype=@EngineerType WHERE  engineer_id = @EngineerID ";

                            }
                            cmd.Parameters.Add(AddTypedParam("@createdby", MySqlDbType.Int32, br.createdby));
                            cmd.Parameters.Add(AddTypedParam("@modifiedby", MySqlDbType.Int64, br.modifiedby));
                            cmd.Parameters.Add(AddTypedParam("@EngineerCode", MySqlDbType.VarChar, br.EngineerCode));
                            cmd.Parameters.Add(AddTypedParam("@EngineerName", MySqlDbType.VarChar, br.EngineerName));
                            
                            cmd.Parameters.Add(AddTypedParam("@SkillID", MySqlDbType.VarChar, br.SkillID));
                            cmd.Parameters.Add(AddTypedParam("@MobileNo", MySqlDbType.VarChar, br.MobileNo));
                            cmd.Parameters.Add(AddTypedParam("@EmailID", MySqlDbType.VarChar, br.EmailID));
                            cmd.Parameters.Add(AddTypedParam("@EngineerID", MySqlDbType.VarChar, br.EngineerID));
                            cmd.Parameters.Add(AddTypedParam("@password", MySqlDbType.VarChar, br.password));
                            cmd.Parameters.Add(AddTypedParam("@VehicleID", MySqlDbType.VarChar, br.vehicleID));
                            cmd.Parameters.Add(AddTypedParam("@EngineerType", MySqlDbType.VarChar, br.EngineerType));

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

            dtResult = RunExecuteScalarSQLIntWithParams(sqlStr, dbname,parameters);
            return dtResult;
        }
        public int DeleteEngineer(int EngineerID, int modifiedBy,string dbname)
        {
            int m_rowsAffected = 0;
            try
            {
                string strSql = " Update engineers Set Active = 0, Modified_By=@modifiedBy, Modified_DT=Now() Where engineer_id = @EngineerID ";


                MySqlParameter[] parameters = { 
                                                new MySqlParameter("@EngineerID", MySqlDbType.Int64),
                                                new MySqlParameter("@ModifiedBy", MySqlDbType.Int64)};

                parameters[0].Value = EngineerID;
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
