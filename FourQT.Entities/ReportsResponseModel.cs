using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{

    public class Report
    {
        public int id { get; set; }

        public string? moduleName { get; set; }

        public string? moduleType { get; set; }

        public string? processName { get; set; }

        public string? processType { get; set; }

        public string? iconUrl { get; set; }

        //public string? link { get; set; }

        //public bool active { get; set; }

        //public int createBy { get; set; }

        //public string? createDate { get; set; }
    }


    public class ReportsResponseModel
    {
        public List<Report> reportSources { get; set; } = new List<Report>();
    }

    public interface IEmployeeLeads
    {
        //public int employeeId { get; set; }
        public int loginId { get; set; }
        public string? loginType { get; set; }
        public string? empName { get; set; }
    }

    public class LeadStatusReportResponse : ILeadsReportResponse, IEmployeeLeads
    {
        //public int employeeId { get; set; }
        public int loginId { get; set; }
        public string? loginType { get; set; }
        public string? empName { get; set; }
        public string? Active { get; set; }
        //public int mId { get; set; }
        public string? manager { get; set; }
        public string? hOd { get; set; }
        public string? location { get; set; }
        public string? empCode { get; set; }
        public int newLeads { get; set; }
        public int pendingLeads { get; set; }
        public int futureLeads { get; set; }
        public int todayLeads { get; set; }
        public int rejectedDumpLeads { get; set; }
        public int successLeads { get; set; }
        public int attemptedLeads { get; set; }
        public int totalLeads { get; set; }
        public int totalActiveLeads { get; set; }
        public string? filterUserLoginType { get; set; }
    }

    public class LoginWiseEnquiryCountResponse : ILeadsReportResponse
    {
        public string? loginType { get; set; }
        public string? categoryName { get; set; }
        public int rowNumber { get; set; }
        public int totalRowsCount { get; set; }
        public int lngSort { get; set; }
        public string? sourceEnqName { get; set; }
        public int emAddId { get; set; }
        public int pEnq { get; set; }
        public int sThrough { get; set; }
        public int qualified { get; set; }
        public int sVDone { get; set; }
        public string? campaign { get; set; }
        public int newLeads { get; set; }
        public int pendingLeads { get; set; }
        public int futureLeads { get; set; }
        public int todayLeads { get; set; }
        public int rejectedDumpLeads { get; set; }
        public int successLeads { get; set; }
        public int attemptedLeads { get; set; }
        public int totalLeads { get; set; }
    }

    public class GetEnquiryTypeReportResponse : ILeadsReportResponse
    {
        public string? loginType { get; set; }
        public string? enquiryType { get; set; }
        public int newLeads { get; set; }
        public int pendingLeads { get; set; }
        public int futureLeads { get; set; }
        public int todayLeads { get; set; }
        public int rejectedDumpLeads { get; set; }
        public int successLeads { get; set; }
        public int attemptedLeads { get; set; }
        public int totalLeads { get; set; }
    }

    public class GetEmployeewiseEnquiry_TodayResponse : IEmployeeLeads
    {
        public int loginId { get; set; }
        public string? empName { get; set; }
        public string? loginType { get; set; }
        public int todayFollowUp { get; set; }
        public int period9amTo12am { get; set; }
        public int period12amTo3pm { get; set; }
        public int period3pmTo6pm { get; set; }
        public int period6pmTo9pm { get; set; }
        public int todayTransferToMe { get; set; }
        public int todayTransferByMe { get; set; }
        public int todayWorked { get; set; }
        public int newLeads { get; set; }
        public int svCvDone { get; set; }
        public int newUploaded { get; set; }
        public int newNotUploaded { get; set; }
        public int totalActiveLeads { get; set; }
        public string? filterUserLoginType { get; set; }

    }

    public interface ILeadsReportResponse
    {
        public int newLeads { get; set; }
        public int pendingLeads { get; set; }
        public int futureLeads { get; set; }
        public int todayLeads { get; set; }
        public int rejectedDumpLeads { get; set; }
        public int successLeads { get; set; }
        public int attemptedLeads { get; set; }
        public int totalLeads { get; set; }
    }

    public class GetMiscellaneousReportsResponseWrap
    {
        public List<GetMiscellaneousReportsResponse>? mainReport { get; set; } = new List<GetMiscellaneousReportsResponse>();
        public List<GetMiscellaneousReportsResponse>? sideReport { get; set; } = new List<GetMiscellaneousReportsResponse>();
        public string? totalLeads { get; set; }
    }

    public interface IGetMiscellaneousReportsResponse
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int value { get; set; }
    }

    public class GetMiscellaneousReportsResponse : IGetMiscellaneousReportsResponse
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int value { get; set; }
        public string? loginType { get; set; }
    }

    public class GetMiscellaneousReportsResponse_Emp : IGetMiscellaneousReportsResponse
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int value { get; set; }
        public string? loginType { get; set; }

    }

    public class GetMiscellaneousReportsResponse_Master 
    { 
        public int id { get; set; }
        public string? name { get; set; }
        public string? reportType { get; set; } 
        public Boolean directCallToLeadLisitng { get; set; }
    }
}
