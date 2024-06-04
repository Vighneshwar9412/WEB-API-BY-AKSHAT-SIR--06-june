using FourQT.Entities;

namespace MobAppCoreAPI.Interfaces
{
    public interface IReports
    {
        public Task<APIObjectResponse> GetAllReportList(HttpRequest req, string moduleType);
        public Task<APIObjectResponse> GetLeadStatus(HttpRequest req, LeadStatusReportRequest model, HttpContext context);
        public Task<APIObjectResponse> GetLoginWiseEnquiryCount(HttpRequest req, LoginWiseEnqCountRequest model, HttpContext context);
        public Task<APIObjectResponse> GetEnquiryTypeReport(HttpRequest req, HttpContext context);
        public Task<APIObjectResponse> GetEmployeewiseEnquiryToday(HttpRequest req, GetEmployeewiseEnquiry_TodayRequest model, HttpContext context);
        public Task<APIObjectResponse> GetMiscellaneousReports_Master(HttpRequest req, HttpContext context);
        public Task<APIObjectResponse> GetMiscellaneousReports(HttpRequest req, GetMiscellaneousReportsRequest model, HttpContext context);
        public Task<APIObjectResponse> GetMiscellaneousReports_Emp(HttpRequest req, GetMiscellaneousReportsRequest_Emp model, HttpContext context); 
    }
}
