using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Artifacts.Masters
{
     [Serializable]
   public class BETicket
    {
        public int CompanyID { get; set; }
        public string companylogo { get; set; }
        public string companyname { get; set; }
        public string barcodeimage { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public int TicketID { get; set; }
        public string TicketDt { get; set; }
        public string TicketTime { get; set; }
        public int BrandID { get; set; }
        public int ModelID { get; set; }
        public int CustomerID { get; set; }
        public int BranchID { get; set; }
        public int ProductID { get; set; }
        public int ProblemID { get; set; }

        public int JobTypeId { get; set; }

        public int ServiceTypeID { get; set; }
        public int StatusID { get; set; }

        public int EngineerID { get; set; }
        public string EngineerName { get; set; }
        public int Assigned_to { get; set; }
        public string TicketNo { get; set; }
        public string remarks { get; set; }
        public string SerialNumber { get; set; }

        //--Added mano--//

        public string CallRecivedAt { get; set; }
        public string NameOfCaller { get; set; }
        public string Call_Detail_Nature { get; set; }
        //---End---//

        public string invoiceno { get; set; }
        public string invdate { get; set; }
        public Decimal invamt { get; set; }
        public decimal recamt { get; set; }
        public decimal tothrsspent { get; set; }

        public decimal Otherclaimamount { get; set; }

        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string Date { get; set; }
        public string CustomerName { get; set; }
        public string BranchName { get; set; }
        public string PhoneNo { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string ProductName { get; set; }

        public string ServiceType { get; set; }
        public string NatureOfProblem { get; set; }

        public string JobTypes { get; set; }
        public string Status { get; set; }
        public string ReportDate { get; set; }
        public string PartNo { get; set; }
        public string CustomerAddress { get; set; }
        public string ServiceLocation { get; set; }
        public string ContactPerson { get; set; }

        public int prID { get; set; }
        public int sevID { get; set; }
        public string prName { get; set; }
        public string sevName { get; set; }


        public List<BETicketStatus> ticketStatus { get; set; }
        public List<BETicketPartDetails> partdetails { get; set; }
        public List<BETicketDocument> document { get; set; }
    }
}
