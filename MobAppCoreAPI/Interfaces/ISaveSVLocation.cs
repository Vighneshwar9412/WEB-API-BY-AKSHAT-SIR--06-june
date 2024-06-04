using FourQT.Entities;

namespace MobAppCoreAPI.Interfaces
{
    public interface ISaveSVLocation
    {
        Task<dynamic> SVlocationsave(HttpRequest req, SVLocationShort lead,HttpContext context);
    }
}
