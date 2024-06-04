using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class Dashboard_TodayLeads
    {
        public string? DisplayNumber { get; set; }
        public string? MobileNo { get; set; }
        public string? ProjectName { get; set; }
        public string? TodayTime { get; set; }
        public string? SubResponse { get; set; }
        public string? ColorCode { get; set; }
        public int enquiryId { get; set; }

    }

    public class TodayLeadsResponseModel
    {
        public int totalRecords { get; set; }
        public List<Dashboard_TodayLeads> todayTodoLeadsList { get; set; } = new List<Dashboard_TodayLeads>();
    }

    public class TodayLeadsResponseModelNew
    {
        public int totalRecords { get; set; }
        public List<Dashboard_TodayLeadsNew> todayTodoLeadsList { get; set; } = new List<Dashboard_TodayLeadsNew>();
    }

    public class Dashboard_TodayLeadsNew : Dashboard_TodayLeads
    {
        public string? status { get; set; }
        public string? source { get; set; }
        public string? displayEnquiryDate { get; set; }
        public string? campaign { get; set; }       
        public string? response { get; set; }
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
