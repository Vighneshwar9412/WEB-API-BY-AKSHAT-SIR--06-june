using FourQT.Entities;

namespace MobAppCoreAPI.Interfaces
{
    public interface ILeadSuccess
    {
        Task<dynamic> leadsuccess(HttpRequest req, LeadSuccess lead,HttpContext context);
    }
}
