using FourQT.Entities;

namespace MobAppCoreAPI.Interfaces
{
    public interface ISendleadSMS
    {
        Task<dynamic> SendleadSMS(HttpRequest req,SendSMS sms,HttpContext context);
    }
}

