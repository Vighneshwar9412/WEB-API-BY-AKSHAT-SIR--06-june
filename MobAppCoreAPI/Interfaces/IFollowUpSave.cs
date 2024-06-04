using FourQT.Entities;

namespace MobAppCoreAPI.Interfaces
{
    public interface IFollowUpSave
    {
        Task<dynamic> followupsave(HttpRequest req, ShortFollowUpSave lead,HttpContext context);
    }
}
