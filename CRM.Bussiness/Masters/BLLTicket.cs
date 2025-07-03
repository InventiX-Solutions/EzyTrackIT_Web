using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.UI;
using System;
using System.Drawing;
using System.IO;

using CRM.Artifacts;
using CRM.Data.Masters;
using CRM.Artifacts.Masters;

using System.Configuration;
using MySql.Data.MySqlClient;

namespace CRM.Bussiness.Masters
{
    public class BLLTicket
    {
        public string GetDocumentNumber(int iCompanyID, string sSerialKey, string sRetriveType, string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.GetDocumentNumber(iCompanyID, sSerialKey, sRetriveType, dbname);
        }

        public DataTable GetTicketList(string fromdate, string todate, int? status, int? customer, int? Branch, int? Product, int? Brand, int? Model, int? Problem, int? Engineer, int? Jobtype, string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.GetTicketList(fromdate, todate, status,customer,Branch,Product,Brand,Model,Problem,Engineer,Jobtype, dbname);
        }
        //public DataTable GetTicketListExcel(string fromdate, string todate, int? status, int? customer, int? Branch, int? Product, int? Brand, int? Model, int? Problem, int? Engineer, int? Jobtype, string dbname)
        //{
        //    DALTicket myDALTicket = new DALTicket();
        // //   return myDALTicket.GetTicketListExcel(fromdate, todate, status, customer, Branch, Product, Brand, Model, Problem, Engineer, Jobtype, dbname);
        //    DataSet dsResult = myDALTicket.GetTicketListExcel(fromdate, todate, status, customer, Branch, Product, Brand, Model, Problem, Engineer, Jobtype, dbname);

        //    // Check if DataSet contains tables
        //    if (dsResult != null && dsResult.Tables.Count > 0)
        //    {
        //        // Assuming you want to return the first table from the DataSet
        //        return dsResult.Tables[0];
        //    }
        //}
        public DataTable GetTicketListExcel(string fromdate, string todate, int? status, int? customer, int? Branch, int? Product, int? Brand, int? Model, int? Problem, int? Engineer, int? Jobtype, bool ClosedStatusFlag, string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            DataSet dsResult = myDALTicket.GetTicketListExcel(fromdate, todate, status, customer, Branch, Product, Brand, Model, Problem, Engineer, Jobtype, ClosedStatusFlag, dbname);

            // Check if DataSet contains tables
            if (dsResult != null && dsResult.Tables.Count > 0)
            {
                // Assuming you want to return the first table from the DataSet
                return dsResult.Tables[0];
            }
            else
            {
                // Handle the case where DataSet is null or does not contain any tables
                return null; // or any other appropriate default value
            }
        }
        //public DataTable GetTicketListsp(string fromdate, string todate, int? status, int? customer, int? Branch, int? Product, int? Brand, int? Model, int? Problem, int? Engineer, int? Jobtype, string dbname)
        //{
        //    DALTicket myDALTicket = new DALTicket();
        //    return myDALTicket.GetTicketListsp(fromdate, todate, status, customer, Branch, Product, Brand, Model, Problem, Engineer, Jobtype, dbname);
        //}
        public DataTable GettMilestonebyEngineer(string dbname, string engid, string FDate, string TDate)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.GettMilestonebyEngineer(dbname, engid, FDate, TDate);
        }
        public DataTable GetModellist(string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.GetModellist(dbname);
        }
        public DataTable GetCustomerdetails(int CustomerID, string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.GetCustomerdetails(CustomerID, dbname);
        }
        public DataTable GetBranchdetails(int branchID, string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.GetBranchdetails(branchID, dbname);
        }

        public DataTable GetBrandlist(string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.GetBrandlist(dbname);
        }

        public DataTable GetEngineerlist(string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.GetEngineerlist(dbname);
        }
        public DataTable GetStatuslist(string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.GetStatuslist(dbname);
        }
        public DataTable GetProductlist(string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.GetProductlist(dbname);
        }
        public DataTable GetCustomerlist(string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.GetCustomerlist(dbname);
        }
        public DataTable GetSTlist(string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.GetSTlist(dbname);
        }
        public DataTable GetProblemlist(string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.GetProblemlist(dbname);
        }

        public DataTable GetJobTypeslist(string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.GetJobTypeslist(dbname);
        }



        public DataTable GetCurrentStatus(int TicketID, string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.GetCurrentStatus(TicketID, dbname);
        }
        public DataTable GetReportDate(int TicketID, string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.GetReportDate(TicketID, dbname);
        }

        public BETicket GetTicket(int CompanyID, int TicketID, string dbname)
        {
            BETicket beTicket = new BETicket();

            MySqlDataReader mySqlDataReader;

            DALTicket myDALTicket = new DALTicket();
            mySqlDataReader = myDALTicket.GetTicket(CompanyID, TicketID, dbname);

            while (mySqlDataReader.Read())
            {

                beTicket.TicketID = Convert.ToInt32(mySqlDataReader["TicketID"].ToString());
                beTicket.Date = mySqlDataReader["TicketDate"].ToString();
                beTicket.TicketTime = mySqlDataReader["TicketTime"].ToString();

                beTicket.TicketNo = mySqlDataReader["TicketNo"].ToString();
                beTicket.CustomerID = Convert.ToInt32(mySqlDataReader["CustomerID"].ToString());
                beTicket.CustomerName = mySqlDataReader["customer_name"].ToString();
                beTicket.BranchName = mySqlDataReader["customer_branch_Name"].ToString();
                beTicket.BranchID = Convert.ToInt32(mySqlDataReader["BranchID"].ToString());
                beTicket.BrandID = Convert.ToInt32(mySqlDataReader["brand_id"].ToString());
                beTicket.BrandName = mySqlDataReader["brand_name"].ToString();
                beTicket.ModelID = Convert.ToInt32(mySqlDataReader["model_id"].ToString());
                beTicket.ModelName = mySqlDataReader["model_name"].ToString();
                beTicket.ProductID = Convert.ToInt32(mySqlDataReader["product_id"].ToString());
                beTicket.ProductName = mySqlDataReader["product_name"].ToString();
                beTicket.ReportDate = mySqlDataReader["ReportDt"].ToString();
                beTicket.CustomerAddress = mySqlDataReader["CustomerAddress"].ToString();
                beTicket.ContactPerson = mySqlDataReader["contact_person"].ToString();
                beTicket.PhoneNo = mySqlDataReader["phone_no"].ToString();
                beTicket.ServiceLocation = mySqlDataReader["ServiceLocation"].ToString();
                beTicket.PartNo = mySqlDataReader["PartNo"].ToString();
                beTicket.sevName = (mySqlDataReader["sevName"].ToString());
                beTicket.prName = (mySqlDataReader["prName"].ToString());

                beTicket.sevID = Convert.ToInt32(mySqlDataReader["sevID"].ToString());
                beTicket.prID = Convert.ToInt32(mySqlDataReader["prID"].ToString());


                beTicket.StatusID = Convert.ToInt32(mySqlDataReader["StatusID"].ToString());
                beTicket.Status = mySqlDataReader["CurrentStatus"].ToString();
                beTicket.ProblemID = Convert.ToInt32(mySqlDataReader["ProblemID"].ToString());
                beTicket.NatureOfProblem = mySqlDataReader["problem_name"].ToString();
             
               

                beTicket.ServiceTypeID = Convert.ToInt32(mySqlDataReader["ServiceTypeID"].ToString());
                beTicket.ServiceType = mySqlDataReader["service_type_name"].ToString();
                beTicket.EngineerID = Convert.ToInt32(mySqlDataReader["Assigned_to"].ToString());
                beTicket.EngineerName = mySqlDataReader["engineer_code"].ToString();
                beTicket.remarks = mySqlDataReader["Remarks"].ToString();

                beTicket.document = GetTicketDocuments(TicketID, dbname);
                beTicket.partdetails = partdetails(TicketID, dbname);
                beTicket.ticketStatus = ticketStatus(TicketID, dbname);
                beTicket.invamt = Convert.ToDecimal(mySqlDataReader["invoice_amt"].ToString());
                beTicket.invoiceno = mySqlDataReader["invoice_no"].ToString();
                beTicket.recamt = Convert.ToDecimal(mySqlDataReader["Receipt_amt"].ToString());
                beTicket.Otherclaimamount = Convert.ToDecimal(mySqlDataReader["OtherClaim"].ToString());
                beTicket.SerialNumber = mySqlDataReader["SerialNumber"].ToString();
                //--Added mano-- 22-03-2022//
                beTicket.CallRecivedAt = mySqlDataReader["CallRecivedAt"].ToString();
                beTicket.NameOfCaller = mySqlDataReader["NameOfCaller"].ToString();
                if (mySqlDataReader["JobTypeId"] != DBNull.Value)
                {
                    beTicket.JobTypeId = Convert.ToInt32(mySqlDataReader["JobTypeId"].ToString());
                }
                beTicket.Call_Detail_Nature = mySqlDataReader["Call_Detail_Nature"].ToString();
                //---end--//
                beTicket.companyname = mySqlDataReader["CompanyName"].ToString();
                beTicket.companylogo = mySqlDataReader["Logo"].ToString();
                beTicket.address1 = mySqlDataReader["AddressLine1"].ToString();
                beTicket.address2 = mySqlDataReader["AddressLine2"].ToString();
                beTicket.city = mySqlDataReader["City"].ToString();
                beTicket.state = mySqlDataReader["State"].ToString();
                beTicket.country = mySqlDataReader["Country"].ToString();
                beTicket.PostalCode = mySqlDataReader["PostalCode"].ToString();
                beTicket.PhoneNumber = mySqlDataReader["PhoneNumber"].ToString();
                if (!string.IsNullOrEmpty(mySqlDataReader["invoice_dt"].ToString()))
                {
                    string dt = Convert.ToDateTime(mySqlDataReader["invoice_dt"].ToString()).ToString("dd/MM/yyyy");
                    beTicket.invdate = dt;
                }
            }
            mySqlDataReader.Close();
            return beTicket;
        }
        private List<BETicketPartDetails> partdetails(int TicketID, string dbname)//this one
        {
            DALTicket myDALTicket = new DALTicket();
            List<BETicketPartDetails> bepartdetailsList = new List<BETicketPartDetails>();
            DataTable dtpartdetails = myDALTicket.GetTicketpartdetails(TicketID, dbname);

            //    MySqlDataReader mySqlDataReader;
            //    return myDALTicket.GetTicketpartdetails(TicketID);
            foreach (DataRow dRow in dtpartdetails.Rows)
            {
                BETicketPartDetails bePartdetails = new BETicketPartDetails();

                bePartdetails.OrderNo = dRow["OrderNo"].ToString();
                bePartdetails.old_ref1 = dRow["old_ref1"].ToString();
                bePartdetails.new_ref1 = dRow["new_ref1"].ToString();
                bePartdetails.old_ref2 = dRow["old_ref2"].ToString();
                bePartdetails.new_ref2 = dRow["new_ref2"].ToString();
                bePartdetails.remarks = dRow["remarks"].ToString();
                bepartdetailsList.Add(bePartdetails);
            }
            //myDALTicket = null;
            //dtpartdetails = null;
            return bepartdetailsList;
        }
        private List<BETicketStatus> ticketStatus(int TicketID, string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            List<BETicketStatus> bestatusList = new List<BETicketStatus>();
            DataTable dtstatus = myDALTicket.GetTicketStatusDetails(TicketID, dbname);
            //   MySqlDataReader mySqlDataReader;
            foreach (DataRow dRow in dtstatus.Rows)
            {
                BETicketStatus beStatus = new BETicketStatus();
                //beStatus.Currentstatus = dRow["Currentstatus"].ToString();
                beStatus.NewStatus = dRow["NewStatus"].ToString();
                beStatus.status_id = Convert.ToInt32(dRow["status_id"].ToString());
                beStatus.Assignedtoname = dRow["engineer_name"].ToString();
                beStatus.Assignedto = Convert.ToInt32(dRow["engineer_id"].ToString());
                beStatus.starttime = dRow["starttime"].ToString();
                beStatus.endtime = dRow["endtime"].ToString();
                beStatus.tothrs = Convert.ToDecimal(dRow["tothrs"].ToString());
                beStatus.ClaimAmount = dRow["claimamount"].ToString();
                beStatus.remarks = dRow["remarks"].ToString();
                bestatusList.Add(beStatus);
            }
            //myDALTicket = null;
            //dtstatus = null;
            return bestatusList;
        }
        private List<BETicketDocument> GetTicketDocuments(int TicketID, string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            List<BETicketDocument> beDocumentList = new List<BETicketDocument>();
            DataTable dtdocument = myDALTicket.GetTicketDocumentDetails(TicketID, dbname);
            foreach (DataRow dRow in dtdocument.Rows)
            {
                BETicketDocument beDocument = new BETicketDocument();
                beDocument.DocfullName = dRow["document_name"].ToString();
                beDocument.remarks = dRow["remarks"].ToString();


                string[] docs = beDocument.DocfullName.Split('.');
                beDocument.DocumentName = docs[0].ToString();
                beDocument.DocumentPath = dRow["document_path"].ToString();
                beDocumentList.Add(beDocument);
            }
            //myDALTicket = null;
            //dtdocument = null;
            return beDocumentList;
        }
        public DataTable GetDropDownValues(string selectedTextName, string selectedValueName, string tableName, string filterCondition, string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.LoadDropDownList(selectedTextName, selectedValueName, tableName, filterCondition, dbname);
        }
        public DataTable GeMultipletDropDownValues(int CustomerID, string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.LoadDropDownListMultipleTables(CustomerID, dbname);
        }
        public int GetDuplicateExists(int companyID, string tableName, string columnName, string codeORName, string idcolumnname, int id, string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.CheckDuplicateExists(companyID, tableName, columnName, codeORName, idcolumnname, id, dbname);
        }
        public int InsertOrUpdateRecord(BETicket br, string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.InsertOrUpdateRecord(br, dbname);
        }
        public int InsertOrUpdateDoc(BETicket br, string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.InsertOrUpdateDoc(br, dbname);
        }
        public int DeleteTicket(int TicketID, int ModifiedBy, string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.DeleteTicket(TicketID, ModifiedBy, dbname);
        }

        public DataTable GetSeverelist(string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.GetSeverelist(dbname);
        }
        public DataTable GetPrioritylist(string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.GetPrioritylist(dbname);
        }

        /// push notification get
        /// 

        public DataSet GetPushnotificationDetails(int ticketid, String id, string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.GetPushnotificationDetails(ticketid, id, dbname);
        }

        public DataTable GetticketemailDetails(int ticketid,string dbname)
        {
            DALTicket myDALTicket = new DALTicket();
            return myDALTicket.GetticketemailDetails( ticketid, dbname);
        }
       

       
    }
}

