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

namespace CRM.Bussiness
{
    public class BLLStatus
    {
        public DataTable GetStatusList(string dbname)
        {
            DALStatus myDALStatus = new DALStatus();
            return myDALStatus.GetStatusList(dbname);
        }
        public BEStatus GetStatus(int CompanyId, int StatusID,string dbname)
        {
            BEStatus beStatus = new BEStatus();

            MySqlDataReader mySqlDataReader;



            DALStatus myDALStatus = new DALStatus();
            mySqlDataReader = myDALStatus.GetStatus(CompanyId, StatusID, dbname);

            while (mySqlDataReader.Read())
            {

                beStatus.StatusID = int.Parse(mySqlDataReader["StatusID"].ToString());
                beStatus.StatusCode = mySqlDataReader["StatusCode"].ToString();
                beStatus.StatusName = mySqlDataReader["StatusName"].ToString();
                beStatus.SequenceNo = mySqlDataReader["sequenceNo"].ToString();
                beStatus.StatusImage = mySqlDataReader["StatusImage"].ToString();
            }
            mySqlDataReader.Close();
            return beStatus;
        }
        public int GetDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id,string dbname)
        {
            DALStatus myDALStatus = new DALStatus();
            return myDALStatus.CheckDuplicateExists(companyID, tableName, columnName, codeORName, idcolumnname, id, dbname);
        }
        public int InsertOrUpdateRecord(BEStatus br,string dbname)
        {
            DALStatus myDALStatus = new DALStatus();
            return myDALStatus.InsertOrUpdateRecord(br, dbname);
        }
        public int DeleteStatus(int StatusID, int modifiedBy,string dbname)
        {
            DALStatus myDALStatus = new DALStatus();
            return myDALStatus.DeleteStatus(StatusID, modifiedBy, dbname);
        }
    }
}
