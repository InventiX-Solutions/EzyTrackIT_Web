using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using CRM.Artifacts;
using System.Configuration;
using System.Data;

namespace CRM.Data
{
    public class DALCommon : Base.Data.BaseSql
    {
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
        public MySqlDataReader GetMap(string User, string dbname)
        {
            string Result = string.Empty;
            string strSql = @"SELECT Intime_Lat,Intime_Long FROM tcc_trackit.userpunchlog A,tcc_trackit.engineers B where A.StaffId=B.engineer_id and A.StaffId='" + User + "' ORDER BY RowId   desc LIMIT 1 ;";
            MySqlDataReader dt = RunSqlDataReaderSQLString(strSql, dbname);

            return dt;

        }
        public DataTable GetStatusData(string ReportType, string StartTime, string FinishTime, int CustomerID, int BranchID, int ProductID, int BrandID, int ModelID, int ProblemID, int EngineerID, int jobtypeID, string dbname)
        {
            MySqlParameter[] parameters = { new MySqlParameter("VReportType", MySqlDbType.VarChar, 50),
                                            new MySqlParameter("Fdate", MySqlDbType.VarChar, 50),
                                            new MySqlParameter("Tdate", MySqlDbType.VarChar, 50),
                                            new MySqlParameter("VCustomerID", MySqlDbType.Int32),
                                            new MySqlParameter("VBranchID", MySqlDbType.Int32),
                                            new MySqlParameter("VProductID", MySqlDbType.Int32),
                                            new MySqlParameter("VBrandID", MySqlDbType.Int32),
                                            new MySqlParameter("VModelID", MySqlDbType.Int32),
                                            new MySqlParameter("VProblemID", MySqlDbType.Int32),
                                            new MySqlParameter("VAssigned_to", MySqlDbType.Int32),
                                            new MySqlParameter("VjobtypeID", MySqlDbType.Int32)
                                          };
            parameters[0].Value = ReportType;
            parameters[1].Value = StartTime;
            parameters[2].Value = FinishTime;
            parameters[3].Value = CustomerID;
            parameters[4].Value = BranchID;
            parameters[5].Value = ProductID;
            parameters[6].Value = BrandID;
            parameters[7].Value = ModelID;
            parameters[8].Value = ProblemID;
            parameters[9].Value = EngineerID;
            parameters[10].Value = jobtypeID;
            DataSet dsResult = RunStoredProcedure("usp_getstatusdata", dbname, parameters, "MyDataTable");//changes made in sp 
            return dsResult.Tables[0];
        }

        public DataTable GetExpectedStatusValuesAsDataTable(string dbname)//added for dynamic in dashboard
        {
            DataTable dtResult = new DataTable();
            string sqlStr = string.Empty;
            // sqlStr += "select StatusName as ColumnValue,SequenceNo from Status where active='1' order by sequenceNo Asc";
            sqlStr += "select StatusName as ColumnValue from Status where active='1' order by sequenceNo Asc";
            DataTable dsResult = RunQrysOnly(sqlStr, dbname);
            return dsResult;
        }
        public DataTable GetAttendanceList(string StartTime, string FinishTime, string userid, string dbname)
        {
            string sqlStr = string.Empty;
            string output = string.Empty;
            //if(userid!="")
            //{
            //    //output = string.Format("'{0}'", userid);
            //    output = userid;
            //    sqlStr = "SELECT A.RowId,date_format(str_to_date(A.PunchDate, '%Y-%m-%d'),'%d-%m-%Y') PunchDate,TIME_FORMAT(A.Intime, '%H:%i:%s') Intime,A.Intime_Lat,A.Intime_Long,A.Intime_img,A.Address,B.engineer_name,case when A.CheckInStatus='1' then 'CheckIn' else 'CheckOut' end as CheckInStatus FROM userpunchlog A ,engineers B where B.engineer_id=A.StaffId and A.PunchDate  BETWEEN  @StartDate AND @EndDate and A.StaffId in ('" + output + "') order by A.PunchDate,A.Intime desc;";
            //}
            //else{
            //    output = userid;
            //    sqlStr = "SELECT A.RowId,date_format(str_to_date(A.PunchDate, '%Y-%m-%d'),'%d-%m-%Y') PunchDate,TIME_FORMAT(A.Intime, '%H:%i:%s') Intime,A.Intime_Lat,A.Intime_Long,A.Intime_img,A.Address,B.engineer_name,case when A.CheckInStatus='1' then 'CheckIn' else 'CheckOut' end as CheckInStatus FROM userpunchlog A ,engineers B where B.engineer_id=A.StaffId and A.PunchDate  BETWEEN  @StartDate AND @EndDate order by A.PunchDate,A.Intime desc;";
            //}

            if (userid != "")
            {

                sqlStr = "select B.engineer_code,B.engineer_name,B.SkillName,date_format(str_to_date(B.PunchDate, '%Y-%m-%d'),'%d-%m-%Y') PunchDate,"
                         + " case when ifnull(C.CheckInStatus,0)=2 then 'Checked OUT' when ifnull(C.CheckInStatus,0)=0 and"
                         + " ifnull(A.CheckInStatus,0)=1 then 'Checked IN' when ifnull(A.CheckInStatus,0)=0 then 'Not Checked' end as EMPStatus,"
                         // + " TIMEDIFF(TIME_FORMAT(C.Intime, '%H:%i:%s'),TIME_FORMAT(A.Intime, '%H:%i:%s')) as TimeSpent,TIME_FORMAT(A.Intime, '%H:%i:%s') Intime,"
                         + " TIMEDIFF(TIME_FORMAT(max(C.Intime), '%H:%i'),TIME_FORMAT(min(A.Intime), '%H:%i')) as TimeSpent,TIME_FORMAT(min(A.Intime), '%h:%i %p') Intime,"
                         + " A.Address as INLocation,A.Intime_img as  IN_Img ,TIME_FORMAT(max(C.Intime), '%h:%i %p') outtime,C.Address as OutLocation,"
                         + " C.Intime_img as  OUT_Img from (select B.engineer_code,B.engineer_name,s.SkillName,B.SkillID,B.engineer_id,B.PunchDate"
                         + " from (select A.engineer_code,A.engineer_name,A.SkillID,A.engineer_id,B.PunchDate From (select *From engineers where Active=1 and engineer_id in (" + userid + ")) A,"
                         + " (select  distinct PunchDate From userpunchlog where PunchDate between @StartDate AND @EndDate) as B) B, engineerskill s"
                         + " where B.SkillID=s.SkillID and s.Active=1 )"
                         + " B left join userpunchlog A on B.engineer_id=A.StaffId  and B.PunchDate=A.PunchDate and A.CheckInStatus=1 left join userpunchlog C on B.engineer_id=C.StaffId  and B.PunchDate=C.PunchDate and C.CheckInStatus !=1"

                         + " group by B.engineer_code,B.engineer_name,B.SkillName,B.PunchDate";

            }
            else
            {
                output = userid;
                sqlStr = "select B.engineer_code,B.engineer_name,B.SkillName,date_format(str_to_date(B.PunchDate, '%Y-%m-%d'),'%d-%m-%Y') PunchDate,"
                         + " case when ifnull(C.CheckInStatus,0)=2 then 'Checked OUT' when ifnull(C.CheckInStatus,0)=0 and"
                         + " ifnull(A.CheckInStatus,0)=1 then 'Checked IN' when ifnull(A.CheckInStatus,0)=0 then 'Not Checked' end as EMPStatus,"
                         //    +" TIMEDIFF(TIME_FORMAT(C.Intime, '%H:%i:%s'),TIME_FORMAT(A.Intime, '%H:%i:%s')) as TimeSpent,TIME_FORMAT(A.Intime, '%H:%i:%s') Intime,"
                         + " TIME_FORMAT(TIMEDIFF(TIME_FORMAT(max(C.Intime), '%H:%i:%s'),TIME_FORMAT(min(A.Intime), '%H:%i:%s')), '%H:%i') as TimeSpent,TIME_FORMAT(min(A.Intime), '%h:%i %p') Intime,"
                         + " A.Address as INLocation,A.Intime_img as  IN_Img ,TIME_FORMAT(max(C.Intime), '%h:%i %p') outtime,C.Address as OutLocation,"
                         + " C.Intime_img as  OUT_Img from (select B.engineer_code,B.engineer_name,s.SkillName,B.SkillID,B.engineer_id,B.PunchDate"
                         + " from (select A.engineer_code,A.engineer_name,A.SkillID,A.engineer_id,B.PunchDate From (select *From engineers where Active=1) A,"
                         + " (select  distinct PunchDate From userpunchlog where PunchDate between @StartDate AND @EndDate) as B) B, engineerskill s"
                         + " where B.SkillID=s.SkillID and s.Active=1 )"
                         + " B left join userpunchlog A on B.engineer_id=A.StaffId  and B.PunchDate=A.PunchDate and A.CheckInStatus=1 left join userpunchlog C on B.engineer_id=C.StaffId  and B.PunchDate=C.PunchDate and C.CheckInStatus !=1"
                         + " group by B.engineer_code,B.engineer_name,B.SkillName,B.PunchDate";
            }

            MySqlParameter[] parameters = { new MySqlParameter("StartDate", MySqlDbType.VarChar, 50),
                                          new MySqlParameter("EndDate", MySqlDbType.VarChar, 50),
                                          new MySqlParameter("UserId", MySqlDbType.VarChar, 1000)};
            parameters[0].Value = StartTime.Trim();
            parameters[1].Value = FinishTime.Trim();
            parameters[2].Value = output.Trim();
            //DataSet dsResult = RunSQLWithParam(sqlStr, dbname, parameters, "MyDataTable");
            //return dsResult.Tables[0];


            DataSet dsResult = RunSQLWithParam(sqlStr, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public DataTable GetClosedJobMapList(string StartTime, string FinishTime, string userid, string dbname)
        {
            string sqlStr = string.Empty;
            string output = string.Empty;
            if (userid != "")
            {
                //output = string.Format("'{0}'", userid);
                output = userid;
                //sqlStr = @"select  TicketID,Intime_lat,Intime_Long,Address from tickets where   Intime_lat<>'' and Intime_Long<>'' and date_format(str_to_date((REPLACE((LEFT(TicketDt,10)), '-', '/')), '%d/%m/%Y'), '%d/%c/%Y') BETWEEN @StartDate AND @EndDate and Assigned_to in (@UserId)  order by ModifiedDT desc ;";



                //ela change 02/03/2020 display all status list in tracking

                // sqlStr = @"select B.engineer_name,c.customer_name,A.TicketID,A.TicketNo, A.Intime_lat,A.Intime_Long,A.Address,date_format(str_to_date((REPLACE((LEFT(A.TicketDt,10)), '-', '/')), '%d/%m/%Y'), '%d/%c/%Y') as JobDate,date_format(str_to_date(A.ModifiedDT, '%Y-%m-%d'),'%d/%m/%Y') completionsDate from tickets A ,engineers B,customerbranch c,status d where  A.StatusID=d.StatusID and d.StatusCode='8_Closed' and CustomerID=customer_branch_id and engineer_id=Assigned_to and  Intime_lat<>'' and Intime_Long<>'' and date_format(str_to_date((REPLACE((LEFT(TicketDt,10)), '-', '/')), '%d/%m/%Y'), '%d/%c/%Y')  BETWEEN @StartDate AND @EndDate and A.Assigned_to in (@UserId)  order by A.ModifiedDT desc ;";
                //   sqlStr = @"select B.engineer_name,c.customer_name,A.TicketID,A.TicketNo, A.Intime_lat,A.Intime_Long,A.Address,date_format(str_to_date((REPLACE((LEFT(A.TicketDt,10)), '-', '/')), '%d/%m/%Y'), '%d/%c/%Y') as JobDate,date_format(str_to_date(A.ModifiedDT, '%Y-%m-%d'),'%d/%m/%Y') completionsDate,d.StatusCode as Status from tickets A ,engineers B,customerbranch c,status d where  A.StatusID=d.StatusID  and CustomerID=customer_branch_id and engineer_id=Assigned_to and  Intime_lat<>'' and Intime_Long<>'' and STR_TO_DATE(date_format(str_to_date(A.ModifiedDT, '%Y-%m-%d'),'%d/%m/%Y'),'%d/%m/%Y')  BETWEEN  STR_TO_DATE(@StartDate, '%d/%m/%Y') AND STR_TO_DATE(@EndDate, '%d/%m/%Y') and A.Assigned_to in (@UserId)  order by A.ModifiedDT desc ;";

                //ela change 24/12/2020 date formate change tracking
                sqlStr = @"select B.engineer_name,e.customer_Name,c.customer_branch_Name,A.TicketID,A.TicketNo, A.Intime_lat,A.Intime_Long,A.Address,DATE_FORMAT(A.TicketDt,'%d/%m/%Y') as JobDate,DATE_FORMAT(A.ModifiedDT,'%d/%m/%Y') completionsDate,d.StatusCode as Status from tickets A ,engineers B,customerbranch c,status d,customers e where e.customer_ID=c.customer_id and A.CustomerID=e.customer_ID and A.StatusID=d.StatusID  and A.BranchID=customer_branch_id and engineer_id=Assigned_to and  Intime_lat<>'' and Intime_Long<>'' and STR_TO_DATE(date_format(str_to_date(A.ModifiedDT, '%Y-%m-%d'),'%d/%m/%Y'),'%d/%m/%Y')  BETWEEN  STR_TO_DATE(@StartDate, '%d/%m/%Y') AND STR_TO_DATE(@EndDate, '%d/%m/%Y') and A.Assigned_to in (@UserId)  order by A.ModifiedDT desc ;";


            }
            else
            {
                output = userid;
                //sqlStr = @"select  TicketID,Intime_lat,Intime_Long,Address from tickets where   Intime_lat<>'' and Intime_Long<>'' and date_format(str_to_date((REPLACE((LEFT(TicketDt,10)), '-', '/')), '%d/%m/%Y'), '%d/%c/%Y') BETWEEN @StartDate AND @EndDate  order by ModifiedDT desc ;";



                //ela change 02/03/2020 display all status list in tracking

                // sqlStr = @"select B.engineer_name,c.customer_name,A.TicketID,A.TicketNo, A.Intime_lat,A.Intime_Long,A.Address,date_format(str_to_date((REPLACE((LEFT(A.TicketDt,10)), '-', '/')), '%d/%m/%Y'), '%d/%c/%Y') as JobDate,date_format(str_to_date(A.ModifiedDT, '%Y-%m-%d'),'%d/%m/%Y') completionsDate from tickets A ,engineers B,customerbranch c,status d where  A.StatusID=d.StatusID and d.StatusCode='8_Closed' and CustomerID=customer_branch_id and engineer_id=Assigned_to and  Intime_lat<>'' and Intime_Long<>'' and date_format(str_to_date((REPLACE((LEFT(TicketDt,10)), '-', '/')), '%d/%m/%Y'), '%d/%c/%Y')  BETWEEN @StartDate AND @EndDate  order by A.ModifiedDT desc ;";
                //  sqlStr = @"select B.engineer_name,c.customer_name,A.TicketID,A.TicketNo, A.Intime_lat,A.Intime_Long,A.Address,date_format(str_to_date((REPLACE((LEFT(A.TicketDt,10)), '-', '/')), '%d/%m/%Y'), '%d/%c/%Y') as JobDate,date_format(str_to_date(A.ModifiedDT, '%Y-%m-%d'),'%d/%m/%Y') completionsDate,d.StatusCode as Status from tickets A ,engineers B,customerbranch c,status d where  A.StatusID=d.StatusID  and CustomerID=customer_branch_id and engineer_id=Assigned_to and  Intime_lat<>'' and Intime_Long<>'' and STR_TO_DATE(date_format(str_to_date(A.ModifiedDT, '%Y-%m-%d'),'%d/%m/%Y'),'%d/%m/%Y')  BETWEEN  STR_TO_DATE(@StartDate, '%d/%m/%Y') AND STR_TO_DATE(@EndDate, '%d/%m/%Y') order by A.ModifiedDT desc ;";

                //ela change 24/12/2020 date formate change tracking
                sqlStr = @"select B.engineer_name,e.customer_Name,c.customer_branch_Name,A.TicketID,A.TicketNo, A.Intime_lat,A.Intime_Long,A.Address,DATE_FORMAT(A.TicketDt,'%d/%m/%Y') as JobDate,DATE_FORMAT(A.ModifiedDT,'%d/%m/%Y') completionsDate,d.StatusCode as Status from tickets A ,engineers B,customerbranch c,status d,customers e where e.customer_ID=c.customer_id and A.CustomerID=e.customer_ID and A.StatusID=d.StatusID  and A.BranchID=customer_branch_id and engineer_id=Assigned_to and  Intime_lat<>'' and Intime_Long<>'' and STR_TO_DATE(date_format(str_to_date(A.ModifiedDT, '%Y-%m-%d'),'%d/%m/%Y'),'%d/%m/%Y')  BETWEEN  STR_TO_DATE(@StartDate, '%d/%m/%Y') AND STR_TO_DATE(@EndDate, '%d/%m/%Y') order by A.ModifiedDT desc ;";

            }

            MySqlParameter[] parameters = { new MySqlParameter("@StartDate", MySqlDbType.VarChar, 50),
                                          new MySqlParameter("@EndDate", MySqlDbType.VarChar, 50),
                                          new MySqlParameter("@UserId", MySqlDbType.VarChar, 1000)};
            parameters[0].Value = StartTime.Replace("-", "/").ToString().Trim();
            parameters[1].Value = FinishTime.Replace("-", "/").ToString().Trim();
            parameters[2].Value = output.Trim();
            //DataSet dsResult = RunSQLWithParam(sqlStr, dbname, parameters, "MyDataTable");
            //return dsResult.Tables[0];


            DataSet dsResult = RunSQLWithParam(sqlStr, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public DataTable LoadDropDownListActive(int companyID, string selectedTextName, string selectedValueName, string tableName, string filterCondition, string dbname)
        {
            DataTable dtResult = new DataTable();
            string sqlStr = string.Empty;
            //sqlStr = "Select '' Name, '' Value UNION ALL ";
            sqlStr += " Select " + selectedTextName + " Name, " + selectedValueName + " Value" + " From " + tableName + " Where CompanyID = @CompanyID And ACTIVE = 1 ";
            if (!string.IsNullOrEmpty(filterCondition))
            {
                sqlStr += " And " + filterCondition;
            }
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
            //set values
            parameters[0].Value = companyID;

            DataSet dsResult = RunSQLWithParam(sqlStr, dbname, parameters, "MyDataTable");

            return dsResult.Tables[0];
        }

        public DataTable GetEngineerDetails(string dbname)
        {
            DataTable dtResult = new DataTable();
            string sqlStr = string.Empty;
            sqlStr += "select engineer_id as value,engineer_name as Name from engineers where active='1'";
            DataTable dsResult = RunQrysOnly(sqlStr, dbname);
            return dsResult;
        }
        public int UpdateLog(BECommom BeCommon, string dbname)
        {
            int dsResult = 0;
            string sqlStr = string.Empty;
            using (MySqlConnection conn = CreateConnection(GetConnectionString(dbname)))
            {
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {

                            cmd.Transaction = trans;

                            cmd.CommandText = @"INSERT INTO ErrorLog (FormName,ErrorDescription,ErrorType,CreatedDate)
                                                select @FormName, @ErrorDescription,@ErrorType, @CreatedDate ";

                            cmd.Parameters.Add(AddTypedParam("@FormName", MySqlDbType.VarChar, BeCommon.FormName));
                            cmd.Parameters.Add(AddTypedParam("@ErrorDescription", MySqlDbType.VarChar, BeCommon.ErrorDescription));
                            cmd.Parameters.Add(AddTypedParam("@ErrorType", MySqlDbType.VarChar, BeCommon.ErrorType));
                            cmd.Parameters.Add(AddTypedParam("@CreatedDate", MySqlDbType.Datetime, BeCommon.CreatedDate));

                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                        }

                        trans.Commit();
                        dsResult = 1;

                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        dsResult = 0;
                    }
                }
            }
            return dsResult;
        }

        public DataTable GetCustomerTimeAnalysisList(string StartTime, string EndTime, string engineerid, string customerid, string dbname)
        {
            string sqlStr = string.Empty;
            string sCondition = string.Empty;

            if (!string.IsNullOrEmpty(customerid))
                sCondition = " C.customer_branch_id in ('" + customerid + "') ";

            if (!string.IsNullOrEmpty(engineerid))
            {
                if (!string.IsNullOrEmpty(sCondition))
                    sCondition += " and ";

                sCondition += " E.engineer_id in ('" + engineerid + "') ";
            }

            sqlStr = @"select M.customer_code, M.customer_name, C.customer_branch_code, C.customer_branch_name, E.engineer_code, E.engineer_name, A.TicketNo, A.TicketDt as TicketDT, 
                    TIME_FORMAT(min(starttime), '%H:%i:%s') as starttime, if(min(starttime) = max(endtime), null, TIME_FORMAT(max(endtime), '%H:%i:%s')) as endtime, 
                    date_format(min(starttime),'%d-%m-%Y %h:%i:%s') as startdatetime, date_format(max(endtime),'%d-%m-%Y %h:%i:%s') as enddatetime,
                    TIMESTAMPDIFF(Second, min(starttime), max(endtime)), SEC_TO_TIME(TIMESTAMPDIFF(Second, min(starttime), max(endtime))) as TimeSpent
                    from
                    customers M join customerbranch C on C.customer_id = M.customer_id join 
                    (select tk.TicketID, tk.TicketNo, tk.TicketDT, tk.CustomerID, tk.branchid, tk.Product_ID, ts.status_id, ts.starttime, ts.endtime, 
                    ts.engineer_id, s.sequenceNo
                     from tickets tk, tickets_status ts, status s
                     where tk.TicketID = ts.tickets_id and tk.Active = 1 and ts.status_id = s.statusid and s.active = 1
                     and date(tk.TicketDT) between @StartDate and @EndDate 
                     ) A on C.customer_branch_id =  A.branchid
                    join engineers E on A.engineer_id = E.engineer_id ";

            //and STR_TO_DATE(tk.TicketDT, '%d/%m/%Y') BETWEEN STR_TO_DATE(@StartDate, '%d/%m/%Y') and STR_TO_DATE(@EndDate, '%d/%m/%Y')

            if (!string.IsNullOrEmpty(sCondition))
                sqlStr += " where " + sCondition;

            sqlStr += " group by M.customer_code, M.customer_Name, C.customer_branch_code, C.customer_branch_name, E.engineer_code, E.engineer_name, A.TicketNo, A.TicketDT ";

            MySqlParameter[] parameters = { new MySqlParameter("StartDate", MySqlDbType.VarChar, 50),
                                          new MySqlParameter("EndDate", MySqlDbType.VarChar, 50)};
            parameters[0].Value = StartTime.Trim();
            parameters[1].Value = EndTime.Trim();

            DataSet dsResult = RunSQLWithParam(sqlStr, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }

        private string GetConnectionString(string dbname)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["PeritusConnectionString"].ConnectionString);
            builder.Database = dbname;
            return builder.ToString();
        }

        public DataTable getcompileddata(DataTable dt, string dbname)
        {
            DataSet dsResult = new DataSet();

            using (MySqlConnection conn = CreateConnection(GetConnectionString(dbname)))
            {
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "truncate table temp_customerbranch; ";
                            cmd.ExecuteNonQuery();

                            foreach (DataRow objRow in dt.Rows)
                            {
                                MySqlParameter[] parameters = { new MySqlParameter("@vcustomer_branch_code", MySqlDbType.VarChar, 50),
                                          new MySqlParameter("@vcustomer_branch_Name", MySqlDbType.VarChar, 150),
                                          new MySqlParameter("@vcustomer_Name", MySqlDbType.VarChar, 150),
                                          new MySqlParameter("@vaddress_line_1", MySqlDbType.VarChar, 255),
                                          new MySqlParameter("@vaddress_line_2", MySqlDbType.VarChar, 255),
                                          new MySqlParameter("@vcontact_person", MySqlDbType.VarChar, 100),
                                          new MySqlParameter("@vphone_no", MySqlDbType.VarChar, 20),
                                          new MySqlParameter("@vServiceLocation", MySqlDbType.VarChar, 50),
                                          new MySqlParameter("@vremarks", MySqlDbType.VarChar, 1000),

                                                          };

                                parameters[0].Value = objRow["CustomerBranchCode"].ToString();
                                parameters[1].Value = objRow["CustomerBranchName"].ToString();
                                parameters[2].Value = objRow["CustomerName"].ToString();
                                parameters[3].Value = objRow["AddressLine_1"].ToString();
                                parameters[4].Value = objRow["AddressLine_2"].ToString();
                                parameters[5].Value = objRow["ContactPerson"].ToString();
                                parameters[6].Value = objRow["PhoneNo"].ToString();
                                parameters[7].Value = objRow["ServiceLocation"].ToString();
                                parameters[8].Value = objRow["Remarks"].ToString();

                                dsResult = RunStoredProcedure("usp_InsertTempCustomerBranch", dbname, parameters, "MyDataTable");
                            }

                            conn.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                    }

                    string sqlStr = string.Empty;

                    sqlStr = @"SELECT E.*, IFNULL(D.customer_ID, 0) As customer_ID
                    from temp_customerbranch E
                    left join customers D on ltrim(rtrim(E.customer_Name)) = ltrim(rtrim(D.customer_Name))";


                    DataTable dtTempTable = RunQrysOnly(sqlStr, dbname);

                    return dtTempTable;
                }
            }
        }

        public DataTable gett_emp_customer_compileddata(DataTable dt, string dbname)
        {
            DataSet dsResult = new DataSet();

            using (MySqlConnection conn = CreateConnection(GetConnectionString(dbname)))
            {
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "truncate table temp_customer; ";
                            cmd.ExecuteNonQuery();

                            foreach (DataRow objRow in dt.Rows)
                            {
                                MySqlParameter[] parameters = { new MySqlParameter("@vcustomer_Code", MySqlDbType.VarChar, 50),
                                          new MySqlParameter("@vcustomer_Name", MySqlDbType.VarChar, 150),


                                                          };

                                parameters[0].Value = objRow["CustomerCode"].ToString();
                                parameters[1].Value = objRow["CustomerName"].ToString();

                                dsResult = RunStoredProcedure("usp_InsertTempCustomer", dbname, parameters, "MyDataTable");
                            }

                            conn.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                    }

                    string sqlStr = string.Empty;

                    sqlStr = @"SELECT *
                    from temp_customer ";


                    DataTable dtTempTable = RunQrysOnly(sqlStr, dbname);

                    return dtTempTable;
                }
            }
        }

        //new
        public DataTable gett_emp_product_compileddata(DataTable dt, string dbname)
        {
            DataSet dsResult = new DataSet();

            using (MySqlConnection conn = CreateConnection(GetConnectionString(dbname)))
            {
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "truncate table temp_product; ";
                            cmd.ExecuteNonQuery();

                            foreach (DataRow objRow in dt.Rows)
                            {
                                MySqlParameter[] parameters = { new MySqlParameter("@vproduct_Code", MySqlDbType.VarChar, 50),
                                          new MySqlParameter("@vproduct_Name", MySqlDbType.VarChar, 150),


                                                          };

                                parameters[0].Value = objRow["ProductCode"].ToString();
                                parameters[1].Value = objRow["ProductName"].ToString();

                                dsResult = RunStoredProcedure("usp_InsertTempProduct", dbname, parameters, "MyDataTable");
                            }

                            conn.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                    }

                    string sqlStr = string.Empty;

                    sqlStr = @"SELECT *
                    from temp_product ";


                    DataTable dtTempTable = RunQrysOnly(sqlStr, dbname);

                    return dtTempTable;
                }
            }
        }

        public DataTable gett_emp_brand_compileddata(DataTable dt, string dbname)
        {
            DataSet dsResult = new DataSet();

            using (MySqlConnection conn = CreateConnection(GetConnectionString(dbname)))
            {
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "truncate table temp_brand; ";
                            cmd.ExecuteNonQuery();

                            foreach (DataRow objRow in dt.Rows)
                            {
                                MySqlParameter[] parameters = { new MySqlParameter("@vbrand_Code", MySqlDbType.VarChar, 50),
                                          new MySqlParameter("@vbrand_Name", MySqlDbType.VarChar, 150),
                                         new MySqlParameter("@vproduct_Name", MySqlDbType.VarChar, 150),

                                                          };

                                parameters[0].Value = objRow["BrandCode"].ToString();
                                parameters[1].Value = objRow["BrandName"].ToString();
                                parameters[2].Value = objRow["ProductName"].ToString();

                                dsResult = RunStoredProcedure("usp_InsertTempBrand", dbname, parameters, "MyDataTable");
                            }

                            conn.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                    }

                    string sqlStr = string.Empty;

                    sqlStr = @"SELECT *
                    from temp_brand ";


                    DataTable dtTempTable = RunQrysOnly(sqlStr, dbname);

                    return dtTempTable;
                }
            }
        }

        public DataTable gett_emp_engineer_compileddata(DataTable dt, string dbname)
        {
            DataSet dsResult = new DataSet();

            using (MySqlConnection conn = CreateConnection(GetConnectionString(dbname)))
            {
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "truncate table temp_engineer; ";
                            cmd.ExecuteNonQuery();

                            foreach (DataRow objRow in dt.Rows)
                            {
                                MySqlParameter[] parameters = { new MySqlParameter("@vengineer_code", MySqlDbType.VarChar, 15),
                                          new MySqlParameter("@vengineer_Name", MySqlDbType.VarChar, 100),
                                          new MySqlParameter("@vengineer_Skill", MySqlDbType.VarChar, 100),
                                          new MySqlParameter("@vengineer_mobileno", MySqlDbType.VarChar, 20),
                                          new MySqlParameter("@vengineer_emailid", MySqlDbType.VarChar, 100),
                                          new MySqlParameter("@vengineer_password", MySqlDbType.VarChar, 50),

                                                          };

                                parameters[0].Value = objRow["engineer_code"].ToString();
                                parameters[1].Value = objRow["engineer_name"].ToString();
                                parameters[2].Value = objRow["engineer_skill"].ToString();
                                parameters[3].Value = objRow["mobileno"].ToString();
                                parameters[4].Value = objRow["emailid"].ToString();
                                parameters[5].Value = objRow["password"].ToString();


                                dsResult = RunStoredProcedure("usp_InsertTempEngineer", dbname, parameters, "MyDataTable");
                            }

                            conn.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                    }

                    string sqlStr = string.Empty;

                    sqlStr = @"SELECT *
                    from temp_engineer ";


                    DataTable dtTempTable = RunQrysOnly(sqlStr, dbname);

                    return dtTempTable;
                }
            }
        }

        public DataTable GetMastersDetailswithTable(string mastertblname, string masteridname, int masteridvalue, string mastercolumns, int companyID, string dbname)
        {
            DataSet dsResult = new DataSet();
            string strSql = string.Empty;
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int32) };
            parameters[0].Value = Convert.ToInt32(companyID);
            //if (mastertblname == "question")
            //{
            //    string val = string.Empty;
            //    if (masteridvalue == 1)
            //    {
            //        val = "T";
            //    }
            //    else
            //    {
            //        val = "O";
            //    }

            //    strSql = @" SELECT " + mastercolumns + " from " + mastertblname + " where " + masteridname + "='" + val + "' and Active=1 and company_ID=" + companyID + " ";

            //}
            //else
            //{
            strSql = @" SELECT " + mastercolumns + " from " + mastertblname + " where company_ID=" + companyID + " and " + masteridname + "=" + masteridvalue + " and Active=1 ";
            //}
            dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }

        public int UpdateMasters(string tablename, string updatedvalues, string conditions, int companyID, string dbname, int isDefault = 0, string addConditions = "")
        {
            int dsResult = 0;
            string sqlStr = string.Empty;

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
                            string sql = string.Empty;
                            cmd.Transaction = trans;

                            if (isDefault == 1)
                            {
                                if (tablename.ToUpper() == "category".ToUpper())
                                    sql = "Update " + tablename + " SET isDefaultValue=0 where company_ID=" + companyID + "  " + addConditions + " and Active=1 ";
                                else
                                    sql = "Update " + tablename + " SET isDefaultValue=0 where company_ID=" + companyID + " and Active=1 ";

                                cmd.CommandText = sql;

                                cmd.Parameters.Add(AddTypedParam("@companyID", MySqlDbType.Int32, companyID));

                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }

                            sql = "Update " + tablename + " SET " + updatedvalues + " where company_ID=" + companyID + " and Active=1 ";

                            if (!string.IsNullOrEmpty(conditions))
                            {
                                sql = sql + "  and  " + conditions + "";
                            }
                            cmd.CommandText = sql;

                            cmd.Parameters.Add(AddTypedParam("@companyID", MySqlDbType.Int32, companyID));

                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                        dsResult = 1;
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        dsResult = 0;
                    }
                    finally
                    {
                        //this.m_SqlConnection.Close();
                        CloseConnectionPooling(conn);
                    }
                }
            }
            return dsResult;
        }


        public string VerifyCodes(int compnayID, string colname, string colvalue, string tables, string condition, string dbname)
        {
            string returnval = string.Empty;
            string sql = string.Empty;

            if (tables == "Session")
            {
                sql = @"select case when  count(*) > 0 then 'Exists' else 'New Value' end as result from " + tables + " where Active=1 and  " + colname + " = '" + colvalue + "' ";
            }
            else
            {
                sql = @"select case when  count(*) > 0 then 'Exists' else 'New Value' end as result from " + tables + " where company_ID='" + compnayID + "' and  " + colname + " = '" + colvalue + "' and Active = 1 ";
            }

            if (!string.IsNullOrEmpty(condition))
                sql += condition;

            returnval = RunExecuteScalarSQLString(sql, dbname);
            return returnval;
        }

        public DataTable getDefaultList(string companyID, string module, string dbname)
        {
            DataSet dsResult = new DataSet();
            string strSql = string.Empty;
            MySqlParameter[] parameters = { new MySqlParameter("@Company_ID", SqlDbType.Int) };
            parameters[0].Value = Convert.ToInt32(companyID);
            strSql = @"Select Company_ID, Template_ID, TemplateType_ID, TemplateType, case When EmailFlag = 1 then 'Yes' when EmailFlag = 0 then 'No' end as EmailFlag, 
                    case When SMSFlag = 1 then 'Yes' when SMSFlag = 0 then 'No' end as SMSFlag, case When WhatsappFlag = 1 then 'Yes' when WhatsappFlag = 0 then 'No' end as WhatsappFlag,
                    MailSubject, MailContent, SMSSubject, SMSContent, WhatsappContent, TemplateName, Category_ID, Category_ID_Text,MailCC
                    from NotificationTemplate Where Company_ID = @Company_ID And Active = 1 ";
            dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];

        }
        public int DeleteMasters(string mastertblname, string masteridname, int masteridvalue, int companyID, int modifiedby, string dbname)
        {
            int dsResult = 0;
            string sqlStr = string.Empty;

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

                            cmd.CommandText = @"UPDATE " + mastertblname + "  SET Active=0, ModifiedDT=NOW(), ModifiedBy=" + modifiedby + "  where   company_ID=" + companyID + " and " + masteridname + "=" + masteridvalue + " and Active=1 ";

                            cmd.Parameters.Add(AddTypedParam("@CompanyID", MySqlDbType.Int32, companyID));
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                        dsResult = 1;
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        dsResult = 0;
                    }
                    finally
                    {
                        //this.m_SqlConnection.Close();
                        CloseConnectionPooling(conn);
                    }
                }
            }
            return dsResult;
        }
        public DataTable ReturnName(string sSelectedTextName, string sTableName, string sFilterCondition, string dbname)
        {
            DataTable dsResult = new DataTable();
            string sqlStr = string.Empty;
            sqlStr += " Select " + sSelectedTextName + "  From " + sTableName + " where 1=1";
            if (!string.IsNullOrEmpty(sFilterCondition))
            {
                sqlStr += " And " + sFilterCondition;
            }
            try
            {
                return dsResult = RunQrysOnly(sqlStr, dbname);
            }
            catch (Exception ex)
            {
                return dsResult;
            }
        }

        public DataTable GetNotificationTemplateDetails(int TemplateTypeID, string dbname)
        {
            string strSql = @"SELECT Template_ID, TemplateType_ID, TemplateType, EmailFlag, SMSFlag, WhatsappFlag, MailSubject, MailContent, SMSSubject, SMSContent, WhatsappContent, TemplateName, Category_ID, Category_ID_Text, MailRecipientType, SMSRecipientType, WhatsappRecipientType, MailCC FROM notificationtemplate WHERE TemplateType_ID = @TemplateType_ID AND Active = 1; ";
            MySqlParameter[] parameters = { new MySqlParameter("@TemplateType_ID", MySqlDbType.VarChar, 50)
                                          };
            parameters[0].Value = TemplateTypeID;

            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }


        public DataTable GetJobEngineesEmailIDs(int TicketID, string dbname)
        {
            // Define the SQL query to retrieve email IDs based on the TicketID and CompanyID
            string strSql = @"SELECT e1.emailid AS emailid
                    FROM tickets t
                    JOIN engineers e1 ON t.Assigned_to = e1.engineer_id 
                    WHERE t.TicketID = @TicketID 

                    UNION

                    SELECT e2.emailid AS emailid
                    FROM (
                        SELECT *
                        FROM tickets_status
                        WHERE tickets_id = @TicketID 
                        ORDER BY ticketstatusid DESC
                        LIMIT 1
                    ) AS last_status
                    JOIN engineers e2 ON last_status.engineer_id = e2.engineer_id;";

            // Define the parameters
            MySqlParameter[] parameters =
                {
                    new MySqlParameter("@TicketID", MySqlDbType.Int32) { Value = TicketID },
                    new MySqlParameter("@CompanyID", MySqlDbType.Int32) { Value = 1 } // Set CompanyID to 1 as per your requirement
                };

            // Execute the SQL with parameters
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }

        public DataTable GetSupervisorEmailIDs(string dbname)
        {
            // Define the SQL query to retrieve email IDs based on the TicketID and CompanyID
            string strSql = @"SELECT emailid FROM engineers WHERE EngineerType = 'S' AND Active = 1; ";

            // Define the parameters
            MySqlParameter[] parameters =
                {
                    new MySqlParameter("@CompanyID", MySqlDbType.Int32) { Value = 1 } // Set CompanyID to 1 as per your requirement
                };

            // Execute the SQL with parameters
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
           
            return dsResult.Tables[0];
        }


        public DataTable GetOutstandingJobListData(int companyID, string dbname)
        {
            string sqlStr = string.Empty;

            sqlStr = @"SELECT DISTINCT t.TicketID,t.TicketNo,t.CreatedDT AS tktdate,IFNULL(jb.JobTypes,'NA') AS JobTypes, z.customer_Name,zx.customer_branch_Name,b.brand_name, m.model_name,t.Remarks,p.product_name,
            s.service_type_name,pb.problem_name,IFNULL(eg.engineer_code,'NA') AS engineer_code,st.StatusName AS CurrentStatus,t.tothrs_spent AS tothrs,eg.engineer_id,
            cy.CompanyCode,t.SerialNumber,t.NameOfCaller,t.Call_Detail_Nature,IFNULL(t.Receipt_amt,0.00) AS Receipt_amt,t.invoice_amt,t.ReportDt,t.CallRecivedAt,
            t.PartNo,IFNULL(eg.engineer_name,'NA') AS engineer_name,CONCAT(t.invoice_no,' / ',DATE_FORMAT(STR_TO_DATE(t.invoice_dt, '%Y-%m-%d'),'%d-%m-%Y')) AS Invoice, t.NameOfCaller, t.Call_Detail_Nature, TIMESTAMPDIFF(HOUR, t.createdDT, now()) 
            FROM customerbranch c, brands b,models m,products p,service_type s,problems pb,Status st, tickets t 
            LEFT JOIN Engineers eg ON t.Assigned_to = eg.engineer_id AND eg.Active = 1 
            LEFT JOIN jobtype jb ON t.JobTypeId = jb.JobTypeId, customers z, customerbranch zx, company cy 
            WHERE z.customer_ID = c.customer_id AND zx.customer_branch_id = t.BranchID 
            AND t.CustomerID = c.customer_id AND t.brand_id = b.brand_id AND t.model_id = m.model_id AND t.product_id = p.product_id 
            AND t.ServiceTypeID = s.service_typeid AND cy.CompanyID = t.CompanyID AND t.ProblemID = pb.problem_id 
            AND t.Active = 1 AND c.Active = 1 AND b.Active = 1 AND m.Active = 1 AND p.Active = 1 
            AND s.Active = 1 AND pb.Active = 1 AND t.StatusID = st.StatusID AND st.Active = 1
            AND t.StatusID != 2
            AND TIMESTAMPDIFF(HOUR, t.createdDT, now()) > 24
            ORDER BY t.CreatedDT desc ";

            MySqlParameter[] parameters = { new MySqlParameter("Company_ID", MySqlDbType.Int32)};

            parameters[0].Value = 1;

            DataSet dsResult = RunSQLWithParam(sqlStr, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }

        public DataTable GetVehicleMileageList(string StartTime, string EndTime, string engineerid, string vehicleid, string dbname)
        {
            string sqlStr = string.Empty;
            string sCondition = string.Empty;

            if (!string.IsNullOrEmpty(vehicleid))
                sCondition = " AND M.VehicleID in ('" + vehicleid + "') ";

            if (!string.IsNullOrEmpty(engineerid))
                sCondition += " AND M.EngineerID in ('" + engineerid + "') ";


            sqlStr = @"SELECT T.ticketNo, date_format(M.Created_DT,'%d-%m-%Y %h:%i:%s') as MileageDate, M.StartKM, M.EndKM, M.totalKM, M.Mileage, 
	                    V.VehicleCode, V.VehicleName, V.VehicleNo, E.Engineer_Code, E.Engineer_Name, C.customer_name, B.customer_branch_name 
                    FROM mileagedetails M, tickets T, engineervehicle V, engineers E, customers C, customerbranch B
                    WHERE M.TicketID = T.TicketID AND M.VehicleID = V.VehicleID
                    AND M.EngineerID = E.Engineer_ID
                    AND T.CustomerID = C.Customer_ID AND T.BranchID = B.Customer_Branch_ID
                    AND C.customer_id = B.customer_id
                    AND DATE(M.Created_DT) BETWEEN @StartDate AND @EndDate  ";

            if (!string.IsNullOrEmpty(sCondition))
                sqlStr += sCondition;

            sqlStr += " ORDER BY  M.Created_DT";

            MySqlParameter[] parameters = { new MySqlParameter("StartDate", MySqlDbType.VarChar, 50),
                                          new MySqlParameter("EndDate", MySqlDbType.VarChar, 50)};
            parameters[0].Value = StartTime.Trim();
            parameters[1].Value = EndTime.Trim();

            DataSet dsResult = RunSQLWithParam(sqlStr, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
    }
}
