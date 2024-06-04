using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FourQT.Entities
{
    public class SiteVisit 
    {
        public string? EnquiryId { get; set; }
        public string? DisplayName { get; set; }
        public string? DisplayMobile { get; set; }
        public string? ProjectName { get; set; }
        public string? TodayTime { get; set; }
    }

    public class SiteVisitResponseModel
    {
        public int totalRecords { get; set; }
        public List<SiteVisit> todaySiteVisitScheduledList { get; set; } = new List<SiteVisit>();
    }

    public class SiteVisitResponseModelNew
    {
        public int totalRecords { get; set; }
        public List<SiteVisitNew> todaySiteVisitScheduledList { get; set; } = new List<SiteVisitNew>();
    }

    public class SiteVisitNew : SiteVisit
    {
        public string? status { get; set; }
        public string? source { get; set; }
        public string? displayEnquiryDate { get; set; }
        public string? campaign { get; set; }
        public string? response { get; set; }
        public string? subResponse { get; set; }
        public string? displayLastFollowedDate { get; set; }
        public string? displayNextFollowupDate { get; set; }
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
        public string? name { get; set; }
        public string? mobileNo1 { get; set; }
        public string? emailId1 { get; set; }
        public string? project { get; set; }
        public int FUTAll { get; set; }
        public int ResponseSequenceNo { get; set; }

        public string? salutation { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? mobileNo2 { get; set; }
        public string? mobileNo3 { get; set; }
        public string? emailId2 { get; set; }
        public string? dob { get; set; }
        public string? doa { get; set; }
    }
}

