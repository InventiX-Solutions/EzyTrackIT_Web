using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Artifacts;
using CRM.Data;
using System.Data;
using MySql.Data.MySqlClient;

namespace CRM.Bussiness
{
    public class BLLCommon
    {
        public int UpdateLog(BECommom BeCommon, string dbname)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.UpdateLog(BeCommon,dbname);
        }
        public DataTable GetStatusData(string ReportType,string StartTime,string FinishTime,int CustomerID,int BranchID,int ProductID,int BrandID,int ModelID,int ProblemID,int EngineerID,int jobtypeID, string dbname)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.GetStatusData(ReportType, StartTime, FinishTime, CustomerID, BranchID, ProductID, BrandID, ModelID, ProblemID, EngineerID, jobtypeID, dbname);
        }
        public DataTable GetStatusdashboardData( string dbname)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.GetStatusDashboardData( dbname);
        }
        public DataTable GetExpectedStatusValuesAsDataTable(string dbname)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.GetExpectedStatusValuesAsDataTable(dbname);
        }
        public DataTable GetDropDownList(int companyID, string selectedTextName, string selectedValueName, string tableName, string filterCondition, string dbname)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.LoadDropDownListActive(companyID, selectedTextName, selectedValueName, tableName, filterCondition,dbname);
        }
        public DataTable GetAttendanceList(string StartTime, string FinishTime, string userid, string dbname)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.GetAttendanceList(StartTime, FinishTime, userid, dbname);
        }
        public DataTable GetClosedJobMapList(string StartTime, string FinishTime, string userid, string dbname)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.GetClosedJobMapList(StartTime, FinishTime, userid, dbname);
        }
        public DataTable GetEngineerDetails(string dbname)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.GetEngineerDetails(dbname);
        }
        public MySqlDataReader GetMap(string User, string dbname)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.GetMap(User, dbname);
        }
        public DataTable GetCustomerTimeAnalysisList(string StartTime, string EndTime, string engineerid, string customerid, string dbname)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.GetCustomerTimeAnalysisList(StartTime, EndTime, engineerid, customerid, dbname);
        }

        public DataTable getcompileddata(DataTable dt, string dbname)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.getcompileddata(dt, dbname);
        }
        public DataTable gett_emp_customer_compileddata(DataTable dt, string dbname)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.gett_emp_customer_compileddata(dt, dbname);
        }
        public DataTable GetMastersDetailswithTable(string mastertblname, string masteridname, int masteridvalue, string mastercolumns, int companyID, string dbname)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.GetMastersDetailswithTable(mastertblname, masteridname, masteridvalue, mastercolumns, companyID, dbname);
        }
        public int UpdateMasters(string tablename, string updatedvalues, string conditions, int companyID, string dbname, int isDefault = 0, string addconditions = "")
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.UpdateMasters(tablename, updatedvalues, conditions, companyID, dbname, isDefault, addconditions);
        }
        public string VerifyCodes(int compnayID, string colname, string colvalue, string tables, string dbname, string sCondition)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.VerifyCodes(compnayID, colname, colvalue, tables, sCondition, dbname);
        }

        public DataTable getDefaultList(string dbname, string companyID, string module)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.getDefaultList(companyID, module, dbname);
        }
        public int DeleteMasters(string mastertblname, string masteridname, int masteridvalue, int companyID, int modifiedby, string dbname)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.DeleteMasters(mastertblname, masteridname, masteridvalue, companyID, modifiedby, dbname);
        }

        public DataTable ReturnName(string sSelectedTextName, string sTableName, string sFilterCondition, string dbname)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.ReturnName(sSelectedTextName, sTableName, sFilterCondition, dbname);
        }

        public DataTable GetSupervisorEmailIDs(string dbname)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.GetSupervisorEmailIDs(dbname);
        }

        public DataTable GetJobEngineesEmailIDs(int TicketID, string dbname)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.GetJobEngineesEmailIDs(TicketID, dbname);
        }

        public DataTable GetNotificationTemplateDetails(int TemplateTypeID, string dbname)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.GetNotificationTemplateDetails(TemplateTypeID, dbname);
        }

        public DataTable GetOutstandingJobListData(int companyID, string dbname)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.GetOutstandingJobListData(companyID, dbname);
        }

        public DataTable GetVehicleMileageList(string StartTime, string EndTime, string engineerid, string vehicleid, string dbname)
        {
            DALCommon myDALCommon = new DALCommon();
            return myDALCommon.GetVehicleMileageList(StartTime, EndTime, engineerid, vehicleid, dbname);
        }
    }
}
