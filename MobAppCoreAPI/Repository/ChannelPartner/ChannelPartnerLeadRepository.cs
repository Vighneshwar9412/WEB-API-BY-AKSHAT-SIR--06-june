using FourQT.Core.ChannelPartner;
using FourQT.Entities.ChannelPartner;
using FourQT.Entities.Employee;
using FourQT.Masters;
using MobAppCoreAPI.Interfaces.ChannelPartner;

namespace MobAppCoreAPI.Repository.ChannelPartner
{
    public class ChannelPartnerLeadRepository : IChannelPartnerLead
    {
        public async Task<dynamic> getChannelPartnerHomepage(HttpRequest req)
        {
            return await (new ChannelPartnerLeadBLL()).getChannelPartnerHomepage(req);
        }

        public async Task<dynamic> registerChannelPartnerLead(RegisterLeadRequest model, HttpRequest req, HttpContext context)
        {
            return await (new ChannelPartnerLeadBLL()).registerChannelPartnerLead(model, req, context);

        }

        public async Task<dynamic> registerChannelPartnerLead_2(RegisterLeadRequest_2 model, HttpRequest req, HttpContext context)
        {
            return await (new ChannelPartnerLeadBLL()).registerChannelPartnerLead_2(model, req, context);

        }

        public async Task<dynamic> listChannelPartnerLeads(LeadListRequest model,HttpRequest req, HttpContext context)
        {
            return await (new ChannelPartnerLeadBLL()).listChannelPartnerLeads(model,req, context);
        }

        public async Task<dynamic> listSoldBookedUnitsCP(InventoryRequestLong model, HttpRequest req, HttpContext context)
        {
            return await (new ChannelPartnerLeadBLL()).listSoldBookedUnitsCP(model, req, context);
        }

        public async Task<dynamic> listFilterTowerFloorList(FilterDropdownRequest model, HttpRequest req, HttpContext context)
        {
            return await (new ChannelPartnerLeadBLL()).listFilterTowerFloorList(model, req, context);
        }

        public async Task<dynamic> getInventoryCP(InventoryRequestLong model, HttpRequest req, HttpContext context)
        {
            return await (new ChannelPartnerLeadBLL()).getInventoryCP(model, req, context);
        }
    }
}
