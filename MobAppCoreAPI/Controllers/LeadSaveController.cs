using FourQT.Entities;
using FourQT.UserRights;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Interfaces;
using FourQT.Core;
using MobAppCoreAPI.Attributes;

namespace MobAppCoreAPI.Controllers
{
    [Route("api/v1/save-enquiry")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class LeadSaveController : ControllerBase
    {
        private readonly ILeadSave _Ileadsave;
        public LeadSaveController(ILeadSave leadsave) { 

            _Ileadsave= leadsave;

        }


        [HttpPost]

        public async Task<APIObjectResponse> SaveEnquiry([FromBody] LeadShort model)
        {
             return await _Ileadsave.leadsave(Request, model,HttpContext);
            
        }
    }
}
