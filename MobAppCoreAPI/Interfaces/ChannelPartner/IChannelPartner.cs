using FourQT.Entities.Employee;

namespace MobAppCoreAPI.Interfaces.ChannelPartner
{
    public interface IChannelPartner
    {       
        public Task<dynamic> getChannelPartnerHomepage(HttpRequest req);
        public Task<dynamic> getChannelPartnerMasters(InventoryRequestShort model,HttpRequest req);
        public Task<dynamic> changePasswordCP(ChangePasswordEmployeeRequest model,HttpRequest req,HttpContext context);
    }
}
