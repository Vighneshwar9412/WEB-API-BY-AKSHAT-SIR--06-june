using FourQT.Entities;

namespace MobAppCoreAPI.Interfaces
{
    public interface IUpdateRequirement
    {
        Task<dynamic> updaterequirement(HttpRequest req, Requirement require,HttpContext context);
    }
}
