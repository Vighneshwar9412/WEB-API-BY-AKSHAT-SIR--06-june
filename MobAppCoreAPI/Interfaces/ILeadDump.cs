using FourQT.Entities;

namespace MobAppCoreAPI.Interfaces
{
    public interface ILeadDump
    {
        Task<dynamic> leaddump(HttpRequest req, Dump lead,HttpContext context);
    }
}
