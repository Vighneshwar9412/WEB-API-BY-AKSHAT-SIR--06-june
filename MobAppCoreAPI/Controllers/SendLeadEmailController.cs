using FourQT.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Attributes;
using MobAppCoreAPI.Interfaces;

namespace MobAppCoreAPI.Controllers
{
    [Route("api/v1/send-lead-email")]
    [ApiController]
    [APIKey]
    [ApiExplorerSettings(GroupName = "v1")]
    public class SendLeadEmailController : ControllerBase
    {
        private readonly ILeadSendEmail _IsendEmail;

        public SendLeadEmailController(ILeadSendEmail sendleadEmail)
        {
            _IsendEmail = sendleadEmail;
        }

        [HttpPost]
        public async Task<dynamic> sendleadEmail([FromBody] SendEmail email)
        {
            return await _IsendEmail.SendleadEmail(Request, email,HttpContext);
        }
    }
}
