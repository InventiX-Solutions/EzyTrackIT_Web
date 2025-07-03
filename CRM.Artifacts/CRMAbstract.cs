using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRM.Artifacts
{
    [Serializable]
    public abstract class CRMAbstract
    {
      
        public Int32? CompanyID { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string LogoPath { get; set; }
        public int FinancialYear { get; set; }
        public DateTime FinancialYearStartDate { get; set; }
        public DateTime FinancialYearEndDate { get; set; }

        public Int32? ModuleID { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public int CountryID { get; set; }

        public int StateID { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonNo { get; set; }
        public string TelephoneNo { get; set; }
       
        public string OfficeNo { get; set; }
        public string MobileNo { get; set; }

        public string PhoneNumber { get; set; }

        public string FaxNo { get; set; }
        public string EmailID { get; set; }
        public string Website { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte Active { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDT { get; set; }

        public Int32 PageIndex { get; set; }
        public Int32 PageSize { get; set; }

        public string SortBy { get; set; }
        public string SortOrder { get; set; }

        public string Remarks { get; set; }
        public DataSet dsResult { get; set; }

        public int SortOrderPosition { get; set; }
        public string SortOrderDirection { get; set; }

        public string Photo { get; set; }

        
    }
}
