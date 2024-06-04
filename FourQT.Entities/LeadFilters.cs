using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FourQT.Entities.Portal;

namespace FourQT.Entities
{
    public class LeadFilters
    {
        public char LoginType { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Source_Id { get; set; }
        public string? SStatus { get; set; }
        public DateTime? EnquiryDateFrom { get; set; }
        public DateTime? EnquiryDateTo { get; set; }
        public DateTime? LastFollowedDateFrom { get; set; }
        public DateTime? LastFollowedDateTo { get; set; }
        public DateTime? NexFollowupDateFrom { get; set; }
        public DateTime? NexFollowupDateTo { get; set; }
        public string? EnquiryType { get; set; }
        public string? SiteVisitStatus { get; set; }
        public int filteredUserId { get; set; }
        public string? searchText { get; set; }
        public int projectId { get; set; }
        public int lastSubResponseId { get; set; }
        public string? filterUserLoginType { get; set; }
        public int lastResponseId { get; set; }
        public int dumpId { get; set; }
    }

    public class LeadStatusReportRequest
    {
        public string? employeeName { get; set; }
    }

    public class LoginWiseEnqCountRequest
    {
        //loginId
        public string? startDate { get; set; }
        public string? endDate { get; set; }
        public string? employeeType { get; set; }
        public string? revivalType { get; set; }
        public int currentPageSize { get; set; }
        public int pageSize { get; set; }
        public string? sourceIds { get; set; }
        public int employeeId { get; set; }
        public int projectId { get; set; }
        public string? employeeIds { get; set; }
        public Boolean includeCampaign { get; set; }
        public int campaignId { get; set; }
        public int responseId { get; set; }
        public int subResponseId { get; set; }

    }

    public class GetEnquiryTypeReportRequest
    { 
        public int month { get; set; }
        public int year { get; set; }

    }

    public class GetEmployeewiseEnquiry_TodayRequest 
    {
        public string? type { get; set; }    
    }

    public class GetMiscellaneousReportsRequest
    { 
        public string? reportType { get; set; }
        public string? param1 { get; set; }
        public string? param2 { get; set; }
        public string? param3 { get; set; }
        public string? param4 { get; set; }

    }

    public class GetMiscellaneousReportsRequest_Emp : GetMiscellaneousReportsRequest
    {

    }
}
