using FourQT.Entities.Construction;

namespace MobAppCoreAPI.Interfaces
{
    public interface IHR
    {
        public Task<dynamic> getAttendenceReportMonths(HttpRequest request, HttpContext context, string Type);
        public Task<dynamic> getAttendenceDetails(HRAttendenceReportRequestCore model, HttpRequest request, HttpContext context, string Type);
        public Task<dynamic> postAttendence(HRAttendenceRequest model, HttpRequest request, HttpContext context, string Type);
        public Task<dynamic> getAttendenceReport(HRAttendenceReportRequest model, HttpRequest request, HttpContext context, string Type); 

    }
}
