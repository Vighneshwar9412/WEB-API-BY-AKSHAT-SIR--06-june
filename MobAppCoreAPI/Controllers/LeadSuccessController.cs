using FourQT.Entities;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes;
using MobAppCoreAPI.Interfaces;

namespace MobAppCoreAPI.Controllers
{
    [Route("api/v1/lead-success")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class LeadSuccessController : ControllerBase
    {
        private readonly ILeadSuccess _Ileadsuccess;

        public LeadSuccessController(ILeadSuccess leadsuccess)
        {

            _Ileadsuccess = leadsuccess;

        }


        [HttpPost]

        public async Task<APIObjectResponse> SuccessLead([FromBody] LeadSuccess model)
        {
            return await _Ileadsuccess.leadsuccess(Request, model,HttpContext);

        }
    }
}
