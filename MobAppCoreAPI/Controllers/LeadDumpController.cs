using FourQT.Entities;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes;
using MobAppCoreAPI.Interfaces;

namespace MobAppCoreAPI.Controllers
{
    [Route("api/v1/lead-dump")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class LeadDumpController : ControllerBase
    {
        private readonly ILeadDump _Ileaddump;
        public LeadDumpController(ILeadDump leaddump)
        {
            _Ileaddump = leaddump;
        }

        [HttpPost]
        public async Task<APIObjectResponse> DumpLead([FromBody] Dump model)
        {
            return await _Ileaddump.leaddump(Request, model,HttpContext);

        }
    }
}
