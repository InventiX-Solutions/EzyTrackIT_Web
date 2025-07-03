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
    public class DALCompany : Base.Data.BaseSql
    {
        public int DeleteCompany(int CompanyID, int modifiedBy, string dbname)
        {
            int m_rowsAffected = 0;
            try
            {
                string strSql = " Update company Set Active = 0, ModifiedBy=@modifiedBy, ModifiedDT=Now() Where CompanyID = @CompanyID ";


                MySqlParameter[] parameters = { 
                                                new MySqlParameter("@CompanyID", MySqlDbType.Int64),
                                                new MySqlParameter("@ModifiedBy", MySqlDbType.Int64)};

                parameters[0].Value = CompanyID;
                parameters[1].Value = modifiedBy;

                m_rowsAffected = RunSQLWithParam(strSql,dbname, parameters, out m_rowsAffected);
            }
            catch (Exception)
            {
                m_rowsAffected = 0;
            }

            return m_rowsAffected;
        }
        public DataTable GetCompanyList(string dbname)
        {
            //string strSql = @"select CompanyID, CompanyCode, CompanyName, AddressLine1, AddressLine2, City, State, Country, PostalCode, PhoneNumber, FaxNumber, Website, EmailID, RegistrationNo as TINNO, Remarks,  GMT from company WHERE Active=1";

            string strSql = @" select CompanyID, CompanyCode, CompanyName, AddressLine1, AddressLine2, City, State, Country, PostalCode, PhoneNumber, FaxNumber, Website, EmailID, RegistrationNo as TINNO, Remarks,SMTPPort,SMTPHost,SMTPUserName,SMTPPassword,MailCC,MailBCC,MailSubject,MailContent, Mobile_Menu_Attendance,Mobile_Menu_AttendanceHistry,Mobile_Menu_NewJob,Mobile_Menu_GetMyJobList,Mobile_Menu_CompletedJob,Mobile_Menu_MoreDetails,Mobile_Report_Send_to_Mail,Mobile_Report_download, GMT,EmailFlag from company WHERE Active=1 ";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyId", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql,dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public MySqlDataReader GetCompany(int CompanyID, string dbname)
        {
            //string strSql = @" SELECT CompanyID, CompanyCode, CompanyName, AddressLine1, AddressLine2, City, State, Country, PostalCode, PhoneNumber, FaxNumber, Website, EmailID, RegistrationNo as TINNO,ifnull(Logo,'') as Logo,ifnull(Logo2,'') as Logo2, Remarks,GMT from company where CompanyID=" + CompanyID + " and Active=1";
            string strSql = @" SELECT CompanyID, CompanyCode, CompanyName, AddressLine1, AddressLine2, City, State, Country, PostalCode, PhoneNumber, FaxNumber, Website, EmailID, RegistrationNo as TINNO,ifnull(Logo,'') as Logo,ifnull(Logo2,'') as Logo2, Remarks,SMTPPort,SMTPHost,SMTPUserName,SMTPPassword,MailCC,MailBCC,MailSubject,MailContent,Mobile_Menu_Attendance,Mobile_Menu_AttendanceHistry,Mobile_Menu_NewJob,Mobile_Menu_GetMyJobList,Mobile_Menu_CompletedJob,Mobile_Menu_MoreDetails,Mobile_Report_Send_to_Mail,Mobile_Report_download,GMT,EmailFlag from company where CompanyID=" + CompanyID + " and Active=1";

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
        public int InsertOrUpdateRecord(BECompany br, string dbname)
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
//                            if (br.CompanyID == 0)
//                            {
//                                cmd.CommandText = @" INSERT INTO company(CompanyCode, CompanyName, AddressLine1, AddressLine2, City, State, Country, PostalCode, PhoneNumber, FaxNumber, Website, EmailID, RegistrationNo, Remarks, CreatedBy,ModifiedBy,Active,Logo,Logo2)
//                                             VALUES (@CompanyCode, @CompanyName,@AddressLine1,@AddressLine2,@City,@State,@Country,@PostalCode,@PhoneNo,@FaxNo,@Website,@EmailID,@TINNO,@Remarks, @createdby, @modifiedby,1,@companylogo,@companylogo2) ";
//                            }
//                            else
//                            {
//                                cmd.CommandText = @" UPDATE company SET CompanyCode = @CompanyCode, CompanyName = @CompanyName,AddressLine1=@AddressLine1,AddressLine2=@AddressLine2,City=@City,State=@State,Country=@Country,PostalCode=@PostalCode,PhoneNumber=@PhoneNo,FaxNumber=@FaxNo,Website=@Website,EmailID=@EmailID,RegistrationNo=@TINNO,Remarks=@Remarks, ModifiedBy=@modifiedby,Logo=@companylogo,Logo2=@companylogo2 WHERE  CompanyID = @CompanyID ";

//                            }
                            if (br.CompanyID == 0)
                            {
                                cmd.CommandText = @" INSERT INTO company(CompanyCode, CompanyName, AddressLine1, AddressLine2, City, State, Country, PostalCode, PhoneNumber, FaxNumber, Website, EmailID, RegistrationNo, Remarks,GMT,SMTPPort,SMTPHost,SMTPUserName,SMTPPassword,MailCC,MailBCC,MailSubject,MailContent,Mobile_Menu_Attendance,Mobile_Menu_AttendanceHistry,Mobile_Menu_NewJob,Mobile_Menu_GetMyJobList,Mobile_Menu_CompletedJob,Mobile_Menu_MoreDetails,Mobile_Report_Send_to_Mail,Mobile_Report_download,  CreatedBy,ModifiedBy,Active,Logo,Logo2,EmailFlag)
                                             VALUES (@CompanyCode, @CompanyName,@AddressLine1,@AddressLine2,@City,@State,@Country,@PostalCode,@PhoneNo,@FaxNo,@Website,@EmailID,@TINNO,@Remarks,@GMT, @SMTPPort,@SMTPHost,@SMTPUserName,@SMTPPassword,@MailCC,@MailBCC,@MailSubject,@MailContent,@Mobile_Menu_Attendance,@Mobile_Menu_AttendanceHistry,@Mobile_Menu_NewJob,@Mobile_Menu_GetMyJobList,@Mobile_Menu_CompletedJob,@Mobile_Menu_MoreDetails,@Mobile_Report_Send_to_Mail,@Mobile_Report_download, @createdby, @modifiedby,1,@companylogo,@companylogo2,@EmailFlag) ";
                            }
                            else
                            {
                                cmd.CommandText = @" UPDATE company SET CompanyCode = @CompanyCode, CompanyName = @CompanyName,AddressLine1=@AddressLine1,AddressLine2=@AddressLine2,City=@City,State=@State,Country=@Country,PostalCode=@PostalCode,PhoneNumber=@PhoneNo,FaxNumber=@FaxNo,Website=@Website,EmailID=@EmailID,RegistrationNo=@TINNO,Remarks=@Remarks,GMT=@GMT,SMTPPort = @SMTPPort,SMTPHost = @SMTPHost,SMTPUserName=@SMTPUserName,SMTPPassword=@SMTPPassword,MailCC=@MailCC,MailBCC=@MailBCC,MailSubject=@MailSubject,MailContent=@MailContent,Mobile_Menu_Attendance=@Mobile_Menu_Attendance,Mobile_Menu_AttendanceHistry=@Mobile_Menu_AttendanceHistry,Mobile_Menu_NewJob=@Mobile_Menu_NewJob,Mobile_Menu_GetMyJobList=@Mobile_Menu_GetMyJobList,Mobile_Menu_CompletedJob=@Mobile_Menu_CompletedJob,Mobile_Menu_MoreDetails=@Mobile_Menu_MoreDetails,Mobile_Report_Send_to_Mail=@Mobile_Report_Send_to_Mail,Mobile_Report_download=@Mobile_Report_download,   ModifiedBy=@modifiedby,Logo=@companylogo,Logo2=@companylogo2, EmailFlag =@EmailFlag WHERE  CompanyID = @CompanyID ";

                            }
                            cmd.Parameters.Add(AddTypedParam("@createdby", MySqlDbType.Int32, br.createdby));
                            cmd.Parameters.Add(AddTypedParam("@modifiedby", MySqlDbType.Int64, br.modifiedby));
                            cmd.Parameters.Add(AddTypedParam("@CompanyCode", MySqlDbType.VarChar, br.CompanyCode));
                            cmd.Parameters.Add(AddTypedParam("@CompanyName", MySqlDbType.VarChar, br.CompanyName));
                            cmd.Parameters.Add(AddTypedParam("@AddressLine1", MySqlDbType.VarChar, br.AddressLine1));
                            cmd.Parameters.Add(AddTypedParam("@AddressLine2", MySqlDbType.VarChar, br.AddressLine2));
                            cmd.Parameters.Add(AddTypedParam("@City", MySqlDbType.VarChar, br.City));
                            cmd.Parameters.Add(AddTypedParam("@State", MySqlDbType.VarChar, br.State));
                            cmd.Parameters.Add(AddTypedParam("@Country", MySqlDbType.VarChar, br.Country));
                            cmd.Parameters.Add(AddTypedParam("@PostalCode", MySqlDbType.Int32, br.PostalCode));
                            cmd.Parameters.Add(AddTypedParam("@PhoneNo", MySqlDbType.VarChar, br.PhoneNo));
                            cmd.Parameters.Add(AddTypedParam("@FaxNo", MySqlDbType.VarChar, br.FaxNo));
                            cmd.Parameters.Add(AddTypedParam("@Website", MySqlDbType.VarChar, br.Website));
                            cmd.Parameters.Add(AddTypedParam("@EmailID", MySqlDbType.VarChar, br.EmailID));
                            cmd.Parameters.Add(AddTypedParam("@TINNO", MySqlDbType.VarChar, br.TINNO));
                            cmd.Parameters.Add(AddTypedParam("@companylogo", MySqlDbType.VarChar, br.companylogo));
                            cmd.Parameters.Add(AddTypedParam("@companylogo2", MySqlDbType.VarChar, br.companylogo2));
                            cmd.Parameters.Add(AddTypedParam("@Remarks", MySqlDbType.VarChar, br.Remarks));
                            cmd.Parameters.Add(AddTypedParam("@CompanyID", MySqlDbType.Int32, br.CompanyID));
                            cmd.Parameters.Add(AddTypedParam("@GMT", MySqlDbType.VarChar, br.GMT));

                            cmd.Parameters.Add(AddTypedParam("@SMTPPort", MySqlDbType.VarChar, br.SMTPPort));
                            cmd.Parameters.Add(AddTypedParam("@SMTPHost", MySqlDbType.VarChar, br.SMTPHost));
                            cmd.Parameters.Add(AddTypedParam("@SMTPUserName", MySqlDbType.VarChar, br.SMTPUserName));
                            cmd.Parameters.Add(AddTypedParam("@SMTPPassword", MySqlDbType.VarChar, br.SMTPPassword));
                            cmd.Parameters.Add(AddTypedParam("@MailCC", MySqlDbType.VarChar, br.MailCC));
                            cmd.Parameters.Add(AddTypedParam("@MailBCC", MySqlDbType.VarChar, br.MailBCC));
                            cmd.Parameters.Add(AddTypedParam("@MailSubject", MySqlDbType.VarChar, br.MailSubject));
                            cmd.Parameters.Add(AddTypedParam("@MailContent", MySqlDbType.VarChar, br.MailContent));

                            cmd.Parameters.Add(AddTypedParam("@Mobile_Menu_Attendance", MySqlDbType.VarChar, br.Mobile_Menu_Attendance));
                            cmd.Parameters.Add(AddTypedParam("@Mobile_Menu_AttendanceHistry", MySqlDbType.VarChar, br.Mobile_Menu_AttendanceHistry));
                            cmd.Parameters.Add(AddTypedParam("@Mobile_Menu_NewJob", MySqlDbType.VarChar, br.Mobile_Menu_NewJob));
                            cmd.Parameters.Add(AddTypedParam("@Mobile_Menu_GetMyJobList", MySqlDbType.VarChar, br.Mobile_Menu_GetMyJobList));
                            cmd.Parameters.Add(AddTypedParam("@Mobile_Menu_CompletedJob", MySqlDbType.VarChar, br.Mobile_Menu_CompletedJob));
                            cmd.Parameters.Add(AddTypedParam("@Mobile_Menu_MoreDetails", MySqlDbType.VarChar, br.Mobile_Menu_MoreDetails));
                            cmd.Parameters.Add(AddTypedParam("@Mobile_Report_Send_to_Mail", MySqlDbType.VarChar, br.Mobile_Report_Send_to_Mail));
                            cmd.Parameters.Add(AddTypedParam("@Mobile_Report_download", MySqlDbType.VarChar, br.Mobile_Report_download));
                            cmd.Parameters.Add(AddTypedParam("@EmailFlag", MySqlDbType.VarChar, br.EmailFlag));
                          


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
