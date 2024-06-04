using FourQT.Entities.ChannelPartner;
using FourQT.Entities.Employee;

namespace MobAppCoreAPI.Interfaces.ChannelPartner
{
    public interface IChannelPartnerLead
    {
        public Task<dynamic> getChannelPartnerHomepage(HttpRequest req);
        public Task<dynamic> registerChannelPartnerLead(RegisterLeadRequest model, HttpRequest req,HttpContext context); 
        public Task<dynamic> registerChannelPartnerLead_2(RegisterLeadRequest_2 model, HttpRequest req, HttpContext context); 
        public Task<dynamic> listChannelPartnerLeads(LeadListRequest model, HttpRequest req, HttpContext context); 
        public Task<dynamic> listFilterTowerFloorList(FilterDropdownRequest model, HttpRequest req, HttpContext context);
        public Task<dynamic> listSoldBookedUnitsCP(InventoryRequestLong model, HttpRequest req, HttpContext context);
        public Task<dynamic> getInventoryCP(InventoryRequestLong model, HttpRequest req, HttpContext context);
    }
}
