using FourQT.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes;
using MobAppCoreAPI.Interfaces;

namespace MobAppCoreAPI.Controllers
{
    [Route("api/v1/clicktocall")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ClickToCallController : ControllerBase
    {
        private readonly IClickToCall _IClickToCall;

        public ClickToCallController(IClickToCall IClickToCall)
        {
            _IClickToCall = IClickToCall;
        }

        [HttpPost]
        [APIKey]
        public async Task<dynamic> clicktocall([FromBody] ClickCall click)
        {
            return await _IClickToCall.clicktocall(Request, click, HttpContext);
            //MemoryCache
        }
    }
}
