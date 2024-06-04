using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MobAppCoreAPI.Interfaces;
using MobAppCoreAPI.Attributes;

namespace MobAppCoreAPI.Controllers
{
    
    [Route("api/v1/lead-icon")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class LeadIconController : ControllerBase
    {
        private readonly ILeadIcons _IGetLeads;

        public LeadIconController(ILeadIcons IGetLeads)
        {
            _IGetLeads = IGetLeads;
        }

        [HttpGet]       
        public async Task<dynamic> listallLeads()
        {
            return await _IGetLeads.ListDashboardIcons(Request);
        }
    }
}
