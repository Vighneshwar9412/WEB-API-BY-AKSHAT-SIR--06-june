using FourQT.Entities;

namespace MobAppCoreAPI.Interfaces
{
    public interface ILeadSave
    {
        Task<dynamic> leadsave(HttpRequest req,LeadShort lead,HttpContext context);
    }
}
