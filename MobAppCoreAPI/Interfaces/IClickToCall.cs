using FourQT.Entities;

namespace MobAppCoreAPI.Interfaces
{
    public interface IClickToCall
    {
        Task<dynamic> clicktocall(HttpRequest req,ClickCall click, HttpContext context);
    }
}
