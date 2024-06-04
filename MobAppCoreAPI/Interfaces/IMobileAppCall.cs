using FourQT.Entities;

namespace MobAppCoreAPI.Interfaces
{
    public interface IMobileAppCall
    {
        public Task<APIObjectResponse> recordMobAppCall(HttpRequest req, MobAppCall model, HttpContext context);
        public Task<APIObjectResponse> mobAppCallReport(HttpRequest req, MobAppCallReportReq model, HttpContext context);
    }
}
