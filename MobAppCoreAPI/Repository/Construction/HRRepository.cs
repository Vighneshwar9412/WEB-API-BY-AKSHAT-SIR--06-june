using FourQT.Core.Construction;
using FourQT.Entities.Construction;
using MobAppCoreAPI.Interfaces;

namespace MobAppCoreAPI.Repository.Construction
{
    public class HRRepository : IHR
    {
        public async Task<dynamic> getAttendenceReportMonths(HttpRequest request, HttpContext context, string Type)
        {
            return await (new HRBLL()).getAttendenceReportMonths(request, context, Type);
        }

        public async Task<dynamic> getAttendenceDetails(HRAttendenceReportRequestCore model,HttpRequest request, HttpContext context, string Type)
        {
            return await (new HRBLL()).getAttendenceDetails(model, request, context,Type);
        }

        public async Task<dynamic> getAttendenceReport(HRAttendenceReportRequest model, HttpRequest request, HttpContext context, string Type)
        {
            return await (new HRBLL()).getAttendenceReport(model, request, context, Type);
        }

        public async Task<dynamic> postAttendence(HRAttendenceRequest model, HttpRequest request, HttpContext context, string Type)
        {
            return await (new HRBLL()).postAttendence(model, request, context, Type);
        }
    }
}
