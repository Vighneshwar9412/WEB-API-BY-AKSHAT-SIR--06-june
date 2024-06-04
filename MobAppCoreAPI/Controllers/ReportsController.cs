using FourQT.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace MobAppCoreAPI.Controllers
{
    [Route("api/v1/Reports/")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ReportsController : ControllerBase
    {
        public readonly IReports _report;

        public ReportsController(IReports Report)
        {
            _report = Report;
        }


        [HttpGet]
        [Route("GetReportsListing")]
        [SwaggerOperation(Description = "Get Reports List (moduleType = 'L')")]
        public async Task<APIObjectResponse> GetAllReportList(string moduleType)
        {
            return await _report.GetAllReportList(Request, moduleType);
        }

        [HttpPost]
        [Route("GetEmployeeWiseLeadStatus")]
        public async Task<APIObjectResponse> GetLeadStatus([FromBody] LeadStatusReportRequest model)
        {
            return await _report.GetLeadStatus(Request, model, HttpContext);
        }

        [HttpPost]
        [Route("GetSourceWiseLeadStatus")]
        public async Task<APIObjectResponse> GetLoginWiseEnquiryCount([FromBody] LoginWiseEnqCountRequest model)
        {
            return await _report.GetLoginWiseEnquiryCount(Request, model, HttpContext);
        }

        [HttpGet]
        [Route("GetEnquiryTypeReport")]
        public async Task<APIObjectResponse> GetEnquiryTypeReport()
        {
            return await _report.GetEnquiryTypeReport(Request, HttpContext);
        }

        [HttpPost]
        [Route("GetEmployeewiseEnquiry_Today")]
        public async Task<APIObjectResponse> GetEmployeewiseEnquiryToday([FromBody] GetEmployeewiseEnquiry_TodayRequest model)
        {
            return await _report.GetEmployeewiseEnquiryToday(Request, model, HttpContext);
        }

        [HttpGet]
        [Route("GetMiscellaneousReports_Master")]
        public async Task<APIObjectResponse> GetMiscellaneousReports_Master()
        {
            return await _report.GetMiscellaneousReports_Master(Request, HttpContext);
        }

        [HttpPost]
        [Route("GetMiscellaneousReports")]
        public async Task<APIObjectResponse> GetMiscellaneousReports([FromBody] GetMiscellaneousReportsRequest model)
        {
            return await _report.GetMiscellaneousReports(Request, model, HttpContext);
        }

        [HttpPost]
        [Route("GetMiscellaneousReports_Emp")]
        public async Task<APIObjectResponse> GetMiscellaneousReports_Emp([FromBody] GetMiscellaneousReportsRequest_Emp model)
        {
            return await _report.GetMiscellaneousReports_Emp(Request, model, HttpContext);
        }
    }
}
