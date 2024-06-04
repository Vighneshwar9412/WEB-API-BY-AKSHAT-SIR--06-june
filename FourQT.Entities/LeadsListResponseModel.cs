using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class LeadsList
    {
        //public int SNo { get; set; }

        public int enquiryId { get; set; }
        public string? name { get; set; }
        public string? mobileNo1 { get; set; }
        public string? emailId1 { get; set; }

        public string? status { get; set; }
        public string? source { get; set; }
        public string? displayEnquiryDate { get; set; }
        public string? campaign { get; set; }
        public string? response { get; set; }

        public string? subResponse { get; set; }  // main

        public string? displayLastFollowedDate { get; set; }
        public string? displayNextFollowupDate { get; set; }

        public string? project { get; set; } // not

        public string? handler { get; set; }
        public string? owner { get; set; }
        public string? enquiryType { get; set; }
        public string? remarks { get; set; }
        public bool sVDone { get; set; }
        public string? dumpReason { get; set; }       
        public string? projectUnitType { get; set; }
        public string? unitNo { get; set; }
        public string? displayArea { get; set; }
        public string? displayCost { get; set; }
        public string? field1 { get; set; }
        public string? field2 { get; set; }
        public string? field3 { get; set; }
        public int FUT { get; set; }
        public int enquiryTypeId { get; set; }
        public string? bookingDate { get; set; }
        public int FUTAll { get; set; }
        public int ResponseSequenceNo { get; set; }
        public string? salutation {get; set;}
		public string? firstName {get; set;}
		public string? lastName {get; set;}
		public string? mobileNo2 {get; set;}
		public string? mobileNo3 {get; set;}
		public string? emailId2 {get; set;}
		public string? dob {get; set;}
        public string? doa { get; set; }
        public string? DisplayNumber { get; set; }
        public string? bookedCustName { get; set; }
    }

    public class LeadsListResponseModel
    {
        public int TotalRecords { get; set; }
        public List<LeadsList> LeadList { get; set; } = new List<LeadsList>();

    }

    public class LeadDetailsResponse 
    {
        public int enqCityId { get; set; }
        public string? enqcityName { get; set; }
        public int enqFromId { get; set; }
        public string? enqFromName { get; set; }
        public int minBudget { get; set; }
        public int maxBudget { get; set; }
        public decimal minSaleableArea { get; set; }
        public decimal maxSaleableArea { get; set; }
        public string? projectIds { get; set; }
        public string? projectNames { get; set; }
        public string? unitTypeIds { get; set; }
        public string? unitTypeNames { get; set; }
        public int minBudgetId { get; set; }
        public int maxBudgetId { get; set; }
    }
}