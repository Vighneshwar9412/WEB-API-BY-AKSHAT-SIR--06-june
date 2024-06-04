using FourQT.Core.ChannelPartner;
using FourQT.Entities.Employee;
using MobAppCoreAPI.Interfaces.ChannelPartner;

namespace MobAppCoreAPI.Repository.ChannelPartner
{
    public class ChannelPartnerRepository : IChannelPartner
    {
        public async Task<dynamic> getChannelPartnerHomepage(HttpRequest req)
        {
            return await (new ChannelPartnerLeadBLL()).getChannelPartnerHomepage(req);
        }

        public async Task<dynamic> getChannelPartnerMasters(InventoryRequestShort model, HttpRequest req)
        {
            return await (new ChannelPartnerLeadBLL()).getChannelPartnerMasters(model, req);
        }

        public async Task<dynamic> changePasswordCP(ChangePasswordEmployeeRequest model, HttpRequest req, HttpContext context)
        {
            return await (new ChannelPartnerLeadBLL()).changePasswordCP(model,req,context);
        }
    }
}
