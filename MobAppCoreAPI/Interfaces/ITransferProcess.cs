using FourQT.Entities;
namespace MobAppCoreAPI.Interfaces
{
    public interface ITransferProcess
    {
        Task<dynamic> transferprocess(HttpRequest req, Transfer prr,HttpContext context);

        Task<dynamic> bulktransferprocess(HttpRequest req, Transfer prr, HttpContext context);
    }
}
