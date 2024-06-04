using FourQT.Entities;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes;
using MobAppCoreAPI.Interfaces;

namespace MobAppCoreAPI.Controllers
{
    [Route("api/v1/save-followup")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class SaveFollowUpController : ControllerBase
    {
        private readonly IFollowUpSave _IfollowupSave;

        public SaveFollowUpController(IFollowUpSave followupsave)
        {

            _IfollowupSave = followupsave;

        }

        [HttpPost]

        public async Task<APIObjectResponse> SaveFollowUp([FromBody] ShortFollowUpSave model)
        {
            return await _IfollowupSave.followupsave(Request, model,HttpContext);

        }
    }
}
