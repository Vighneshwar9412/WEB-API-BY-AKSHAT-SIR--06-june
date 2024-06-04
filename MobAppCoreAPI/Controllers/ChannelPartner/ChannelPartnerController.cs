using FourQT.Entities.Employee;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes.ChannelPartner;
using MobAppCoreAPI.Interfaces.ChannelPartner;
using MobAppCoreAPI.Interfaces.Employee;

namespace MobAppCoreAPI.Controllers.ChannelPartner
{
    [Route("/api/cp1/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "cp1")]
    [APICPKey]
    public class ChannelPartnerController : ControllerBase
    {
        private readonly IChannelPartner _IChannelPartner;
        public ChannelPartnerController(IChannelPartner IChannelPartner)
        {

            _IChannelPartner = IChannelPartner;

        }

        [HttpGet]
        public async Task<dynamic> GetChannelPartnerHomepage()
        {
            return await _IChannelPartner.getChannelPartnerHomepage(Request);

        }

        [HttpPost]
        public async Task<dynamic> GetChannelPartnerMasters([FromBody] InventoryRequestShort model)
        {
            return await _IChannelPartner.getChannelPartnerMasters(model, Request);

        }

        [HttpPost]
        public async Task<dynamic> ChangePasswordCP([FromBody] ChangePasswordEmployeeRequest model)
        {
            return await _IChannelPartner.changePasswordCP(model, Request, HttpContext);
        }
    }
}
