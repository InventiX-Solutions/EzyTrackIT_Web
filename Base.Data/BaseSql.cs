#region Imports
using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using MySql.Data.MySqlClient;

#endregion

namespace Base.Data
{
    #region Class - BaseSql
    /// <summary>    
    ///   Author Name		:	T.Natarajan
    ///   Creation Date	    :	30 Oct 2013    
    ///   Purpose			:	ConfigurationController
    ///                         BaseSql is the class from which all classes in the Data Services
    ///                         Tier inherit. The core functionality of establishing a connection
    ///                         with the database and executing simple stored procedures is also
    ///                         provided by this base class.
    ///   Revision History  :	Nil
    /// </summary>
    public abstract class BaseSql
    {
        #region Property - Private
        //protected SqlConnection m_SqlConnection;
        protected MySqlConnection m_SqlConnection;
        private string m_SqlConnectionString;
        #endregion

        #region Property - Protected
        /// <summary>
        /// Protected property that exposes the connection string
        /// to inheriting classes. Read-Only.
        /// </summary>
        protected string SqlConnectionString
        {
            get { return this.m_SqlConnectionString; }
        }
        #endregion

        #region Method - Constructor
        /// <summary>
        /// A parameterized constructor, it allows us to take a connection
        /// string as a constructor argument, automatically instantiating
        /// a new connection.
        /// </summary>
        /// <param name="newConnectionString">Connection String to the associated database</param>
        public BaseSql()
        {
            //string server = ConfigurationManager.AppSettings["ServerName"].ToString();
            //string pass= ConfigurationManager.AppSettings["dbpassword"].ToString();
            //this.m_SqlConnectionString = "server="+ server + ";user id=root;password="+ pass + ";persistsecurityinfo=True;database=trackit_clients";
            this.m_SqlConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PeritusConnectionString"].ConnectionString;
            //this.m_SqlConnection = new SqlConnection(this.m_SqlConnectionString);
            

            this.m_SqlConnection = new MySqlConnection(this.m_SqlConnectionString);
        }
        #endregion


        //Sql string
        #region Method - Private - BuildSQLStringQueryCommand
        //private SqlCommand BuildSQLStringQueryCommand(string strSql)
        //{
        //    SqlCommand mySqlCommand = new SqlCommand(strSql, this.m_SqlConnection);
        //    mySqlCommand.CommandType = CommandType.Text;
        //    mySqlCommand.CommandTimeout = 1000;
        //    return mySqlCommand;
        //}
        private MySqlConnection CreateConnection(string connStr)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            return conn;
        }
        private string GetConnectionString(string dbname)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["PeritusConnectionString"].ConnectionString);
            builder.Database = dbname;
            return builder.ToString();
        }

        private MySqlCommand BuildSQLStringQueryCommand(string strSql,string dbname)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(strSql, CreateConnection(GetConnectionString(dbname)));
            mySqlCommand.CommandType = CommandType.Text;
            mySqlCommand.CommandTimeout = 1000;
            return mySqlCommand;
        }

        #endregion

        #region Method - Protected - RunSQLString
        protected int RunSQLString(string strSql,string dbname ,out int rowsAffected)
        {
            int result;
            if (this.m_SqlConnection.State == ConnectionState.Open)
                this.m_SqlConnection.Close();

            this.m_SqlConnection.Open();

            MySqlCommand mySqlCommand = BuildSQLStringQueryCommand(strSql,dbname);
            rowsAffected = mySqlCommand.ExecuteNonQuery();
            result = rowsAffected;
            this.m_SqlConnection.Close();
            return result;
        }
        #endregion

        #region Method - Protected - RunSQLString
        protected DataSet RunSQLString(string strSql, string dbname,string tableName)
        {
            DataSet dataSet = new DataSet();
            if (this.m_SqlConnection.State != ConnectionState.Open)
                this.m_SqlConnection.Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            mySqlDataAdapter.SelectCommand = BuildSQLStringQueryCommand(strSql,dbname);
            mySqlDataAdapter.Fill(dataSet, tableName);
            this.m_SqlConnection.Close();
            return dataSet;
        }
        #endregion

        #region Method - Protected - RunSQLString
        protected DataSet RunSQLString(string strSql,string dbname)
        {
            DataSet dataSet = new DataSet();
            if (this.m_SqlConnection.State != ConnectionState.Open)
                this.m_SqlConnection.Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            mySqlDataAdapter.SelectCommand = BuildSQLStringQueryCommand(strSql,dbname);
            mySqlDataAdapter.Fill(dataSet);
            this.m_SqlConnection.Close();
            return dataSet;
        }
        #endregion

        #region Method - Protected - RunExecuteScalarSQLString
        protected string RunExecuteScalarSQLString(string strSql,string dbname)
        {
            string result;
            if (this.m_SqlConnection.State == ConnectionState.Open)
                this.m_SqlConnection.Close();

            this.m_SqlConnection.Open();

            MySqlCommand mySqlCommand = BuildSQLStringQueryCommand(strSql,dbname);
            result = (string)mySqlCommand.ExecuteScalar();
            this.m_SqlConnection.Close();
            return result;
        }

        protected int RunExecuteScalarSQLInt(string strSql, string dbname)
        {
            int result = 0;
            if (this.m_SqlConnection.State == ConnectionState.Open)
                this.m_SqlConnection.Close();

            this.m_SqlConnection.Open();

            MySqlCommand mySqlCommand = BuildSQLStringQueryCommand(strSql, dbname);
            result = Convert.ToInt16(mySqlCommand.ExecuteScalar());
            this.m_SqlConnection.Close();
            return result;
        }

        protected int RunExecuteScalarSQLIntWithParams(string strSql, string dbname, IDataParameter[] parameters)
        {
            int result = 0;
            if (this.m_SqlConnection.State == ConnectionState.Open)
                this.m_SqlConnection.Close();

            this.m_SqlConnection.Open();

            MySqlCommand mySqlCommand = BuildSQLWithParamQueryCommand(strSql, dbname, parameters);
            result = Convert.ToInt32(mySqlCommand.ExecuteScalar());
            this.m_SqlConnection.Close();
            return result;
        }

        protected string RunExecuteScalarSQLString(string strSql, string dbname, IDataParameter[] parameters)
        {
            string result;
            if (this.m_SqlConnection.State == ConnectionState.Open)
                this.m_SqlConnection.Close();

            this.m_SqlConnection.Open();

            MySqlCommand mySqlCommand = BuildSQLWithParamQueryCommand(strSql,dbname, parameters);
            result = (string)mySqlCommand.ExecuteScalar();
            this.m_SqlConnection.Close();
            return result;
        }

        protected DateTime RunExecuteScalarSQLDateTime(string strSql, string dbname, IDataParameter[] parameters)
        {
            DateTime result;
            if (this.m_SqlConnection.State == ConnectionState.Open)
                this.m_SqlConnection.Close();

            this.m_SqlConnection.Open();

            MySqlCommand mySqlCommand = BuildSQLWithParamQueryCommand(strSql,dbname, parameters);
            result = (DateTime)mySqlCommand.ExecuteScalar();
            this.m_SqlConnection.Close();
            return result;
        }

        protected decimal RunExecuteScalarSQLDecimal(string strSql, string dbname, IDataParameter[] parameters)
        {
            decimal result = 0;
            if (this.m_SqlConnection.State == ConnectionState.Open)
                this.m_SqlConnection.Close();

            this.m_SqlConnection.Open();

            MySqlCommand mySqlCommand = BuildSQLWithParamQueryCommand(strSql,dbname, parameters);
            result = (decimal)mySqlCommand.ExecuteScalar();
            this.m_SqlConnection.Close();
            return result;
        }

        protected decimal RunExecuteScalarSQLDecimalNull(string strSql, string dbname, IDataParameter[] parameters)
        {
            decimal result = 0;
            if (this.m_SqlConnection.State == ConnectionState.Open)
                this.m_SqlConnection.Close();

            this.m_SqlConnection.Open();

            MySqlCommand mySqlCommand = BuildSQLWithParamQueryCommand(strSql,dbname, parameters);
            object resultObj = mySqlCommand.ExecuteScalar();
            if (resultObj != null)
                result = (decimal)mySqlCommand.ExecuteScalar();
            this.m_SqlConnection.Close();
            return result;
        }
        #endregion

        //end sqlstring

        //Sql string with param
        #region Method - Protected - BuildSQLWithParamIntCommand
        private MySqlCommand BuildSQLWithParamIntCommand(string strSQL,string dbname, IDataParameter[] parameters)
        {
            MySqlCommand mySqlCommand = BuildSQLWithParamQueryCommand(strSQL,dbname ,parameters);

            mySqlCommand.Parameters.Add(new MySqlParameter("ReturnValue",
                MySqlDbType.Int64,
                4, /* Size */
                ParameterDirection.ReturnValue,
                false, /* is nullable */
                0, /* byte precision */
                0, /* byte scale */
                string.Empty,
                DataRowVersion.Default,
                null));

            return mySqlCommand;
        }
        #endregion

        #region Method - Protected - BuildSQLWithParamQueryCommand
        private MySqlCommand BuildSQLWithParamQueryCommand(string strSQL,string dbname, IDataParameter[] parameters)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(strSQL, CreateConnection(GetConnectionString(dbname)));
            mySqlCommand.CommandType = CommandType.Text;
            mySqlCommand.CommandTimeout = 0;
            foreach (MySqlParameter myMySqlParameter in parameters)
            {
                mySqlCommand.Parameters.Add(myMySqlParameter);
            }

            return mySqlCommand;
        }
        #endregion

        #region Method - Protected - RunSQLWithParam
        protected int RunSQLWithParam(string strSQL, string dbname, IDataParameter[] parameters, out int rowsAffected)
        {
            int result;
            result = 0;
            if (this.m_SqlConnection.State != ConnectionState.Open)
                this.m_SqlConnection.Open();
            MySqlCommand mySqlCommand = BuildSQLWithParamIntCommand(strSQL,dbname, parameters);
            rowsAffected = mySqlCommand.ExecuteNonQuery();
            //result = (int)mySqlCommand.Parameters["ReturnValue"].Value;
            this.m_SqlConnection.Close();
            return rowsAffected;
        }
        #endregion

        #region Method - Protected - RunSQLWithParam
        protected DataSet RunSQLWithParam(string strSQL, string dbname, IDataParameter[] parameters, string tableName)
        {
            DataSet dataSet = new DataSet();
            if (this.m_SqlConnection.State != ConnectionState.Open)
                this.m_SqlConnection.Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            mySqlDataAdapter.SelectCommand = BuildSQLWithParamQueryCommand(strSQL,dbname, parameters);
            mySqlDataAdapter.Fill(dataSet, tableName);
            this.m_SqlConnection.Close();
            return dataSet;
        }
        #endregion
        //end Sql string with param


        //storedProcName
        #region Method - Protected - BuildIntCommand
        private MySqlCommand BuildIntCommand(string storedProcName, string dbname, IDataParameter[] parameters)
        {
            MySqlCommand mySqlCommand = BuildQueryCommand(storedProcName,dbname ,parameters);

            mySqlCommand.Parameters.Add(new MySqlParameter("ReturnValue",
                MySqlDbType.Int64,
                4, /* Size */
                ParameterDirection.ReturnValue,
                false, /* is nullable */
                0, /* byte precision */
                0, /* byte scale */
                string.Empty,
                DataRowVersion.Default,
                null));

            return mySqlCommand;
        }
        #endregion

        #region Method - Protected - BuildQueryCommand
        private MySqlCommand BuildQueryCommand(string storedProcName,string dbname, IDataParameter[] parameters)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(storedProcName, CreateConnection(GetConnectionString(dbname)));
            mySqlCommand.CommandType = CommandType.StoredProcedure;
            mySqlCommand.CommandTimeout = 0;
            foreach (MySqlParameter mySqlParameter in parameters)
            {
                mySqlCommand.Parameters.Add(mySqlParameter);
            }

            return mySqlCommand;
        }
        #endregion

        #region Method - Protected - RunStoredProcedure
        protected int RunStoredProcedure(string storedProcName, string dbname, IDataParameter[] parameters, out int rowsAffected)
        {
            int result;
            if (this.m_SqlConnection.State != ConnectionState.Open)
                this.m_SqlConnection.Open();
            MySqlCommand mySqlCommand = BuildIntCommand(storedProcName,dbname, parameters);
            rowsAffected = mySqlCommand.ExecuteNonQuery();
            result = (int)mySqlCommand.Parameters["ReturnValue"].Value;
            this.m_SqlConnection.Close();
            return result;
        }
        #endregion

        #region Method - Protected - RunStoredProcedure
        protected DataSet RunStoredProcedure(string storedProcName, string dbname, IDataParameter[] parameters, string tableName)
        {
            DataSet dataSet = new DataSet();
            if (this.m_SqlConnection.State == ConnectionState.Open)
                this.m_SqlConnection.Close();
            this.m_SqlConnection.Open();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            mySqlDataAdapter.SelectCommand = BuildQueryCommand(storedProcName,dbname, parameters);
            mySqlDataAdapter.Fill(dataSet, tableName);
            this.m_SqlConnection.Close();
            return dataSet;
        }
        #endregion

        #region Method - Protected - RunProcedure
        protected MySqlDataReader RunProcedure(string storedProcName, string dbname, IDataParameter[] parameters)
        {
            MySqlDataReader mySqlDataReader;
            if (this.m_SqlConnection.State != ConnectionState.Open)
                this.m_SqlConnection.Open();
            MySqlCommand mySqlCommand = BuildQueryCommand(storedProcName,dbname, parameters);
            mySqlDataReader = mySqlCommand.ExecuteReader();

            return mySqlDataReader;
        }

        #endregion
        //end storedProcName


        #region Method - Protected - RunProcedureWithOutParameter
        protected MySqlDataReader RunProcedureWithOutParameter(string storedProcName, string dbname)
        {
            MySqlDataReader mySqlDataReader;
            if (this.m_SqlConnection.State != ConnectionState.Open)
                this.m_SqlConnection.Open();
            MySqlCommand mySqlCommand = BuildSQLStringQueryCommand(storedProcName,dbname);
            mySqlDataReader = mySqlCommand.ExecuteReader();
            return mySqlDataReader;
        }

        #endregion

        
        #region Method - Protected - RunSqlDataReaderSQLString
        protected MySqlDataReader RunSqlDataReaderSQLString(string strSql, string dbname)
        {
            MySqlDataReader mySqlDataReader;
            if (this.m_SqlConnection.State != ConnectionState.Open)
                this.m_SqlConnection.Open();
            MySqlCommand mySqlCommand = BuildSQLStringQueryCommand(strSql,dbname);
            mySqlDataReader = mySqlCommand.ExecuteReader();
            return mySqlDataReader;
        }
        #endregion
        #region Method - RunProcedure
        protected DataTable RunQrysOnly(string qry,string dbname)
        {
            if (this.m_SqlConnection.State == ConnectionState.Open)
                this.m_SqlConnection.Close();
            this.m_SqlConnection.Open();
            MySqlCommand mySqlCommand = new MySqlCommand(qry, CreateConnection(GetConnectionString(dbname)));
            MySqlDataAdapter da = new MySqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        #endregion

        public void CloseConnectionPooling(MySqlConnection connStr)//added khader
        {
            MySqlConnection.ClearPool(connStr);
            int i = KillSleepingConnections(connStr, 30);
            //MySqlConnection.ClearAllPools();
        }

        private int KillSleepingConnections(MySqlConnection connStr, int iMinSecondsToExpire)
        {
            string strSQL = "show processlist";
            System.Collections.ArrayList m_ProcessesToKill = new System.Collections.ArrayList();
            //MySqlConnection myConn = new MySqlConnection(connS);
            MySqlCommand myCmd = new MySqlCommand(strSQL, connStr);
            MySqlDataReader MyReader = null;
            try
            {
                connStr.Open();
                // Get a list of processes to kill.
                MyReader = myCmd.ExecuteReader();
                while (MyReader.Read())
                {
                    // Find all processes sleeping with a timeout value higher than our threshold.
                    int iPID = Convert.ToInt32(MyReader["Id"].ToString());
                    string strState = MyReader["Command"].ToString();
                    int iTime = Convert.ToInt32(MyReader["Time"].ToString());
                    if (strState == "Sleep" && iTime >= iMinSecondsToExpire && iPID > 0)
                    {
                        // This connection is sitting around doing nothing. Kill it.
                        m_ProcessesToKill.Add(iPID);
                    }
                }
                MyReader.Close();
                foreach (int aPID in m_ProcessesToKill)
                {
                    strSQL = "kill " + aPID;
                    myCmd.CommandText = strSQL;
                    myCmd.ExecuteNonQuery();
                }
            }
            catch (Exception excep)
            {
            }
            finally
            {
                if (MyReader != null && !MyReader.IsClosed)
                {
                    MyReader.Close();
                }
                if (connStr != null && connStr.State == ConnectionState.Open)
                {
                    connStr.Close();
                }
            }
            return m_ProcessesToKill.Count;
        }

        protected int RunSQLStringOnly(string strSql, string dbname)
        {
            int result;
            MySqlCommand mySqlCommand = BuildSQLStringQueryCommand(strSql, dbname);
            if (this.m_SqlConnection.State == ConnectionState.Open)
            {
                this.m_SqlConnection.Close();
                CloseConnectionPooling(this.m_SqlConnection);
            }

            this.m_SqlConnection.Open();

            //MySqlCommand mySqlCommand = BuildSQLStringQueryCommand(strSql, dbname);
            result = mySqlCommand.ExecuteNonQuery();
            this.m_SqlConnection.Close();
            CloseConnectionPooling(this.m_SqlConnection);
            return result;
        }

        //private MySqlCommand BuildSQLStringQueryCommand(string strSql, string dbname)
        //{
        //    MySqlCommand mySqlCommand = new MySqlCommand(strSql, CreateConnection(GetConnectionString(dbname)));
        //    mySqlCommand.CommandType = CommandType.Text;
        //    mySqlCommand.CommandTimeout = 1000;
        //    return mySqlCommand;
        //}

        
    }

    #endregion
}
