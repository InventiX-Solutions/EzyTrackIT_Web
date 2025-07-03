using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using CRM.Data.Security;
using System.Security.Cryptography;
using CRM.Artifacts.Security;
using CRM.Data.Masters;
namespace CRM.Bussiness
{
    public class BLLUser
    {
        #region Method - Public - GetLoginInfo
        public BEUsers GetLoginInfo(BEUsers myUserInfo,string dbname)
        {
            BEUsers myUser = null;
            DataSet myDataSet;
            CRM.Data.Security.DALUser myDALUser = new CRM.Data.Security.DALUser();
            myDataSet = myDALUser.GetLoginInfo(myUserInfo, dbname);
            if (myDataSet.Tables.Count > 0)
            {
                foreach (DataRow myDataRow in myDataSet.Tables[0].Rows)
                {
                    myUser = new BEUsers();
                    myUser.CompanyID = Convert.ToInt32(myDataRow["CompanyID"].ToString());
                    myUser.UserID = Convert.ToInt32(myDataRow["UserID"].ToString());
                    myUser.UserName = myDataRow["UserName"].ToString();
                    myUser.UserPassword = myDataRow["Password"].ToString();
                    myUser.CompanyName = myDataRow["CompanyName"].ToString();
                    myUser.Photo = myDataRow["Photo"].ToString();
                    myUser.Address = myDataRow["address"].ToString();
                    myUser.companylogo = myDataRow["companylogo"].ToString();
                    myUser.companylogo2 = myDataRow["companylogo2"].ToString();
                    myUser.SMTPUserName = myDataRow["SMTPUserName"].ToString();
                    myUser.SMTPPassword = myDataRow["SMTPPassword"].ToString();
                    myUser.SMTPHost = myDataRow["SMTPHost"].ToString();
                    myUser.SMTPPort = myDataRow["SMTPPort"].ToString();
                }
            }
            myDataSet.Clear();
            return myUser;
        }
        #endregion

        public string ValidateRegisteredClient(string ClientCode)
        {
            CRM.Data.Masters.DALUser myDALUser = new CRM.Data.Masters.DALUser();
           
            string message = myDALUser.ValidateRegisteredClient(ClientCode);
            return message;
        }
    }
}
