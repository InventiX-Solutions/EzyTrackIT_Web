using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Artifacts;
using CRM.Data.Masters;
using CRM.Artifacts.Masters;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace CRM.Bussiness.Masters
{
    public class BLLCompany
    {
        public DataTable GetCompanyList(string dbname)
        {
            DALCompany myDALCompany = new DALCompany();
            return myDALCompany.GetCompanyList(dbname);
        }
        public int DeleteCompany(int CompanyID, int modifiedBy, string dbname)
        {
            DALCompany myDALCompany = new DALCompany();
            return myDALCompany.DeleteCompany(CompanyID, modifiedBy,dbname);
        }
        public BECompany GetCompany(int CompanyID, string dbname)
        {
            BECompany beCompany = new BECompany();

            MySqlDataReader mySqlDataReader;



            DALCompany myDALCompany = new DALCompany();
            mySqlDataReader = myDALCompany.GetCompany(CompanyID, dbname);

            while (mySqlDataReader.Read())
            {

                beCompany.CompanyID = int.Parse(mySqlDataReader["CompanyID"].ToString());
                beCompany.CompanyCode = mySqlDataReader["CompanyCode"].ToString();
                beCompany.CompanyName = mySqlDataReader["CompanyName"].ToString();
                beCompany.AddressLine1 = mySqlDataReader["AddressLine1"].ToString();
                beCompany.AddressLine2 = mySqlDataReader["AddressLine2"].ToString();
                beCompany.City = mySqlDataReader["City"].ToString();
                beCompany.State = mySqlDataReader["State"].ToString();
                beCompany.Country = mySqlDataReader["Country"].ToString();
                beCompany.PostalCode = Convert.ToInt32(mySqlDataReader["PostalCode"]);
                beCompany.PhoneNo = mySqlDataReader["PhoneNumber"].ToString();
                beCompany.FaxNo = mySqlDataReader["FaxNumber"].ToString();
                beCompany.Website = mySqlDataReader["Website"].ToString();
                beCompany.EmailID = mySqlDataReader["EmailID"].ToString();
                beCompany.TINNO = mySqlDataReader["TinNo"].ToString();
                beCompany.companylogo = mySqlDataReader["Logo"].ToString();
                beCompany.companylogo2 = mySqlDataReader["Logo2"].ToString();
                beCompany.Remarks = mySqlDataReader["Remarks"].ToString();
                beCompany.GMT = mySqlDataReader["GMT"].ToString();

                beCompany.SMTPPort = mySqlDataReader["SMTPPort"].ToString();
                beCompany.SMTPHost = mySqlDataReader["SMTPHost"].ToString();
                beCompany.SMTPUserName = mySqlDataReader["SMTPUserName"].ToString();

                beCompany.SMTPPassword = mySqlDataReader["SMTPPassword"].ToString();

                beCompany.MailCC = mySqlDataReader["MailCC"].ToString();
                beCompany.MailBCC = mySqlDataReader["MailBCC"].ToString();
                beCompany.MailSubject = mySqlDataReader["MailSubject"].ToString();
                beCompany.MailContent = mySqlDataReader["MailContent"].ToString();

                beCompany.Mobile_Menu_Attendance = int.Parse(mySqlDataReader["Mobile_Menu_Attendance"].ToString());
                beCompany.Mobile_Menu_AttendanceHistry = int.Parse(mySqlDataReader["Mobile_Menu_AttendanceHistry"].ToString());
                beCompany.Mobile_Menu_NewJob = int.Parse(mySqlDataReader["Mobile_Menu_NewJob"].ToString());
                beCompany.Mobile_Menu_GetMyJobList = int.Parse(mySqlDataReader["Mobile_Menu_GetMyJobList"].ToString());
                beCompany.Mobile_Menu_CompletedJob = int.Parse(mySqlDataReader["Mobile_Menu_CompletedJob"].ToString());
                beCompany.Mobile_Menu_MoreDetails = int.Parse(mySqlDataReader["Mobile_Menu_MoreDetails"].ToString());
                beCompany.Mobile_Report_Send_to_Mail = int.Parse(mySqlDataReader["Mobile_Report_Send_to_Mail"].ToString());
                beCompany.Mobile_Report_download = int.Parse(mySqlDataReader["Mobile_Report_download"].ToString());
                beCompany.EmailFlag = int.Parse(mySqlDataReader["EmailFlag"].ToString());



            }
            mySqlDataReader.Close();
            return beCompany;
        }
        public int GetDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id, string dbname)
        {
            DALCompany myDALCompany = new DALCompany();
            return myDALCompany.CheckDuplicateExists(companyID, tableName, columnName, codeORName, idcolumnname, id,dbname);
        }
        public int InsertOrUpdateRecord(BECompany br, string dbname)
        {
            DALCompany myDALCompany = new DALCompany();
            return myDALCompany.InsertOrUpdateRecord(br,dbname);
        }
    }
}
