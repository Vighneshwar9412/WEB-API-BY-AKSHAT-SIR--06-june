using FourQT.Entities;

namespace MobAppCoreAPI.Interfaces
{
    public interface ILeadSendEmail
    {
        Task<dynamic> SendleadEmail(HttpRequest req, SendEmail mail,HttpContext context);
    }
}
