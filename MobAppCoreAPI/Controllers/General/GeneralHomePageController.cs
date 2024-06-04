using FourQT.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobAppCoreAPI.Interfaces.General;
using Swashbuckle.AspNetCore.Annotations;

namespace MobAppCoreAPI.Controllers.General
{
    [Route("/api/g1/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "g1")]
    public class GeneralHomePageController : ControllerBase
    {
        private readonly IGeneralHomepage _IGeneralHomepage;

        public GeneralHomePageController(IGeneralHomepage IGeneralHomepage)
        {
            _IGeneralHomepage = IGeneralHomepage;
        }

        [HttpGet]
        [SwaggerOperation(Description = "Pre Login Homepage \r\n(Media type: 'I' Image, 'V' Video)")]
        public async Task<ActionResult<APIObjectResponse>> GetGeneralHomepageContent(string mKey)
        {
            return await _IGeneralHomepage.getGeneralHomepageContent(mKey);
        }

        [HttpGet]
        [SwaggerOperation(Description = "Select Key for multiple projects \r\n(Token: Main DB Token)")]
        public async Task<ActionResult<APIObjectResponse>> GetMultipleProjectKeys(string mKey)
        {
            return await _IGeneralHomepage.getMultipleProjectKeys(mKey);
        }

    }
}
