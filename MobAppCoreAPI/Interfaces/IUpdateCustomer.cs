using FourQT.Entities;

namespace MobAppCoreAPI.Interfaces
{
    public interface IUpdateCustomer
    {
        Task<dynamic> updatecustomer(HttpRequest req, PartyUpdate lead,HttpContext context);
    }
}
