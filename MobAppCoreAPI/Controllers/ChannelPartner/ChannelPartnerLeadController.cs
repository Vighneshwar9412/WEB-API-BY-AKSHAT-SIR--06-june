using FourQT.Entities.ChannelPartner;
using FourQT.Entities.Employee;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes;
using MobAppCoreAPI.Attributes.ChannelPartner;
using MobAppCoreAPI.Interfaces.ChannelPartner;
using Swashbuckle.AspNetCore.Annotations;

namespace MobAppCoreAPI.Controllers.ChannelPartner
{
    [Route("/api/cp1/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "cp1")]
    [APICPKey]
    public class ChannelPartnerLeadController : ControllerBase
    {
        private readonly IChannelPartnerLead _IChannelPartnerLead;
        public ChannelPartnerLeadController(IChannelPartnerLead IChannelPartnerLead) {

            _IChannelPartnerLead = IChannelPartnerLead;

        }

        [HttpPost]
        [SwaggerOperation(Description = "Register Lead Step 1 \r\n( sendOtpForLeadRegister = 'N' No / 'Y' Yes )\r\n( uploadPhoto  = 'N' No / 'Y' Yes ) \r\nBoth from login response")]
        public async Task<dynamic> RegisterChannelPartnerLead_1([FromBody] RegisterLeadRequest model)
        {
            return await _IChannelPartnerLead.registerChannelPartnerLead(model, Request,HttpContext);
            
        }

        [HttpPost]
        [SwaggerOperation(Description = "Register Lead Step 2 \r\n( When sendOtpForLeadRegister = 'Y')")]
        public async Task<dynamic> RegisterChannelPartnerLead_2([FromBody] RegisterLeadRequest_2 model)
        {
            return await _IChannelPartnerLead.registerChannelPartnerLead_2(model, Request, HttpContext);

        }

        [HttpPost]
        public async Task<dynamic> ListChannelPartnerLeads([FromBody] LeadListRequest model)
        {
            return await _IChannelPartnerLead.listChannelPartnerLeads(model, Request, HttpContext);

        }

        [HttpPost]
        public async Task<dynamic> ListFilterTowerFloorList([FromBody] FilterDropdownRequest model)
        {
            return await _IChannelPartnerLead.listFilterTowerFloorList(model, Request, HttpContext);

        }

        [HttpPost]
        public async Task<dynamic> ListSoldBookedUnitsCP([FromBody] InventoryRequestLong model)
        {
            return await _IChannelPartnerLead.listSoldBookedUnitsCP(model, Request, HttpContext);

        }

        [HttpPost]
        [SwaggerOperation(Description = "GetInventoryUnitStatusCP ( \r\ntype : 'A' Available / 'H' Hold / 'B' Booked /\r\n 'ALL' All) ,\r\nunitNo : '' (Must be empty string to get list),\r\nminArea: 0 ,\r\nmaxArea : 100000 (must be some decimal \r\nvalue to get list between range)")]
        public async Task<dynamic> GetInventoryUnitStatusCP([FromBody] InventoryRequestLong model)
        {
            return await _IChannelPartnerLead.getInventoryCP(model, Request, HttpContext);
        }
    }
}
