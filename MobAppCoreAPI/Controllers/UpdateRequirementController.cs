using FourQT.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes;
using MobAppCoreAPI.Interfaces;

namespace MobAppCoreAPI.Controllers
{
    [Route("api/v1/change-requirement")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UpdateRequirementController : ControllerBase
    {
        private readonly IUpdateRequirement _IUpdateRequirement;

        public UpdateRequirementController(IUpdateRequirement updaterequirement)
        {
            _IUpdateRequirement = updaterequirement;
        }

        [HttpPost]

        public async Task<APIObjectResponse> UpdateRequirement([FromBody] Requirement model)
        {
            return await _IUpdateRequirement.updaterequirement(Request, model,HttpContext);

        }
    }
}