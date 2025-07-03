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
    public class DALNotificationTemplate : Base.Data.BaseSql
    {
        //public DALNotificationTemplate(string sqlConnectionString) : base(sqlConnectionString) { }

        public int InsertOrUpdateRecord(BENotificationTemplate BE, string dbname, out string sMessage)
        {
            int iResult = 0;
            
            //if (this.m_SqlConnection.State == ConnectionState.Open)
            //    this.m_SqlConnection.Close();

            //this.m_SqlConnection.Open();

            using (MySqlConnection conn = CreateConnection(GetConnectionString(dbname)))
            {
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            //Added MailRecipientType, SMSRecipientType, WhatsappRecipientType Columns by Velu on 16-06-2021 for Dynamic email or sms or whatsapp recipient configuration a
                            if (BE.Template_ID == 0)
                            {
                                cmd.CommandText = @" INSERT INTO NotificationTemplate(Company_ID, TemplateType_ID, TemplateType, EmailFlag, SMSFlag, WhatsappFlag, MailSubject, 
                                                    MailContent, SMSSubject, SMSContent, WhatsappContent, CreatedBy, CreatedDT, Active, TemplateName, Category_ID, Category_ID_Text,
                                                    MailRecipientType, SMSRecipientType, WhatsappRecipientType,MailCC)
                                             VALUES (@Company_ID, @TemplateType_ID, @TemplateType, @EmailFlag, @SMSFlag, @WhatsappFlag, @MailSubject, 
                                                    @MailContent, @SMSSubject, @SMSContent, @WhatsappContent, @Created_By, NOW(), 1, @TemplateName, @Category_ID, @Category_ID_Text,
                                                    @MailRecipientType, @SMSRecipientType, @WhatsappRecipientType,@MailCC) ";
                            }
                            else
                            {
                                cmd.CommandText = @" UPDATE NotificationTemplate SET TemplateType_ID = @TemplateType_ID, TemplateType = @TemplateType, EmailFlag = @EmailFlag, 
                                SMSFlag = @SMSFlag, WhatsappFlag = @WhatsappFlag, MailSubject = @MailSubject, MailContent = @MailContent, SMSSubject = @SMSSubject,
                                SMSContent = @SMSContent, WhatsappContent = @WhatsappContent, ModifiedBy = @Modified_By, ModifiedDT = NOW(),
                                TemplateName = @TemplateName, Category_ID = @Category_ID, Category_ID_Text = @Category_ID_Text,
                                MailRecipientType = @MailRecipientType, SMSRecipientType = @SMSRecipientType, WhatsappRecipientType = @WhatsappRecipientType ,MailCC =@MailCC
                                WHERE Company_ID = @Company_ID And Template_ID = @Template_ID ";
                            }

                            cmd.Parameters.Add(AddTypedParam("@TemplateType_ID", MySqlDbType.Int32, BE.TemplateType_ID));
                            cmd.Parameters.Add(AddTypedParam("@TemplateType", MySqlDbType.VarChar, BE.TemplateType));
                            cmd.Parameters.Add(AddTypedParam("@Created_By", MySqlDbType.Int32, BE.CreatedBy));
                            cmd.Parameters.Add(AddTypedParam("@Modified_By", MySqlDbType.Int32, BE.ModifiedBy));
                            cmd.Parameters.Add(AddTypedParam("@Company_ID", MySqlDbType.Int32, BE.Company_ID));
                            cmd.Parameters.Add(AddTypedParam("@Template_ID", MySqlDbType.Int32, BE.Template_ID));
                            cmd.Parameters.Add(AddTypedParam("@WhatsappFlag", MySqlDbType.Int32, BE.WhatsappFlag));
                            cmd.Parameters.Add(AddTypedParam("@EmailFlag", MySqlDbType.Int32, BE.EmailFlag));
                            cmd.Parameters.Add(AddTypedParam("@MailSubject", MySqlDbType.VarChar, BE.MailSubject));
                            cmd.Parameters.Add(AddTypedParam("@MailContent", MySqlDbType.VarChar, BE.MailContent));
                            cmd.Parameters.Add(AddTypedParam("@SMSFlag", MySqlDbType.Int32, BE.SMSFlag));
                            cmd.Parameters.Add(AddTypedParam("@SMSSubject", MySqlDbType.VarChar, BE.SMSSubject));
                            cmd.Parameters.Add(AddTypedParam("@SMSContent", MySqlDbType.VarChar, BE.SMSContent));
                            cmd.Parameters.Add(AddTypedParam("@WhatsappContent", MySqlDbType.VarChar, BE.WhatsappContent));
                            cmd.Parameters.Add(AddTypedParam("@TemplateName", MySqlDbType.VarChar, BE.TemplateName));
                            cmd.Parameters.Add(AddTypedParam("@Category_ID", MySqlDbType.VarChar, BE.Category_ID));
                            cmd.Parameters.Add(AddTypedParam("@Category_ID_Text", MySqlDbType.VarChar, BE.Category_ID_Text));
                            cmd.Parameters.Add(AddTypedParam("@MailRecipientType", MySqlDbType.VarChar, BE.MailRecipientType));
                            cmd.Parameters.Add(AddTypedParam("@SMSRecipientType", MySqlDbType.VarChar, BE.SMSRecipientType));
                            cmd.Parameters.Add(AddTypedParam("@WhatsappRecipientType", MySqlDbType.VarChar, BE.WhatsappRecipientType));
                            cmd.Parameters.Add(AddTypedParam("@MailCC", MySqlDbType.VarChar, BE.MailCC));

                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        iResult = 1;
                        //this.m_SqlConnection.Close();
                        sMessage = "Success";
                    }
                    catch (Exception ex)
                    {
                        sMessage = ex.Message + ":" + ex.GetType() + ":" + ex.Source.ToString();
                        trans.Rollback();
                        iResult = 0;
                    }
                    finally
                    {
                        //this.m_SqlConnection.Close();
                        CloseConnectionPooling(conn);
                    }
                }
            }
            return iResult;
        }
        public MySqlConnection CreateConnection(string connStr)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            this.m_SqlConnection = conn;
            conn.Open();
            return conn;
        }
        private string GetConnectionString(string dbname)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["PeritusConnectionString"].ConnectionString);
            builder.Database = dbname;
            return builder.ToString();
        }
        protected MySqlParameter AddTypedParam(string paraName, MySqlDbType sQLType, object value)
        {
            MySqlParameter parm = new MySqlParameter(paraName, sQLType);
            parm.Value = value;
            return parm;
        }

    }

     
}
