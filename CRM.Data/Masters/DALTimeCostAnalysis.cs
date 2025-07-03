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
    public class DALTimeCostAnalysis : Base.Data.BaseSql
    {
        public DataTable GetReportList(string fromdate, string todate, int ReportType, string dbname)
       {
          
           string strSql = string.Empty;
           strSql = @"select t.TicketID,t.TicketNo, t.CreatedDT as tktdate, c.customer_name,b.brand_name,m.model_name,
                       p.product_name,s.service_type_name,pb.problem_name,ifnull(eg.engineer_code,'NA') as engineer_code,st.StatusName as CurrentStatus,t.tothrs_spent as tothrs,
                       t.SerialNumber,ifnull(t.Receipt_amt,0.00)as Receipt_amt,t.invoice_amt,t.ReportDt,t.PartNo,
		               Concat(t.invoice_no,' / ',date_format(str_to_date(t.invoice_dt, '%Y-%m-%d'),'%d-%m-%Y')) as Invoice From customerbranch c,brands b,models m, products p,service_type s,problems pb,Status st,
                       tickets t left join Engineers eg  on t.Assigned_to=eg.engineer_id and eg.Active=1  where
                       t.CustomerID=c.customer_branch_id and t.brand_id=b.brand_id  and t.model_id=m.model_id and t.product_id=p.product_id and t.ServiceTypeID=s.service_typeid 
                       and t.ProblemID=pb.problem_id and t.Active=1 and c.Active=1 and b.Active=1 and m.Active=1 and p.Active=1 and s.Active=1 and pb.Active=1 and t.StatusID=st.StatusID  and st.Active=1";
           if (!string.IsNullOrEmpty(fromdate) && !string.IsNullOrEmpty(todate))
           {
               strSql = strSql + "  and date(t.CreatedDT) between '" + fromdate + "' and '" + todate + "'";
           }

          

           MySqlParameter[] parameters = { new MySqlParameter("@CompanyID", MySqlDbType.Int64) };
           parameters[0].Value = 1;
           DataSet dsResult = RunSQLWithParam(strSql, dbname, parameters, "MyDataTable");
           return dsResult.Tables[0];
       }
    }
}
