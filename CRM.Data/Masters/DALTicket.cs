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
    public class DALTicket : Base.Data.BaseSql
    {
        public string GetDocumentNumber(int iCompanyID, string sSerialKey, string sRetriveType, string dbname)
        {
            // RetriveType = G - Generate, S - Show

            MySqlParameter[] parameters = { new MySqlParameter("CompID", MySqlDbType.Int32),
                                          new MySqlParameter("SerKey", MySqlDbType.VarChar,5),
                                          new MySqlParameter("RetriveType", MySqlDbType.VarChar,1)};

            parameters[0].Value = 1;
            parameters[1].Value = sSerialKey;
            parameters[2].Value = sRetriveType;

            DataSet dsResult = RunStoredProcedure("usp_DocumentNumbers", dbname, parameters, "MyDataTable");

            String Str = dsResult.Tables[1].Rows[0][0].ToString();
            return Str;
        }
        public DataTable GetSeverelist(string dbname)
        {
            string strSql = @"select sevID,sevName from severity WHERE Active=1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public DataTable GetPrioritylist(string dbname)
        {
            string strSql = @"select prID,prName from priority WHERE Active=1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
//        public DataTable GetTicketList(string fromdate, string todate, string status,int customer,int Branch,int Product,int Brand,int Model,int Problem,int Engineer,int Jobtype, string dbname)
//        {
//            // string strSql = @" select  t.TicketNo,t.TicketDt, c.customer_name where t.CustomerID=c.customer_branch_id and  t.TicketID=" + TicketID + " and t.Active=1";
//            string strSql = string.Empty;
//            //strSql = @"  select t.TicketID,t.TicketNo, t.CreatedDT as tktdate, z.customer_Name,zx.customer_branch_Name,b.brand_name,m.model_name,t.Remarks,
//            //           p.product_name,s.service_type_name,pb.problem_name,ifnull(eg.engineer_code,'NA') as engineer_code,st.StatusName as CurrentStatus,t.tothrs_spent as tothrs,
//            //           t.SerialNumber,ifnull(t.Receipt_amt,0.00)as Receipt_amt,t.invoice_amt,t.ReportDt,t.PartNo,ifnull(eg.engineer_name,'NA') as engineer_name,
//            //     Concat(t.invoice_no,' / ',date_format(str_to_date(t.invoice_dt, '%Y-%m-%d'),'%d-%m-%Y')) as Invoice From customerbranch c,brands b,models m, products p,service_type s,problems pb,Status st,
//            //           tickets t left join Engineers eg  on t.Assigned_to=eg.engineer_id and eg.Active=1 ,customers z,customerbranch zx where z.customer_ID=c.customer_id and zx.customer_branch_id=t.BranchID and
//            //           t.CustomerID=c.customer_id and t.brand_id=b.brand_id  and t.model_id=m.model_id and t.product_id=p.product_id and t.ServiceTypeID=s.service_typeid 
//            //           and t.ProblemID=pb.problem_id and t.Active=1 and c.Active=1 and b.Active=1 and m.Active=1 and p.Active=1 and s.Active=1 and pb.Active=1 and t.StatusID=st.StatusID  and st.Active=1";


//            strSql = @"SELECT t.TicketID,t.TicketNo,t.CreatedDT AS tktdate,IFNULL(jb.JobTypes,'NA') AS JobTypes, z.customer_Name,zx.customer_branch_Name,b.brand_name, m.model_name,t.Remarks,p.product_name,
//    s.service_type_name,pb.problem_name,IFNULL(eg.engineer_code,'NA') AS engineer_code,st.StatusName AS CurrentStatus,t.tothrs_spent AS tothrs,eg.engineer_id,
//    cy.CompanyCode,t.SerialNumber,t.NameOfCaller,t.Call_Detail_Nature,IFNULL(t.Receipt_amt,0.00) AS Receipt_amt,t.invoice_amt,t.ReportDt,t.CallRecivedAt,
//    t.PartNo,IFNULL(eg.engineer_name,'NA') AS engineer_name,CONCAT(t.invoice_no,' / ',DATE_FORMAT(STR_TO_DATE(t.invoice_dt, '%Y-%m-%d'),'%d-%m-%Y')) AS Invoice 
//FROM customerbranch c, brands b,models m,products p,service_type s,problems pb,Status st,  tickets t LEFT JOIN Engineers eg ON t.Assigned_to = eg.engineer_id AND eg.Active = 1 
//LEFT JOIN jobtype jb ON t.JobTypeId = jb.JobTypeId, customers z, customerbranch zx, company cy WHERE z.customer_ID = c.customer_id  AND zx.customer_branch_id = t.BranchID 
//    AND t.CustomerID = c.customer_id AND t.brand_id = b.brand_id   AND t.model_id = m.model_id  AND t.product_id = p.product_id   AND t.ServiceTypeID = s.service_typeid 
//    AND cy.CompanyID = t.CompanyID  AND t.ProblemID = pb.problem_id AND t.Active = 1 AND c.Active = 1  AND b.Active = 1  AND m.Active = 1  AND p.Active = 1 
//    AND s.Active = 1  AND pb.Active = 1  AND t.StatusID = st.StatusID  AND st.Active = 1";



//            //            strSql = @"  select t.TicketID,t.TicketNo,jb.JobTypes, t.CreatedDT as tktdate, z.customer_Name,zx.customer_branch_Name,b.brand_name,m.model_name,t.Remarks,
//            //                       p.product_name,s.service_type_name,pb.problem_name,ifnull(eg.engineer_code,'NA') as engineer_code,st.StatusName as CurrentStatus,t.tothrs_spent as tothrs,
//            //                       t.SerialNumber,t.NameOfCaller, ifnull(t.Receipt_amt,0.00)as Receipt_amt,t.invoice_amt,t.ReportDt,t.CallRecivedAt,t.PartNo,ifnull(eg.engineer_name,'NA') as engineer_name,
//            //                 Concat(t.invoice_no,' / ',date_format(str_to_date(t.invoice_dt, '%Y-%m-%d'),'%d-%m-%Y')) as Invoice From customerbranch c,brands b,models m, products p,service_type s,problems pb,Status st,jobtype jb,
//            //                       tickets t left join Engineers eg  on t.Assigned_to=eg.engineer_id and eg.Active=1 ,customers z,customerbranch zx where z.customer_ID=c.customer_id and zx.customer_branch_id=t.BranchID and
//            //                       t.CustomerID=c.customer_id and t.brand_id=b.brand_id  and t.model_id=m.model_id and t.product_id=p.product_id and t.ServiceTypeID=s.service_typeid and t.JobTypeId=jb.JobTypeId
//            //                       and t.ProblemID=pb.problem_id and t.Active=1 and c.Active=1 and b.Active=1 and m.Active=1 and p.Active=1 and s.Active=1 and pb.Active=1 and t.StatusID=st.StatusID  and st.Active=1 and jb.Active=1";




//            if (!string.IsNullOrEmpty(fromdate) && !string.IsNullOrEmpty(todate))
//            {
//                strSql = strSql + "  and date(t.CreatedDT) between '" + fromdate + "' and '" + todate + "'";
//            }


//            // Add condition for status and other parameters
//           // if (!string.IsNullOrEmpty(status))
//        //    {
//                string actstat = string.Empty;
//                if (!string.IsNullOrEmpty(status))
//                {
//                    actstat += " AND t.StatusID = '" + status + "'";
//                }
//               // else
//             //   {
                   

//                    if (!string.IsNullOrEmpty(customer))
//                    {
//                        actstat += " AND t.CustomerID = '" + customer + "'";
//                    }
//                    if (!string.IsNullOrEmpty(Branch))
//                    {
//                        actstat += " AND t.BranchID = '" + Branch + "'";
//                    }
//                    if (!string.IsNullOrEmpty(Product))
//                    {
//                        actstat += " AND t.product_id = '" + Product + "'";
//                    }
//                    if (!string.IsNullOrEmpty(Model))
//                    {
//                        actstat += " AND t.model_id = '" + Model + "'";
//                    }
//                    if (!string.IsNullOrEmpty(Brand))
//                    {
//                        actstat += " AND t.brand_id = '" + Brand + "'";
//                    }
//                    if (!string.IsNullOrEmpty(Problem))
//                    {
//                        actstat += " AND t.ProblemID = '" + Problem + "'";
//                    }
//                    if (!string.IsNullOrEmpty(Engineer))
//                    {
//                        actstat += " AND t.Assigned_to = '" + Engineer + "'";
//                    }
//                    if (!string.IsNullOrEmpty(Jobtype))
//                    {
//                        actstat += " AND t.JobtypeID = '" + Jobtype + "'";
//                    }
//              //  }
//                strSql += actstat;
//           // }

//            //from
//            //if (!string.IsNullOrEmpty(status))
//            //{
//            //    string actstat = string.Empty;
//            //    if (status == "0")
//            //    {
//            //        actstat = string.Empty;
//            //    }
//            //    else
//            //    {
//            //      //  actstat = "AND t.StatusID = '" + status + " AND t.CustomerID  = '"++"'";
//            //        actstat = " AND t.StatusID = '" + status + "' AND t.CustomerID = '" + customer + "' AND t.BrandID = '" + Branch + "'AND t.CustomerID = '" + customer + "' AND t.BranchID = '" + Branch + "'AND t.ProductID = '" + Product + "' AND t.BrandID = '" + Branch + "'AND t.ModelID = '" + Model + "' AND t.ProblemID = '" + Problem + "'AND t.EngineerID = '" + Engineer + "' AND t.JobtypeID = '" + Jobtype + "'";
                    
                
//            //    }
//                //uptothis
//                //if (status == "1")
//                //{
//                //    actstat = " and t.StatusID = (select StatusID From status where sequenceno=1 and active=1 limit 1)";
//                //}
//                //if (status == "2")
//                //{
//                //    //actstat = " and t.StatusID = (select StatusID From status where sequenceno=2 and active=1 limit 1)";

//                //    actstat = " and t.StatusID not in ( select StatusID From status where sequenceno = 1 and active = 1 union all " +
//                //        "select StatusID From status where StatusName like '%Closed' and active = 1) ";
//                //}
//                //if (status == "3")
//                //{
//                //    //actstat = " and t.StatusID = (select StatusID From status where sequenceno=(select Max(sequenceNo) From Status where Active=1))";
//                //    //actstat = " and t.StatusID = (select StatusID From status where StatusName='Closed')"; //comment by noor 07/08/2021 for filter not work
//                //    actstat = " and t.StatusID = (select StatusID From status where StatusName like '%Closed' and active=1)"; //add by noor 07/08/2021 for closed job filter

//                //}

//             //   strSql = strSql + actstat;
//          //  }

//            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
//            parameters[0].Value = 1;
//            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
//            return dsResult.Tables[0];
//        }
        public DataTable GetTicketList(string fromdate, string todate, int? status, int? customer, int? Branch, int? Product, int? Brand, int? Model, int? Problem, int? Engineer, int? Jobtype, string dbname)
        {
            string strSql = string.Empty;

            strSql = @"SELECT DISTINCT t.TicketID,t.TicketNo,t.CreatedDT AS tktdate,IFNULL(jb.JobTypes,'NA') AS JobTypes, z.customer_Name,zx.customer_branch_Name,b.brand_name, m.model_name,t.Remarks,p.product_name,
    s.service_type_name,pb.problem_name,IFNULL(eg.engineer_code,'NA') AS engineer_code,st.StatusName AS CurrentStatus,t.tothrs_spent AS tothrs,eg.engineer_id,
    cy.CompanyCode,t.SerialNumber,t.NameOfCaller,t.Call_Detail_Nature,IFNULL(t.Receipt_amt,0.00) AS Receipt_amt,t.invoice_amt,t.ReportDt,t.CallRecivedAt,
    t.PartNo,IFNULL(eg.engineer_name,'NA') AS engineer_name,CONCAT(t.invoice_no,' / ',DATE_FORMAT(STR_TO_DATE(t.invoice_dt, '%Y-%m-%d'),'%d-%m-%Y')) AS Invoice 
    FROM customerbranch c, brands b,models m,products p,service_type s,problems pb,Status st, tickets t 
    LEFT JOIN Engineers eg ON t.Assigned_to = eg.engineer_id AND eg.Active = 1 
    LEFT JOIN jobtype jb ON t.JobTypeId = jb.JobTypeId, customers z, customerbranch zx, company cy 
    WHERE z.customer_ID = c.customer_id AND zx.customer_branch_id = t.BranchID 
    AND t.CustomerID = c.customer_id AND t.brand_id = b.brand_id AND t.model_id = m.model_id AND t.product_id = p.product_id 
    AND t.ServiceTypeID = s.service_typeid AND cy.CompanyID = t.CompanyID AND t.ProblemID = pb.problem_id 
    AND t.Active = 1 AND c.Active = 1 AND b.Active = 1 AND m.Active = 1 AND p.Active = 1 
    AND s.Active = 1 AND pb.Active = 1 AND t.StatusID = st.StatusID AND st.Active = 1";

            if (!string.IsNullOrEmpty(fromdate) && !string.IsNullOrEmpty(todate))
            {
                strSql += " AND date(t.CreatedDT) BETWEEN '" + fromdate + "' AND '" + todate + "'";
            }

            string actstat = string.Empty;
            if (status!=0)
            {
                actstat += " AND t.StatusID = '" + status + "'";
            }

            if (customer != 0)
            {
                actstat += " AND t.CustomerID = '" + customer + "'";
            }
            if (Branch != 0)
            {
                actstat += " AND t.BranchID = '" + Branch + "'";
            }
            if (Product != 0)
            {
                actstat += " AND t.product_id = '" + Product + "'";
            }
            if (Model != 0)
            {
                actstat += " AND t.model_id = '" + Model + "'";
            }
            if (Brand != 0)
            {
                actstat += " AND t.brand_id = '" + Brand + "'";
            }
            if (Problem != 0)
            {
                actstat += " AND t.ProblemID = '" + Problem + "'";
            }
            if (Engineer != 0)
            {
                actstat += " AND t.Assigned_to = '" + Engineer + "'";
            }
            if (Jobtype != 0)
            {
                actstat += " AND t.JobtypeID = '" + Jobtype + "'";
            }

            strSql += actstat;
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
                        parameters[0].Value = 1;
                        DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
           // DataSet dsResult = RunSQLWithParam(strSql, dbname, null, "MyDataTable");
            return dsResult.Tables[0];
        }


//        public DataTable GetTicketListExcel(string fromdate, string todate, int? status, int? customer, int? Branch, int? Product, int? Brand, int? Model, int? Problem, int? Engineer, int? Jobtype, string dbname)
//        {
//            string strSql = string.Empty;

//            strSql = @"SELECT DISTINCT t.TicketID,t.TicketNo,t.CreatedDT AS tktdate,IFNULL(jb.JobTypes,'NA') AS JobTypes, z.customer_Name,zx.customer_branch_Name,b.brand_name, m.model_name,t.Remarks,p.product_name,
//    s.service_type_name,pb.problem_name,IFNULL(eg.engineer_code,'NA') AS engineer_code,st.StatusName AS CurrentStatus,t.tothrs_spent AS tothrs,eg.engineer_id,
//    cy.CompanyCode,t.SerialNumber,t.NameOfCaller,t.Call_Detail_Nature,IFNULL(t.Receipt_amt,0.00) AS Receipt_amt,t.invoice_amt,t.ReportDt,t.CallRecivedAt,
//    t.PartNo,IFNULL(eg.engineer_name,'NA') AS engineer_name,CONCAT(t.invoice_no,' / ',DATE_FORMAT(STR_TO_DATE(t.invoice_dt, '%Y-%m-%d'),'%d-%m-%Y')) AS Invoice ,
//       ts.status_id,ts.engineer_id ,(ts.endtime) as endtime,
//       st.StatusName as StatusName
//    FROM customerbranch c, brands b,models m,products p,service_type s,problems pb,Status st, tickets t 
//    LEFT JOIN Engineers eg ON t.Assigned_to = eg.engineer_id AND eg.Active = 1 
//    LEFT JOIN jobtype jb ON t.JobTypeId = jb.JobTypeId, customers z, customerbranch zx, company cy ,
//	tickets_status ts
//    WHERE z.customer_ID = c.customer_id AND zx.customer_branch_id = t.BranchID 
//    AND t.CustomerID = c.customer_id AND t.brand_id = b.brand_id AND t.model_id = m.model_id AND t.product_id = p.product_id 
//    AND t.ServiceTypeID = s.service_typeid AND cy.CompanyID = t.CompanyID AND t.ProblemID = pb.problem_id   AND st.StatusID=ts.status_id 
//         AND t.TicketID = ts.tickets_ID
//    AND t.Active = 1 AND c.Active = 1 AND b.Active = 1 AND m.Active = 1 AND p.Active = 1 
//    AND s.Active = 1 AND pb.Active = 1 AND t.StatusID = st.StatusID AND st.Active = 1";

//            if (!string.IsNullOrEmpty(fromdate) && !string.IsNullOrEmpty(todate))
//            {
//                strSql += " AND date(t.CreatedDT) BETWEEN '" + fromdate + "' AND '" + todate + "'";
//            }

//            string actstat = string.Empty;
//            if (status != 0)
//            {
//                actstat += " AND t.StatusID = '" + status + "'";
//            }

//            if (customer != 0)
//            {
//                actstat += " AND t.CustomerID = '" + customer + "'";
//            }
//            if (Branch != 0)
//            {
//                actstat += " AND t.BranchID = '" + Branch + "'";
//            }
//            if (Product != 0)
//            {
//                actstat += " AND t.product_id = '" + Product + "'";
//            }
//            if (Model != 0)
//            {
//                actstat += " AND t.model_id = '" + Model + "'";
//            }
//            if (Brand != 0)
//            {
//                actstat += " AND t.brand_id = '" + Brand + "'";
//            }
//            if (Problem != 0)
//            {
//                actstat += " AND t.ProblemID = '" + Problem + "'";
//            }
//            if (Engineer != 0)
//            {
//                actstat += " AND t.Assigned_to = '" + Engineer + "'";
//            }
//            if (Jobtype != 0)
//            {
//                actstat += " AND t.JobtypeID = '" + Jobtype + "'";
//            }

//            strSql += actstat;
//            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
//            parameters[0].Value = 1;
//            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
//            // DataSet dsResult = RunSQLWithParam(strSql, dbname, null, "MyDataTable");
//            return dsResult.Tables[0];
//        }
        //above working

//        public DataTable GetTicketListExcel(string fromdate, string todate, int? status, int? customer, int? Branch, int? Product, int? Brand, int? Model, int? Problem, int? Engineer, int? Jobtype, string dbname)
//        {
//            string strSql = @"
//        SET SESSION group_concat_max_len = 1000000;
//        SELECT 
//            GROUP_CONCAT(DISTINCT
//                CONCAT(
//                    'MAX(CASE WHEN StatusName = ''',
//                    StatusName,
//                    ''' THEN endtime END) AS `',
//                    REPLACE(StatusName, ' ', '_'),
//                    '`'
//                )
//            ) INTO @pivot_query
//        FROM 
//            (
//                SELECT DISTINCT
//                    st.StatusName
//                FROM 
//                    Status st
//            ) AS subquery;
//
//        SET @query = CONCAT('
//            SELECT 
//                t.TicketID,
//                t.TicketNo,
//                t.CreatedDT AS Date,
//                IFNULL(jb.JobTypes, ""NA"") AS JobTypes,
//                z.customer_Name,
//                zx.customer_branch_Name,
//                b.brand_name,
//                m.model_name,
//                t.Remarks,
//                p.product_name,
//                s.service_type_name,
//                pb.problem_name,
//                IFNULL(eg.engineer_code, ""NA"") AS engineer_code,
//                t.tothrs_spent AS tothrs,
//                IFNULL(eg.engineer_id, 0) AS engineer_id,
//                cy.CompanyCode,
//                t.SerialNumber,
//                t.NameOfCaller,
//                t.Call_Detail_Nature,
//                IFNULL(t.Receipt_amt, 0.00) AS Receipt_amt,
//                t.invoice_amt,
//                t.ReportDt,
//                t.CallRecivedAt,
//                t.PartNo,
//                IFNULL(eg.engineer_name, ""NA"") AS engineer_name,
//                CONCAT(t.invoice_no, ' / ', DATE_FORMAT(STR_TO_DATE(t.invoice_dt, ""%Y-%m-%d""), ""%d-%m-%Y"")) AS Invoice,
//                ts.status_id,
//                ts.engineer_id AS engineer_id1,
//                ', @pivot_query, '
//            FROM 
//                customerbranch c,
//                brands b,
//                models m,
//                products p,
//                service_type s,
//                problems pb,
//                Status st,
//                tickets t
//            LEFT JOIN 
//                Engineers eg ON t.Assigned_to = eg.engineer_id AND eg.Active = 1
//            LEFT JOIN 
//                jobtype jb ON t.JobTypeId = jb.JobTypeId,
//                customers z,
//                customerbranch zx,
//                company cy,
//                tickets_status ts
//            WHERE 
//                z.customer_ID = c.customer_id
//                AND zx.customer_branch_id = t.BranchID
//                AND t.CustomerID = c.customer_id
//                AND t.brand_id = b.brand_id
//                AND t.model_id = m.model_id
//                AND t.product_id = p.product_id
//                AND t.ServiceTypeID = s.service_typeid
//                AND cy.CompanyID = t.CompanyID
//                AND t.ProblemID = pb.problem_id
//                AND st.StatusID = ts.status_id
//                AND t.TicketID = ts.tickets_ID
//                AND t.Active = 1
//                AND c.Active = 1
//                AND b.Active = 1
//                AND m.Active = 1
//                AND p.Active = 1
//                AND s.Active = 1
//                AND pb.Active = 1
//                AND t.StatusID = st.StatusID
//                AND st.Active = 1
//    ";

//            if (!string.IsNullOrEmpty(fromdate) && !string.IsNullOrEmpty(todate))
//            {
//                strSql += " AND date(t.CreatedDT) BETWEEN '" + fromdate + "' AND '" + todate + "'";

//            }

//            string actstat = string.Empty;
//            if (status != 0)
//            {
//                actstat += " AND t.StatusID = '" + status + "'";
//            }
//            if (customer != 0)
//            {
//                actstat += " AND t.CustomerID = '" + customer + "'";
//            }
//            if (Branch != 0)
//            {
//                actstat += " AND t.BranchID = '" + Branch + "'";
//            }
//            if (Product != 0)
//            {
//                actstat += " AND t.product_id = '" + Product + "'";
//            }
//            if (Model != 0)
//            {
//                actstat += " AND t.model_id = '" + Model + "'";
//            }
//            if (Brand != 0)
//            {
//                actstat += " AND t.brand_id = '" + Brand + "'";
//            }
//            if (Problem != 0)
//            {
//                actstat += " AND t.ProblemID = '" + Problem + "'";
//            }
//            if (Engineer != 0)
//            {
//                actstat += " AND t.Assigned_to = '" + Engineer + "'";
//            }
//            if (Jobtype != 0)
//            {
//                actstat += " AND t.JobtypeID = '" + Jobtype + "'";
//            }

//            strSql += actstat;

//            strSql += @"
//            GROUP BY 
//                t.TicketID
//        ');
//
//        PREPARE stmt FROM @query;
//        EXECUTE stmt;
//        DEALLOCATE PREPARE stmt;
//    ";

//            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
//            parameters[0].Value = 1;
//            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
//            // DataSet dsResult = RunSQLWithParam(strSql, dbname, null, "MyDataTable");
//            return dsResult.Tables[0];
//        }

        // added for changes in excel

        public DataSet GetTicketListExcel(string fromdate, string todate, int? status, int? customer, int? branch, int? product, int? brand, int? model, int? problem, int? engineer, int? jobType, bool ClosedStatusFlag, string dbname)
        {
            MySqlParameter[] parameters = {
        new MySqlParameter("@FromDate", MySqlDbType.VarChar, 20),
        new MySqlParameter("@ToDate", MySqlDbType.VarChar, 20),
        new MySqlParameter("@Status", MySqlDbType.Int32),
        new MySqlParameter("@Customer", MySqlDbType.Int32),
        new MySqlParameter("@Branch", MySqlDbType.Int32),
        new MySqlParameter("@Product", MySqlDbType.Int32),
        new MySqlParameter("@Brand", MySqlDbType.Int32),
        new MySqlParameter("@Model", MySqlDbType.Int32),
        new MySqlParameter("@Problem", MySqlDbType.Int32),
        new MySqlParameter("@Engineer", MySqlDbType.Int32),
        new MySqlParameter("@JobType", MySqlDbType.Int32),
        new MySqlParameter("@ClosedStatusFlag", MySqlDbType.Bit),
        new MySqlParameter("@DbName", MySqlDbType.VarChar, 100)
    };

            parameters[0].Value = fromdate;
            parameters[1].Value = todate;
            parameters[2].Value = status ?? (object)DBNull.Value;
            parameters[3].Value = customer ?? (object)DBNull.Value;
            parameters[4].Value = branch ?? (object)DBNull.Value;
            parameters[5].Value = product ?? (object)DBNull.Value;
            parameters[6].Value = brand ?? (object)DBNull.Value;
            parameters[7].Value = model ?? (object)DBNull.Value;
            parameters[8].Value = problem ?? (object)DBNull.Value;
            parameters[9].Value = engineer ?? (object)DBNull.Value;
            parameters[10].Value = jobType ?? (object)DBNull.Value;
            parameters[11].Value = ClosedStatusFlag != null ? (object)ClosedStatusFlag : DBNull.Value;
            parameters[12].Value = dbname;

          //  DataSet dsResult = RunStoredProcedure("Usp_GetTicketListExcel", dbname, parameters, "MyDataTable"); //Usp_GetTicketDetailStatus
            DataSet dsResult = RunStoredProcedure("Usp_GetTicketDetailStatus", dbname, parameters, "MyDataTable");
            return dsResult;
        }


        public DataTable GetTicketDocuments(int iCompanyID, string sSerialKey, string sRetriveType, string dbname)
        {
            string strSql = string.Empty;
            strSql = @"select t.TicketNo, t.CreatedDT as tktdate, z.customer_Name,b.brand_name,m.model_name,
		                  p.product_name,s.service_type_name,pb.problem_name,t.Assigned_to,t.Remarks 
                   From customerbranch c,brands b,models m, products p,service_type s,problems pb,
	             tickets t,customers z  where t.CustomerID=c.customer_branch_id and t.brand_id=b.brand_id  and t.model_id=m.model_id and t.product_id=p.product_id 
                and t.ServiceTypeID=s.service_typeid  and t.ProblemID=pb.problem_id  and t.Active=1 and c.Active=1 and b.Active=1 and m.Active=1 and p.Active=1 and z.customer_ID=c.customer_id";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public DataTable GetTicketpartdetails(int TicketID, string dbname)
        {
            string strSql = string.Empty;
            strSql = @" select tp.OrderNo,tp.old_ref1,tp.new_ref1,tp.old_ref2,tp.new_ref2,tp.remarks from tickets t, tickets_partreplacement tp 
                     where t.TicketID = tp.tickets_id and tp.tickets_id =" + TicketID + "";

            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public DataTable GetTicketStatusDetails(int TicketID, string dbname)
        {

            string strSql = string.Empty;
            strSql = @"select  ts.status_id,ts.engineer_id,e.engineer_name,ts.claimamount,ts.remarks,(ts.starttime)as starttime, (ts.endtime) as endtime,ts.tothrs, s.StatusCode as NewStatus
            from tickets_status ts,Status s,engineers e where s.StatusID=ts.status_id and ts.engineer_id=e.engineer_id  and e.Active=1   and s.active=1 and ts.tickets_id =" + TicketID + "";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public DataTable GetStatusDetails(string dbname) //status 
        {

            string strSql = string.Empty;
            strSql = @"SELECT StatusName as Status FROM Status WHERE Active =1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public DataTable GetTicketDocumentDetails(int TicketID, string dbname)
        {
            string strSql = string.Empty;
            strSql = @"select document_name,document_path,remarks from tickets_documents   where  tickets_id=" + TicketID;
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public DataTable GetBrandlist(string dbname)
        {
            string strSql = @"select brand_id,brand_name from brands WHERE Active=1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }

        public DataTable GetBranchdetails(int branchID, string dbname)
        {
            string strSql = @"select concat(address_line_1,address_line_2) as Address from customerbranch where Active=1 and customer_branch_id=" + branchID;
            MySqlParameter[] parameters = { new MySqlParameter("@CustomerID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }

        public DataTable GetCustomerdetails(int CustomerID, string dbname)
        {
            string strSql = @"SELECT customer_branch_id as Value,customer_branch_Name as Name FROM customerbranch where Active=1 and customer_id=" + CustomerID;
            MySqlParameter[] parameters = { new MySqlParameter("@CustomerID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public DataTable GetEngineerlist(string dbname)
        {
            string strSql = @"select engineer_id,engineer_name from engineers WHERE Active=1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }

        public DataTable GettMilestonebyEngineer(string dbname, string engid, string FDate, string TDate)
        {
            string strSql = @"SELECT  e.engineer_code,e.mobileno,e.emailid,
                            case when m.tickets_id>0 then Concat('[ ',cs.customer_branch_code,' - ',tk.TicketNo,' ]') else 'NA' end as jobdetails,
                            case when m.status_id=1000 then 'Check-IN'  when m.status_id=2000 then 'Check-OUT'  else SUBSTRING_INDEX(s.StatusName,'_',-1) end as MilestoneName,
                            date_format(starttime,'%b %d %Y %T') as MilestoneTime,starttime  FROM engineers e
                            left join tickets_status m on m.engineer_id=e.engineer_id and e.Active=1 
                            and date(m.starttime) between '" + FDate + "' and '" + TDate + "' "
                            + " left join  status s on m.status_id = s.StatusID and s.Active=1 "
                            + " left join tickets tk on  m.tickets_id=tk.TicketID  and tk.Active=1 "
                            + " left join customerbranch cs on tk.CustomerID=cs.customer_branch_id and cs.Active=1 "
                            + " where e.engineer_id='" + engid + "' order by m.starttime";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public DataTable GetModellist(string dbname)
        {
            string strSql = @"select model_id,model_name from models WHERE Active=1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public DataTable GetProductlist(string dbname)
        {
            string strSql = @"select product_id,product_name from products WHERE Active=1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public DataTable GetCustomerlist(string dbname)
        {
            //  string strSql = @"select customer_branch_id,customer_branch_Name,address_line_1,address_line_2,phone_no from customerbranch WHERE Active=1";
            string strSql = @"SELECT customer_ID,customer_Name FROM customers where Active=1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public DataTable GetStatuslist(string dbname)
        {
            string strSql = @"select StatusID,StatusName from Status where Active = 1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public DataTable GetSTlist(string dbname)
        {
            string strSql = @"select service_typeid,service_type_name from service_type where Active = 1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public DataTable GetProblemlist(string dbname)
        {
            string strSql = @"select problem_id,problem_name from problems where Active = 1";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }

        public DataTable GetJobTypeslist(string dbname)
        {
            string strSql = @"select JobTypeId,JobTypes from jobtype where Active = 1;";
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }

        public DataTable GetCurrentStatus(int TicketID, string dbname)
        {
            string strSql = string.Empty;
            if (TicketID == 0)
            {
                strSql = @"select sequenceNo,StatusID,StatusCode from Status where Active=1 and sequenceNo=(select Min(sequenceNo) from Status  where Active=1)";
            }
            else
            {
                strSql = @"select sequenceNo,StatusID,StatusCode from Status where Active=1 and StatusID=(select StatusID from tickets where Active=1 and TicketId=" + TicketID + ")";
            }
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public DataTable GetReportDate(int TicketID, string dbname)
        {
            string strSql = string.Empty;
            if (TicketID == 0)
            {
                strSql = @"select now() as ReportDt,now() as CallRecivedAt";
            }
            else
            {
                strSql = @"SELECT ReportDt,CallRecivedAt FROM tickets where TicketId='" + TicketID + "';";
            }
            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
            parameters[0].Value = 1;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            return dsResult.Tables[0];
        }
        public MySqlDataReader GetTicket(int CompanyID, int TicketID, string dbname)
        {
            //            string strSql = @" select  t.TicketID, t.TicketNo,t.TicketDt, c.customer_name,c.address_line_1,c.address_line_2,c.phone_no,b.brand_name,m.model_name,
            //                        p.product_name,s.service_type_name,pb.problem_name from tickets t,customerbranch c,brands b,models m, products p,service_type s,problems pb
            //                        where t.CustomerID=c.customer_branch_id and t.brand_id=b.brand_id and t.model_id=m.model_id and t.product_id=p.product_id and t.ServiceTypeID=s.service_typeid 
            //                        and t.ProblemID=pb.problem_id and t.TicketID=" + TicketID + " and t.Active=1";
            //            string strSql = @"  select cm.CompanyID,cm.CompanyCode,cm.CompanyName,cm.AddressLine1,cm.AddressLine2,cm.City,cm.State,cm.Country,cm.PostalCode,cm.Logo2 as Logo,
            // t.TicketID, t.TicketNo,  t.CustomerID, t.product_id, t.brand_id, t.model_id,  t.ServiceTypeID, t.ProblemID, 
            // t.StatusID, t.Assigned_to, DATE_FORMAT(t.CreatedDT,'%d/%m/%Y') as TicketDate ,t.ReportDt,t.PartNo,t.CustomerAddress,t.ServiceLocation,pv.prName,sv.sevName,pv.prID,sv.sevID,
            //t.CustomerAddress,t.ServiceLocation,st.StatusCode as CurrentStatus,t.OtherClaim,
            // ifnull(t.Remarks,'') as Remarks,ifnull(t.invoice_no,'') as invoice_no,t.invoice_amt ,t.Receipt_amt,t.SerialNumber,t.invoice_dt,ifnull(eg.engineer_code,'NA') as engineer_code 
            //,c.customer_name,b.brand_name,m.model_name,p.product_name,s.service_type_name,pb.problem_name
            // From customerbranch c,brands b,models m, products p,service_type s,problems pb,company cm,severity sv,priority pv,
            //  tickets t left join Engineers eg  on t.Assigned_to=eg.engineer_id and eg.Active=1,Status st where 
            //	 t.Active=1 and st.StatusID = t.StatusID and st.Active = 1 and   t.CustomerID=c.customer_branch_id and t.brand_id=b.brand_id  and t.model_id=m.model_id and t.product_id=p.product_id and t.ServiceTypeID=s.service_typeid 
            //    and t.ProblemID=pb.problem_id and c.Active=1 and b.Active=1 and m.Active=1 and p.Active=1 and s.Active=1 and pb.Active=1 and  t.prID=pv.prID and t.sevID=sv.sevID and pv.Active=1 and sv.Active=1 and t.ticketid=" + TicketID + " and cm.companyid=" + CompanyID + " and cm.CompanyID=t.CompanyID";


            string strSql = @"  select t.Call_Detail_Nature, t.JobTypeId, t.CallRecivedAt,t.NameOfCaller, c.customer_branch_Name, t.BranchID, cm.CompanyID,cm.CompanyCode,cm.CompanyName,cm.AddressLine1,cm.AddressLine2,cm.City,cm.State,cm.Country,cm.PostalCode,cm.PhoneNumber,cm.Logo2 as Logo,
             t.TicketID, t.TicketNo,  t.CustomerID, t.product_id, t.brand_id, t.model_id,  t.ServiceTypeID, t.ProblemID, 
             t.StatusID, t.Assigned_to, DATE_FORMAT(t.CreatedDT,'%d/%m/%Y') as TicketDate ,IFNULL(DATE_FORMAT( t.CreatedDT,'%h:%i%p'),'') as TicketTime,t.ReportDt,t.PartNo,t.CustomerAddress,t.ServiceLocation,pv.prName,sv.sevName,pv.prID,sv.sevID,
            c.contact_person,c.phone_no,st.StatusCode as CurrentStatus,t.OtherClaim,
             ifnull(t.Remarks,'') as Remarks,ifnull(t.invoice_no,'') as invoice_no,t.invoice_amt ,t.Receipt_amt,t.SerialNumber,t.invoice_dt,ifnull(eg.engineer_code,'NA') as engineer_code 
            ,z.customer_Name,b.brand_name,m.model_name,p.product_name,s.service_type_name,pb.problem_name
             From brands b,models m, products p,service_type s,problems pb,company cm,severity sv,priority pv,
              tickets t left join Engineers eg  on t.Assigned_to=eg.engineer_id and eg.Active=1,Status st ,customers z,customerbranch c where 
            	 t.Active=1 and st.StatusID = t.StatusID and st.Active = 1 and   t.CustomerID=c.customer_id and t.brand_id=b.brand_id  and t.model_id=m.model_id and t.product_id=p.product_id and t.ServiceTypeID=s.service_typeid 
                and t.ProblemID=pb.problem_id and c.Active=1 and b.Active=1 and m.Active=1 and p.Active=1 and s.Active=1 and pb.Active=1 and  t.prID=pv.prID and t.sevID=sv.sevID and pv.Active=1 and z.customer_ID=c.customer_id and c.customer_branch_id=t.BranchID and sv.Active=1 and t.ticketid=" + TicketID + " and cm.companyid=" + CompanyID + " and cm.CompanyID=t.CompanyID";


            //            //--Added mano-- 22-03-2022//

            //            string strSql = @" select c.customer_branch_Name, t.BranchID, cm.CompanyID,cm.CompanyCode,cm.CompanyName,cm.AddressLine1,cm.AddressLine2,cm.City,cm.State,cm.Country,cm.PostalCode,cm.PhoneNumber,cm.Logo2 as Logo,
            // t.TicketID, t.TicketNo,  t.CustomerID, t.product_id, t.brand_id, t.model_id,  t.ServiceTypeID, t.ProblemID,t.JobTypeId, 
            // t.StatusID, t.Assigned_to, DATE_FORMAT(t.CreatedDT, '%d/%m/%Y') as TicketDate ,IFNULL(DATE_FORMAT(t.CreatedDT, '%h:%i%p'), '') as TicketTime,t.ReportDt, t.CallRecivedAt,t.PartNo,t.CustomerAddress,t.ServiceLocation,pv.prName,sv.sevName,pv.prID,sv.sevID,
            //c.contact_person,c.phone_no,st.StatusCode as CurrentStatus,t.OtherClaim,
            // ifnull(t.Remarks, '') as Remarks,ifnull(t.invoice_no, '') as invoice_no,t.invoice_amt ,t.Receipt_amt,t.SerialNumber,t.NameOfCaller,t.Call_Detail_Nature, t.invoice_dt,ifnull(eg.engineer_code, 'NA') as engineer_code
            //,z.customer_Name,b.brand_name,m.model_name,p.product_name,s.service_type_name,pb.problem_name,jb.JobTypes
            // From brands b, models m, products p, service_type s,problems pb,jobtype jb,company cm,severity sv, priority pv,
            //  tickets t left join Engineers eg  on t.Assigned_to = eg.engineer_id and eg.Active = 1,Status st, customers z,customerbranch c where

            //     t.Active = 1 and st.StatusID = t.StatusID and st.Active = 1 and t.CustomerID = c.customer_id and t.brand_id = b.brand_id  and t.model_id = m.model_id and t.product_id = p.product_id and t.ServiceTypeID = s.service_typeid
            //    and t.ProblemID = pb.problem_id and t.JobTypeId =jb.JobTypeId  and c.Active = 1 and b.Active = 1 and m.Active = 1 and p.Active = 1 and s.Active = 1 and pb.Active = 1 and jb.Active = 1 and t.prID = pv.prID and t.sevID = sv.sevID and pv.Active = 1 and z.customer_ID = c.customer_id and c.customer_branch_id = t.BranchID and sv.Active = 1 and t.ticketid = " + TicketID + " and cm.companyid = " + CompanyID + " and cm.CompanyID = t.CompanyID";

            //            //--end---//

            return RunProcedureWithOutParameter(strSql, dbname);
        }
        public int InsertOrUpdateRecord(BETicket br, string dbname)
        {
            int iResult = 0;
            int ticket_headerid = 0;
            string TicketNo = string.Empty;
            using (MySqlConnection conn = CreateConnection(GetConnectionString(dbname)))
            {
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            if (!string.IsNullOrEmpty(br.TicketID.ToString()) && br.TicketID != 0)
                            {
                                cmd.CommandText = "Delete From tickets_status Where tickets_id = @TicketID; Delete From tickets_partreplacement where tickets_id = @TicketID; ";

                                cmd.Parameters.Add(AddTypedParam("@TicketID", MySqlDbType.Int64, br.TicketID));

                                cmd.ExecuteNonQuery();

                                cmd.Parameters.Clear();

                                TicketNo = br.TicketNo;

                            }
                            else
                            {
                                DataSet ds = new DataSet();
                                cmd.CommandText = "usp_DocumentNumbers";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add(AddTypedParam("CompID", MySqlDbType.Int32, 1));
                                cmd.Parameters.Add(AddTypedParam("SerKey", MySqlDbType.VarChar, "JOB"));
                                cmd.Parameters.Add(AddTypedParam("RetriveType", MySqlDbType.VarChar, "G"));
                                var adapter = new MySqlDataAdapter(cmd);
                                adapter.Fill(ds);
                                cmd.Parameters.Clear();
                                cmd.CommandText = string.Empty;
                                cmd.CommandType = CommandType.Text;
                                TicketNo = ds.Tables[1].Rows[0][0].ToString();
                            }
                            if (br.TicketID == 0)
                            {
                                //                                cmd.CommandText = @" INSERT INTO tickets(CompanyID,TicketNo,TicketDt,CustomerID,BranchID,product_id,brand_id,model_id,
                                //ServiceTypeID,ProblemID,StatusID,CreatedBy,Assigned_to,Remarks,invoice_no,invoice_amt,invoice_dt,tothrs_spent,Receipt_amt,SerialNumber,ReportDt,PartNo,CustomerAddress,ServiceLocation,prID,sevID,OtherClaim)
                                //                                             VALUES (1,@TicketNo, @TicketDt,@CustomerID,@BranchID,@ProductID,@BrandID,@ModelID,@ServiceTypeID,@ProblemID,@StatusID,
                                //@CreatedBy,@Assigned_to,@Remarks,@invoice_no,@invoice_amt,@invoice_dt,@tothrs_spent,@Receipt_amt,@SerialNumber,@ReportDate,@PartNo,@CustomerAddress,@ServiceLocation,@prID,@sevID,@Otherclaimamount) ";

                                //--Added mano-- 22-03-2022//
                                if (dbname == "stss_trackit")
                                {
                                    cmd.CommandText = @" INSERT INTO tickets(CompanyID, TicketNo, TicketDt, CustomerID, BranchID, product_id, brand_id, model_id,
                                        ServiceTypeID, ProblemID,JobTypeId, StatusID, CreatedBy, Assigned_to, Remarks, invoice_no, invoice_amt, invoice_dt, tothrs_spent, Receipt_amt, SerialNumber, CallRecivedAt, NameOfCaller,Call_Detail_Nature, ReportDt, PartNo, CustomerAddress, ServiceLocation, prID, sevID, OtherClaim)
                                             VALUES(1, @TicketNo, @TicketDt, @CustomerID, @BranchID, (SELECT product_id FROM products where Active=1 and product_name='-' LIMIT 1 ), 
                                            (SELECT brand_id FROM brands where Active=1 and brand_name='-' LIMIT 1), (SELECT model_id FROM models where Active=1 and model_name='-' LIMIT 1), 
                                            (SELECT service_typeid FROM service_type where Active=1 and service_type_name='-' LIMIT 1), (SELECT problem_id FROM problems where Active=1 and problem_name='-' LIMIT 1), @JobTypeId, @StatusID,
                                        @CreatedBy, @Assigned_to, @Remarks, @invoice_no, @invoice_amt, @invoice_dt, @tothrs_spent, @Receipt_amt, @SerialNumber, @CallRecivedAt, @NameOfCaller, @Call_Detail_Nature, @ReportDate, @PartNo, @CustomerAddress, @ServiceLocation, 
                                        (SELECT prID FROM priority where Active=1 and prName='-' LIMIT 1), (SELECT sevID FROM severity where Active=1 and sevName='-' LIMIT 1), @Otherclaimamount) ";
                                }
                                else
                                {
                                    cmd.CommandText = @" INSERT INTO tickets(CompanyID, TicketNo, TicketDt, CustomerID, BranchID, product_id, brand_id, model_id,
                                        ServiceTypeID, ProblemID,JobTypeId, StatusID, CreatedBy, Assigned_to, Remarks, invoice_no, invoice_amt, invoice_dt, tothrs_spent, Receipt_amt, SerialNumber, CallRecivedAt, NameOfCaller,Call_Detail_Nature, ReportDt, PartNo, CustomerAddress, ServiceLocation, prID, sevID, OtherClaim)
                                             VALUES(1, @TicketNo, @TicketDt, @CustomerID, @BranchID, @ProductID, @BrandID, @ModelID, @ServiceTypeID, @ProblemID, @JobTypeId, @StatusID,
                                        @CreatedBy, @Assigned_to, @Remarks, @invoice_no, @invoice_amt, @invoice_dt, @tothrs_spent, @Receipt_amt, @SerialNumber, @CallRecivedAt, @NameOfCaller, @Call_Detail_Nature, @ReportDate, @PartNo, @CustomerAddress, @ServiceLocation, @prID, @sevID, @Otherclaimamount) ";
                                }
                                //--end--//
                            }
                            else
                            {
                                //cmd.CommandText = @" UPDATE tickets SET BranchID=@BranchID,CustomerID=@CustomerID,brand_id=@BrandID,model_id=@ModelID,
                                //                                                     product_id=@ProductID,ServiceTypeID=@ServiceTypeID,ProblemID=@ProblemID, ModifiedBy=@ModifiedBy,Assigned_to=@Assigned_to,Remarks=@Remarks,
                                //invoice_no=@invoice_no,invoice_amt=@invoice_amt,invoice_dt=@invoice_dt,tothrs_spent=@tothrs_spent,Receipt_amt=@Receipt_amt,SerialNumber=@SerialNumber,ReportDt=@ReportDate,PartNo=@PartNo,CustomerAddress=@CustomerAddress,ServiceLocation=@ServiceLocation,prID=@prID,sevID=@sevID,OtherClaim=@Otherclaimamount 
                                //WHERE  TicketID = @TicketID ";

                              //  --Added mano-- 22 - 03 - 2022//
                                                                cmd.CommandText = @" UPDATE tickets SET BranchID = @BranchID, CustomerID = @CustomerID, brand_id = @BrandID, model_id = @ModelID,
                                                                                                                     product_id = @ProductID, ServiceTypeID = @ServiceTypeID, ProblemID = @ProblemID,JobTypeId = @JobTypeId, ModifiedBy = @ModifiedBy, Assigned_to = @Assigned_to, Remarks = @Remarks,
                                                                invoice_no = @invoice_no, invoice_amt = @invoice_amt, invoice_dt = @invoice_dt, tothrs_spent = @tothrs_spent, Receipt_amt = @Receipt_amt, SerialNumber = @SerialNumber, Call_Detail_Nature = @Call_Detail_Nature,  CallRecivedAt = @CallRecivedAt, NameOfCaller = @NameOfCaller, ReportDt = @ReportDate, PartNo = @PartNo, CustomerAddress = @CustomerAddress, ServiceLocation = @ServiceLocation, prID = @prID, sevID = @sevID, OtherClaim = @Otherclaimamount
                                WHERE TicketID = @TicketID ";
                                //--end--//
                            }
                            cmd.Parameters.Add(AddTypedParam("@TicketNo", MySqlDbType.VarChar, TicketNo));

                            //ela change - nvarchar to datetime - 24-12-2020
                            //cmd.Parameters.Add(AddTypedParam("@TicketDt", MySqlDbType.VarChar, br.TicketDt));
                            cmd.Parameters.Add(AddTypedParam("@TicketDt", MySqlDbType.DateTime, br.TicketDt.Trim() == string.Empty ? DateTime.Now : Convert.ToDateTime(br.TicketDt)));
                            cmd.Parameters.Add(AddTypedParam("@CustomerID", MySqlDbType.Int32, br.CustomerID));
                            cmd.Parameters.Add(AddTypedParam("@BranchID", MySqlDbType.Int32, br.BranchID));
                            cmd.Parameters.Add(AddTypedParam("@ModifiedBy", MySqlDbType.Int64, br.ModifiedBy));
                            cmd.Parameters.Add(AddTypedParam("@TicketID", MySqlDbType.VarChar, br.TicketID));
                            cmd.Parameters.Add(AddTypedParam("@ProductID", MySqlDbType.Int32, br.ProductID));
                            cmd.Parameters.Add(AddTypedParam("@BrandID", MySqlDbType.Int32, br.BrandID));
                            cmd.Parameters.Add(AddTypedParam("@ModelID", MySqlDbType.Int32, br.ModelID));
                            cmd.Parameters.Add(AddTypedParam("@Remarks", MySqlDbType.VarChar, br.remarks));
                            cmd.Parameters.Add(AddTypedParam("@ServiceTypeID", MySqlDbType.Int32, br.ServiceTypeID));
                            cmd.Parameters.Add(AddTypedParam("@ProblemID", MySqlDbType.Int32, br.ProblemID));

                            cmd.Parameters.Add(AddTypedParam("@JobTypeId", MySqlDbType.Int32, br.JobTypeId));

                            cmd.Parameters.Add(AddTypedParam("@StatusID", MySqlDbType.Int32, br.StatusID));
                            cmd.Parameters.Add(AddTypedParam("@CreatedBy", MySqlDbType.Int32, br.CreatedBy));
                            cmd.Parameters.Add(AddTypedParam("@Assigned_to", MySqlDbType.Int32, br.Assigned_to));
                            cmd.Parameters.Add(AddTypedParam("@invoice_no", MySqlDbType.VarChar, br.invoiceno));
                            cmd.Parameters.Add(AddTypedParam("@invoice_amt", MySqlDbType.Decimal, br.invamt));
                            cmd.Parameters.Add(AddTypedParam("@Otherclaimamount", MySqlDbType.Decimal, br.Otherclaimamount));


                            cmd.Parameters.Add(AddTypedParam("@invoice_dt", MySqlDbType.VarChar, br.invdate));
                            cmd.Parameters.Add(AddTypedParam("@tothrs_spent", MySqlDbType.Decimal, br.tothrsspent)); //Change by noor MySqlDbType.Int to MySqlDbType.Decimal
                            cmd.Parameters.Add(AddTypedParam("@Receipt_amt", MySqlDbType.Decimal, br.recamt));
                            cmd.Parameters.Add(AddTypedParam("@SerialNumber", MySqlDbType.VarChar, br.SerialNumber));

                            //--Added mano-- 22-03-2022//

                            cmd.Parameters.Add(AddTypedParam("@CallRecivedAt", MySqlDbType.DateTime, (String.IsNullOrEmpty(br.CallRecivedAt)?DateTime.Now: Convert.ToDateTime(br.CallRecivedAt))));

                            cmd.Parameters.Add(AddTypedParam("@NameOfCaller", MySqlDbType.VarChar, br.NameOfCaller));

                            cmd.Parameters.Add(AddTypedParam("@Call_Detail_Nature", MySqlDbType.VarChar, br.Call_Detail_Nature));

                            //--end--//

                            //ela change - nvarchar to datetime - 24-12-2020
                            //   cmd.Parameters.Add(AddTypedParam("@ReportDate", MySqlDbType.VarChar, br.ReportDate));
                            cmd.Parameters.Add(AddTypedParam("@ReportDate", MySqlDbType.DateTime, Convert.ToDateTime(br.ReportDate)));
                            cmd.Parameters.Add(AddTypedParam("@PartNo", MySqlDbType.VarChar, br.PartNo));
                            cmd.Parameters.Add(AddTypedParam("@ServiceLocation", MySqlDbType.VarChar, br.ServiceLocation));

                            cmd.Parameters.Add(AddTypedParam("@CustomerAddress", MySqlDbType.VarChar, br.CustomerAddress));
                            cmd.Parameters.Add(AddTypedParam("@prID", MySqlDbType.Int32, br.prID));
                            cmd.Parameters.Add(AddTypedParam("@sevID", MySqlDbType.Int32, br.sevID));

                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            if (br.TicketID == 0)
                            {
                                cmd.CommandText = @" Select TicketID From tickets Where  TicketNo = @TicketNo  and Active=1";

                                cmd.Parameters.Add(AddTypedParam("@TicketNo", MySqlDbType.VarChar, br.TicketNo));

                                ticket_headerid = (int)cmd.ExecuteScalar();

                                cmd.Parameters.Clear();
                            }
                            else
                                ticket_headerid = br.TicketID;

                            for (int i = 0; i < br.partdetails.Count; i++)
                            {
                                cmd.CommandText = @"INSERT INTO tickets_partreplacement(tickets_id,old_ref1,new_ref1,old_ref2,new_ref2,OrderNo,remarks,created_by,created_dt) VALUES
                                                     (@TicketID,@old_ref1,@new_ref1,@old_ref2,@new_ref2,@OrderNo,@remarks,@CreatedBy,NOW())";
                                cmd.Parameters.Add(AddTypedParam("@TicketID", MySqlDbType.VarChar, ticket_headerid));
                                cmd.Parameters.Add(AddTypedParam("@old_ref1", MySqlDbType.VarChar, br.partdetails[i].old_ref1));
                                cmd.Parameters.Add(AddTypedParam("@new_ref1", MySqlDbType.VarChar, br.partdetails[i].new_ref1));
                                cmd.Parameters.Add(AddTypedParam("@old_ref2", MySqlDbType.VarChar, br.partdetails[i].old_ref2));

                                cmd.Parameters.Add(AddTypedParam("@new_ref2", MySqlDbType.VarChar, br.partdetails[i].new_ref2));
                                cmd.Parameters.Add(AddTypedParam("@remarks", MySqlDbType.VarChar, br.partdetails[i].remarks));
                                cmd.Parameters.Add(AddTypedParam("@CreatedBy", MySqlDbType.Int32, br.CreatedBy));
                                cmd.Parameters.Add(AddTypedParam("@OrderNo", MySqlDbType.VarChar, br.partdetails[i].OrderNo));
                                cmd.CommandTimeout = 200;
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();

                            }

                            for (int j = 0; j < br.ticketStatus.Count; j++)
                            {
                                cmd.CommandText = @"INSERT INTO tickets_status(tickets_id,sequenceno,status_id,engineer_id,remarks,starttime,endtime,updated_by,tothrs,claimamount) Values
                                                     (@TicketID,0,@status_id,@Assignedto,@remarks,@starttime,@endtime,@CreatedBy,@tothrs,@ClaimAmount)";
                                cmd.Parameters.Add(AddTypedParam("@TicketID", MySqlDbType.VarChar, ticket_headerid));
                                cmd.Parameters.Add(AddTypedParam("@status_id", MySqlDbType.Int32, br.ticketStatus[j].status_id));
                                cmd.Parameters.Add(AddTypedParam("@Assignedto", MySqlDbType.VarChar, br.ticketStatus[j].Assignedto));
                                cmd.Parameters.Add(AddTypedParam("@remarks", MySqlDbType.VarChar, br.ticketStatus[j].remarks));

                                //ela change - nvarchar to datetime - 24-12-2020
                                //cmd.Parameters.Add(AddTypedParam("@starttime", MySqlDbType.VarChar, br.ticketStatus[j].starttime));
                                //cmd.Parameters.Add(AddTypedParam("@endtime", MySqlDbType.VarChar, br.ticketStatus[j].endtime));

                                cmd.Parameters.Add(AddTypedParam("@starttime", MySqlDbType.DateTime, Convert.ToDateTime(br.ticketStatus[j].starttime)));
                                cmd.Parameters.Add(AddTypedParam("@endtime", MySqlDbType.DateTime, Convert.ToDateTime(br.ticketStatus[j].endtime)));

                                cmd.Parameters.Add(AddTypedParam("@CreatedBy", MySqlDbType.Int32, br.CreatedBy));
                                cmd.Parameters.Add(AddTypedParam("@tothrs", MySqlDbType.Decimal, br.ticketStatus[j].tothrs));
                                cmd.Parameters.Add(AddTypedParam("@ClaimAmount", MySqlDbType.Decimal, br.ticketStatus[j].ClaimAmount));

                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }

                            cmd.CommandText = @" update tickets A,(select tickets_id,status_id From tickets_status where ticketstatusid=(
                                                        select max(ticketstatusid)  From tickets_status where tickets_id=@TicketID)) B
                                                        set A.StatusID=B.status_id where A.TicketID=B.tickets_id";
                            cmd.Parameters.Add(AddTypedParam("@TicketID", MySqlDbType.VarChar, ticket_headerid));
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();


                        }
                        trans.Commit();
                        iResult = ticket_headerid;
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
        //17-09
        public int InsertOrUpdateDoc(BETicket br, string dbname)
        {
            int iResult = 0;
            int ticket_headerid = 0;
            string TicketNo = string.Empty;
            using (MySqlConnection conn = CreateConnection(GetConnectionString(dbname)))
            {
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            if (!string.IsNullOrEmpty(br.TicketID.ToString()) && br.TicketID != 0)
                            {
                                cmd.CommandText = " Delete from tickets_documents where tickets_id=@TicketID; ";

                                cmd.Parameters.Add(AddTypedParam("@TicketID", MySqlDbType.Int64, br.TicketID));

                                cmd.ExecuteNonQuery();

                                cmd.Parameters.Clear();

                                TicketNo = br.TicketNo;

                            }

                            for (int k = 0; k < br.document.Count; k++)
                            {
                                cmd.CommandText = @"INSERT INTO tickets_documents(tickets_id,document_name,document_path,created_by,created_dt) Values
                                                (@TicketID,@DocumentName,@DocumentPath,@CreatedBy,NOW())";
                                cmd.Parameters.Add(AddTypedParam("@TicketID", MySqlDbType.Int32, br.document[k].TicketID));
                                cmd.Parameters.Add(AddTypedParam("@DocumentName", MySqlDbType.VarChar, br.document[k].DocumentName));
                                cmd.Parameters.Add(AddTypedParam("@DocumentPath", MySqlDbType.VarChar, br.document[k].DocumentPath));
                                cmd.Parameters.Add(AddTypedParam("@CreatedBy", MySqlDbType.Int32, br.document[k].CreatedBy));

                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }
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
        public int DeleteTicket(int TicketID, int ModifiedBy, string dbname)
        {
            int m_rowsAffected = 0;
            try
            {
                string strSql = " Update ticket Set Active = 0, ModifiedBy=@ModifiedBy, ModifiedDT=Now() Where TicketID = @TicketID ";


                MySqlParameter[] parameters = {
                                                new MySqlParameter("@TicketID", MySqlDbType.Int64),
                                                new MySqlParameter("@ModifiedBy", MySqlDbType.Int64)};

                parameters[0].Value = TicketID;
                parameters[1].Value = ModifiedBy;

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

            dtResult = RunExecuteScalarSQLIntWithParams(sqlStr, dbname, parameters);
            return dtResult;
        }
        public DataTable LoadDropDownList(string selectedTextName, string selectedValueName, string tableName, string filterCondition, string dbname)
        {
            DataTable dtResult = new DataTable();
            string sqlStr = string.Empty;
            //sqlStr = "Select '' Name, '' Value UNION ALL ";

            sqlStr += " Select " + selectedTextName + " Name, " + selectedValueName + " Value" + " From " + tableName + " where 1=1";
            if (!string.IsNullOrEmpty(filterCondition))
            {
                sqlStr += " And " + filterCondition;
            }
            DataSet dsResult = RunSQLString(sqlStr, dbname, "MyDataTable");

            return dsResult.Tables[0];
        }

        public DataTable LoadDropDownListMultipleTables(int CustomerID, string dbname)
        {
            DataTable dtResult = new DataTable();
            string sqlStr = string.Empty;
            sqlStr = "SELECT address_line_1,address_line_2,phone_no FROM tcc_trackit.customerbranch where customer_branch_id=@CustomerID ";

            MySqlParameter[] parameters = { new MySqlParameter("@CustomerID", MySqlDbType.Int64) };
            //set values
            parameters[0].Value = @CustomerID;

            DataSet dsResult = RunSQLWithParam(sqlStr, dbname, parameters, "MyDataTable");

            return dsResult.Tables[0];
        }
        private string GetConnectionString(string dbname)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["PeritusConnectionString"].ConnectionString);
            builder.Database = dbname;
            return builder.ToString();
        }




        /// push notification get
        /// 

        public DataSet GetPushnotificationDetails(int ticketid,String id, string dbname)
        {
            DataSet DS = new DataSet();
            try
            {
                string strQry = string.Empty;

                //                strQry = @"SELECT TicketID, TicketNo,IFNULL(b.TokenID,'') as TokenID,c.StatusCode,c.PushNotification_Content,(select DISTINCT d.engineer_name  from engineers d where a.CreatedBy=d.engineer_id ) as CreateBY,(select DISTINCT d.engineer_name  from engineers d where a.Assigned_to=d.engineer_id ) as Assigned_to FROM tickets a,engineers b,status c
                //WHERE TicketID = (SELECT Max(TicketID) FROM tickets)  and a.CreatedBy=b.engineer_id and b.engineer_id in('" + id + "') and b.Active=1 and c.StatusID=a.StatusID and c.PushNotification=1 and c.Active=1 ; ";

                if (ticketid == 0)
                {
                    strQry = @"select TicketID, TicketNo,(select DISTINCT IFNULL(d.TokenID,'')  from engineers d where a.Assigned_to=d.engineer_id )  as TokenID,c.StatusCode,c.PushNotification_Content,(select DISTINCT d.UserName  from usersetup d where a.CreatedBy=d.UserID ) as CreateBY,(select DISTINCT d.engineer_name  from engineers d where a.Assigned_to=d.engineer_id ) as Assigned_to FROM tickets a,status c
WHERE TicketID = (SELECT Max(TicketID) FROM tickets)  and a.Assigned_to in('" + id + "') and c.StatusID=a.StatusID and c.PushNotification=1 and c.Active=1 ; ";
                    DS = RunSQLString(strQry, dbname, "Customer table");
                }
                else {
                    strQry = @"select TicketID, TicketNo,(select DISTINCT IFNULL(d.TokenID,'')  from engineers d where a.Assigned_to=d.engineer_id )  as TokenID,c.StatusCode,c.PushNotification_Content,(select DISTINCT d.UserName  from usersetup d where a.CreatedBy=d.UserID ) as CreateBY,(select DISTINCT d.engineer_name  from engineers d where a.Assigned_to=d.engineer_id ) as Assigned_to FROM tickets a,status c
WHERE TicketID = '"+ ticketid + "'  and a.Assigned_to in('" + id + "') and c.StatusID=a.StatusID and c.PushNotification=1 and c.Active=1 ; ";
                    DS = RunSQLString(strQry, dbname, "Customer table");
                }
                return DS;
            }
            catch (Exception e)
            {
                return DS;
            }
        }

      
        public DataTable GetticketemailDetails(int ticketid, string dbname)
        {
            string strSql = string.Empty;

//            strSql = @"SELECT DISTINCT t.TicketID,t.TicketNo as JobNo,t.CreatedDT AS JobDate,IFNULL(jb.JobTypes,'NA') AS JobType, z.customer_Name as Customer,zx.customer_branch_Name,b.brand_name as Brand, m.model_name as Model,t.Remarks,p.product_name as Product,
//    s.service_type_name as ServiceType,pb.problem_name as Problem,IFNULL(eg.engineer_code,'NA') AS engineer_code,st.StatusName AS CurrentStatus,t.tothrs_spent AS tothrs,eg.engineer_id,
//    cy.CompanyCode,t.SerialNumber,t.NameOfCaller,t.Call_Detail_Nature,IFNULL(t.Receipt_amt,0.00) AS Receipt_amt,t.invoice_amt,t.ReportDt,t.CallRecivedAt,
//    t.PartNo as PartNo,IFNULL(eg.engineer_name,'NA') AS Engineer,CONCAT(t.invoice_no,' / ',DATE_FORMAT(STR_TO_DATE(t.invoice_dt, '%Y-%m-%d'),'%d-%m-%Y')) AS Invoice 
//    FROM customerbranch c, brands b,models m,products p,service_type s,problems pb,Status st, tickets t 
//    LEFT JOIN Engineers eg ON t.Assigned_to = eg.engineer_id AND eg.Active = 1 
//    LEFT JOIN jobtype jb ON t.JobTypeId = jb.JobTypeId, customers z, customerbranch zx, company cy 
//    WHERE z.customer_ID = c.customer_id AND zx.customer_branch_id = t.BranchID 
//    AND t.CustomerID = c.customer_id AND t.brand_id = b.brand_id AND t.model_id = m.model_id AND t.product_id = p.product_id 
//    AND t.ServiceTypeID = s.service_typeid AND cy.CompanyID = t.CompanyID AND t.ProblemID = pb.problem_id 
//    AND t.Active = 1 AND c.Active = 1 AND b.Active = 1 AND m.Active = 1 AND p.Active = 1 
//    AND s.Active = 1 AND pb.Active = 1 AND t.StatusID = st.StatusID AND st.Active = 1 and t.TicketID = @TicketID";

             strSql = @"SELECT DISTINCT t.TicketID, t.TicketNo as JobNo, t.CreatedDT AS JobDate, IFNULL(jb.JobTypes,'NA') AS JobType, z.customer_Name as Customer,
 zx.customer_branch_Name, b.brand_name as Brand, m.model_name as Model, t.Remarks, p.product_name as Product, s.service_type_name as ServiceType,
 pb.problem_name as Problem, IFNULL(eg.engineer_code,'NA') AS engineer_code, st.StatusName AS CurrentStatus, t.tothrs_spent AS tothrs, eg.engineer_id,
 cy.CompanyCode, t.SerialNumber, t.NameOfCaller, t.Call_Detail_Nature, IFNULL(t.Receipt_amt, 0.00) AS Receipt_amt, t.invoice_amt, t.ReportDt, 
 t.CallRecivedAt, t.PartNo as PartNo, IFNULL(eg.engineer_name,'NA') AS Engineername, CONCAT(t.invoice_no, ' / ', DATE_FORMAT(STR_TO_DATE(t.invoice_dt, '%Y-%m-%d')
 ,'%d-%m-%Y')) AS Invoice, IFNULL((SELECT e2.engineer_name FROM tickets_status ts JOIN engineers e2 ON ts.engineer_id = e2.engineer_id WHERE ts.tickets_id = t.TicketID  ORDER BY ts.ticketstatusid DESC  LIMIT 1), 'NA') AS Engineer 
 FROM customerbranch c, brands b, models m, products p, service_type s, problems pb, Status st, tickets t LEFT JOIN Engineers eg 
 ON t.Assigned_to = eg.engineer_id AND eg.Active = 1 LEFT JOIN jobtype jb ON t.JobTypeId = jb.JobTypeId, customers z, customerbranch zx, company cy 
 WHERE z.customer_ID = c.customer_id AND zx.customer_branch_id = t.BranchID AND t.CustomerID = c.customer_id AND t.brand_id = b.brand_id AND 
 t.model_id = m.model_id AND t.product_id = p.product_id AND t.ServiceTypeID = s.service_typeid AND cy.CompanyID = t.CompanyID AND 
 t.ProblemID = pb.problem_id AND t.Active = 1 AND c.Active = 1 AND b.Active = 1 AND m.Active = 1 AND p.Active = 1 AND s.Active = 1 AND pb.Active = 1 
 AND t.StatusID = st.StatusID AND st.Active = 1 AND t.TicketID = @TicketID";

            MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) ,
                                          new MySqlParameter("@ticketid", MySqlDbType.Int64)};
            parameters[0].Value = 1;
            parameters[1].Value = ticketid;
            DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
            // DataSet dsResult = RunSQLWithParam(strSql, dbname, null, "MyDataTable");
            return dsResult.Tables[0];
        }



    }
}
